using System;
using System.Text;
using System.IO;

using NUnit.Framework;
using NSubstitute;

using ChsuSchedule.Data.Html;

namespace ChsuSchedule.Data.Tests
{
	[TestFixture]
	class StudentScheduleRepositoryTest : AssertionHelper
	{
		#region Tests

		[Test]
		public void FetchScheduleTest()
		{
			var expectedRow = new StudentDayScheduleRow
			{
				Classroom = "212 Сов.8",
				Duration = "13-30 - 15-00",
				Subject = "лр Организация производства",
				Teacher = "Виноградова Л.Н. доцент"
			};

			var proxy = Substitute.For<IScheduleContentSource>();
			proxy.GetStudentScheduleContent(GROUP, SEMESTER, MODE).Returns((info) =>
			{
				using (var stream = new StreamReader(FILENAME, Encoding.GetEncoding("windows-1251")))
				{
					return stream.ReadToEnd();
				}
			});

			var rep = new StudentScheduleRepository(proxy);
			var schedule = rep.FetchSchedule(GROUP, DATE);

			Expect(schedule.DaySchedules, Is.Not.Null);
			Expect(schedule.DaySchedules.Count, Is.Not.EqualTo(0));

			var daySchedule = schedule.DaySchedules[1];

			Expect(daySchedule.Rows, Is.Not.Null);
			Expect(daySchedule.Rows.Count, Is.Not.EqualTo(0));

			Expect(daySchedule.Rows[0].Classroom, Is.EqualTo(expectedRow.Classroom));
			Expect(daySchedule.Rows[0].Duration, Is.EqualTo(expectedRow.Duration));
			Expect(daySchedule.Rows[0].Subject, Is.EqualTo(expectedRow.Subject));
			Expect(daySchedule.Rows[0].Teacher, Is.EqualTo(expectedRow.Teacher));
		}

		#endregion

		#region Test data

		private readonly string FILENAME = @"Tests\ChsuSchedule.Data.Tests\data\students.txt";
		private static readonly DateTime DATE = new DateTime(2017, 02, 14);
		private const string GROUP = "1ПИб-01-41оп";
		private const int SEMESTER = 2;
		private const ScheduleMode MODE = ScheduleMode.ClassesSchedule;

		#endregion
	}
}
