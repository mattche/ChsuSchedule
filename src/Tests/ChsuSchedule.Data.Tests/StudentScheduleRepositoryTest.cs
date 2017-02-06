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
				Date = new DateTime(2017, 2, 14),
				Classroom = "212 Сов.8",
				Duration = "13-30 - 15-00",
				Subject = "лр Организация производства",
				Teacher = "Виноградова Л.Н. доцент"
			};

			var proxy = Substitute.For<IScheduleContentSource>();
			proxy.GetStudentScheduleContent(_group, _sem, _mode).Returns((info) =>
			{
				using (var stream = new StreamReader(_filename, Encoding.GetEncoding("windows-1251")))
				{
					return stream.ReadToEnd();
				}
			});

			var rep = new StudentScheduleRepository(proxy);
			var schedule = rep.FetchSchedule(_group, new DateTime(2017, 02, 14));
			var daySchedule = schedule.DaySchedules[1];

			Expect(daySchedule.Rows[0].Date, Is.EqualTo(expectedRow.Date));
			Expect(daySchedule.Rows[0].Classroom, Is.EqualTo(expectedRow.Classroom));
			Expect(daySchedule.Rows[0].Duration, Is.EqualTo(expectedRow.Duration));
			Expect(daySchedule.Rows[0].Subject, Is.EqualTo(expectedRow.Subject));
			Expect(daySchedule.Rows[0].Teacher, Is.EqualTo(expectedRow.Teacher));
		}

		#endregion

		#region Test data

		const string _filename = @"Tests\ChsuSchedule.Data.Tests\data\raspisanie.html";
		const string _group = "1ПИб-01-41оп";
		const int _sem = 2;
		const ScheduleMode _mode = ScheduleMode.ClassesSchedule;

		#endregion
	}
}
