using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChsuSchedule.Data.Parser;
using ChsuSchedule.Data.Html;

namespace ChsuSchedule.Data
{
	public sealed class TeacherScheduleRepository
	{
		#region .ctor

		public TeacherScheduleRepository()
			: this(ScheduleContentSourceFactory.CreateDefaultSource())
		{
		}

		public TeacherScheduleRepository(IScheduleContentSource contentSource)
		{
			_source = contentSource;
			_parser = new ScheduleParser();
		}

		#endregion

		#region Methods

		public TeacherWeekSchedule FetchSchedule(string teacher, DateTime date)
		{
			if (!_classesScheduleCache.HasValue || teacher != _classesScheduleCache.Value.Key)
			{
				var semester = ChsuCalendarCalculator.CalcSemester(date);
				var html = _source.GetTeacherScheduleContent(teacher, semester, ScheduleMode.ClassesSchedule);
				var parsedRecords = _parser.ParseTeacherClassesSchedule(html);
				_classesScheduleCache = new KeyValuePair<string, IEnumerable<TeacherClassesScheduleRecord>>(teacher, parsedRecords);
			}

			return FetchScheduleCore(date);
		}

		public async Task<TeacherWeekSchedule> FetchScheduleAsync(string teacher, DateTime date)
		{
			if (!_classesScheduleCache.HasValue || teacher != _classesScheduleCache.Value.Key)
			{
				var semester = ChsuCalendarCalculator.CalcSemester(date);
				var html = await _source.GetTeacherScheduleContentAsync(teacher, semester, ScheduleMode.ClassesSchedule).ConfigureAwait(false);
				var parsedRecords = _parser.ParseTeacherClassesSchedule(html);
				_classesScheduleCache = new KeyValuePair<string, IEnumerable<TeacherClassesScheduleRecord>>(teacher, parsedRecords);
			}

			return FetchScheduleCore(date);
		}

		#endregion

		#region Private methods

		private TeacherWeekSchedule FetchScheduleCore(DateTime date)
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
						return rec.Periodicity == Periodicity.NotEven || rec.Periodicity == Periodicity.Weekly;
				});

			var weekdays = filtered.Select(rec => rec.Weekday).Distinct();

			var daySchedules = weekdays
				.Select(wd =>
				{
					var rows = filtered
						.Where(rec => rec.Weekday == wd)
						.Select(rec => new TeacherDayScheduleRow()
						{
							Duration = rec.Duration,
							Subject = rec.Subject,
							Classroom = rec.Classroom,
							Group = rec.Group
						});
					return new TeacherDaySchedule(ChsuCalendarCalculator.CalcDate(date, wd), rows);
				});

			return new TeacherWeekSchedule(weekNum, daySchedules);
		}

		#endregion

		#region Data

		private KeyValuePair<string, IEnumerable<TeacherClassesScheduleRecord>>? _classesScheduleCache;

		private IScheduleContentSource _source;

		private IScheduleParser _parser;
		
		#endregion
	}
}
