using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;

using ChsuSchedule.Data.Parser;
using ChsuSchedule.Data.Html;

namespace ChsuSchedule.Data
{
	public sealed class StudentScheduleRepository : IStudentScheduleRepository
	{
		#region .ctor

		public StudentScheduleRepository()
			: this(ScheduleContentSourceFactory.CreateDefaultSource())
		{
		}

		public StudentScheduleRepository(IScheduleContentSource contentSource)
		{
			_source = contentSource;
			_parser = new ScheduleParser();
		}

		#endregion

		#region Methods

		public StudentWeekSchedule FetchSchedule(string group, DateTime date)
		{
			if (!_classesScheduleCache.HasValue || group != _classesScheduleCache.Value.Key)
			{
				var semester = ChsuCalendarCalculator.CalcSemester(date);
				var html = _source.GetStudentScheduleContent(group, semester, ScheduleMode.ClassesSchedule);
				var parsedRecords = _parser.ParseStundentClassesSchedule(html);
				_classesScheduleCache = new KeyValuePair<string, IEnumerable<StudentClassesScheduleRecord>>(group, parsedRecords);
			}

			return FetchScheduleCore(date);
		}

		public async Task<StudentWeekSchedule> FetchScheduleAsync(string group, DateTime date)
		{
			if (!_classesScheduleCache.HasValue || group != _classesScheduleCache.Value.Key)
			{
				var semester = ChsuCalendarCalculator.CalcSemester(date);
				var html = await _source.GetStudentScheduleContentAsync(group, semester, ScheduleMode.ClassesSchedule);
				var parsedRecords = _parser.ParseStundentClassesSchedule(html);
				_classesScheduleCache = new KeyValuePair<string, IEnumerable<StudentClassesScheduleRecord>>(group, parsedRecords);
			}

			return FetchScheduleCore(date);
		}

		#endregion

		#region Private methods

		private StudentWeekSchedule FetchScheduleCore(DateTime date)
		{
			var records = _classesScheduleCache.Value.Value;
			var weekNum = ChsuCalendarCalculator.CalcWeekNumber(date);
			var isWeekEven = weekNum % 2 == 0;

			var filtered = records
				.Where(rec => rec.WeeksStart <= weekNum && rec.WeeksEnd >= weekNum)
				.Where(rec =>
				{
					if (isWeekEven)
						return rec.Periodicity == Periodicity.Even || rec.Periodicity == Periodicity.Weekly;
					else
						return rec.Periodicity == Periodicity.Odd || rec.Periodicity == Periodicity.Weekly;
				});

			var weekdays = filtered.Select(rec => rec.Weekday).Distinct();

			var daySchedules = weekdays
				.Select(wd =>
				{
					var rows = filtered
						.Where(rec => rec.Weekday == wd)
						.Select(rec => new StudentDayScheduleRow()
						{
							Duration = rec.Duration,
							Subject = rec.Subject,
							Classroom = rec.Classroom,
							Teacher = rec.Teacher
						});
					return new StudentDaySchedule(ChsuCalendarCalculator.CalcDate(date, wd), rows);
				});

			return new StudentWeekSchedule(weekNum, daySchedules);
		}

		#endregion

		#region Data

		private KeyValuePair<string, IEnumerable<StudentClassesScheduleRecord>>? _classesScheduleCache;

		private IScheduleContentSource _source;

		private IScheduleParser _parser;
		
		#endregion
	}
}
