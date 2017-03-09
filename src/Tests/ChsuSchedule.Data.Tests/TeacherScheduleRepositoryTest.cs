using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NUnit.Framework;
using NSubstitute;

using ChsuSchedule.Data.Html;

namespace ChsuSchedule.Data.Tests
{
	[TestFixture]
	class TeacherScheduleRepositoryTest : AssertionHelper
	{
		[Test]
		public void FetchScheduleTest()
		{
			var expectedRow = new TeacherDayScheduleRow
			{
				Classroom = "227 Сов.8",
				Duration = "13-30 - 15-00",
				Subject = "л Администрирование информационных систем",
				Group = "1ИСб-00-41оп"
			};

			var proxy = Substitute.For<IScheduleContentSource>();
			proxy.GetTeacherScheduleContent(TEACHER, SEMESTER, MODE).Returns((info) =>
			{
				using (var stream = new StreamReader(FILENAME, Encoding.GetEncoding("windows-1251")))
				{
					return stream.ReadToEnd();
				}
			});

			var rep = new TeacherScheduleRepository(proxy);
			var schedule = rep.FetchSchedule(TEACHER, DATE);

			Expect(schedule.DaySchedules, Is.Not.Null);
			Expect(schedule.DaySchedules.Count, Is.Not.EqualTo(0));

			var daySchedule = schedule.DaySchedules[1];

			Expect(daySchedule.Rows, Is.Not.Null);
			Expect(daySchedule.Rows.Count, Is.Not.EqualTo(0));

			Expect(daySchedule.Rows[0].Classroom, Is.EqualTo(expectedRow.Classroom));
			Expect(daySchedule.Rows[0].Duration, Is.EqualTo(expectedRow.Duration));
			Expect(daySchedule.Rows[0].Subject, Is.EqualTo(expectedRow.Subject));
			Expect(daySchedule.Rows[0].Group, Is.EqualTo(expectedRow.Group));
		}

		#region Test data

		private readonly string FILENAME = @"Tests\ChsuSchedule.Data.Tests\data\teachers.txt";
		private static readonly DateTime DATE = new DateTime(2017, 03, 07);
		private const string TEACHER = "Селяничев О.Л. доцент";
		private const int SEMESTER = 2;
		private const ScheduleMode MODE = ScheduleMode.ClassesSchedule;

		#endregion
	}
}
