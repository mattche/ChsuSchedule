using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace ChsuSchedule.Data.Tests
{
	[TestFixture]
	class ChsuCalendarCalculatorTest : AssertionHelper
	{
		[Test]
		public void CalcSemesterTest()
		{
			var dt = new DateTime(2017, 2, 7);
			Expect(ChsuCalendarCalculator.CalcSemester(dt), Is.EqualTo(2));
		}

		[Test]
		public void CalcWeekNumberTest()
		{
			var dt = new DateTime(2016, 9, 2);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(1));
			dt = new DateTime(2016, 9, 4);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(1));
			dt = new DateTime(2016, 9, 5);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(2));
			dt = new DateTime(2017, 2, 13);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(25));
			dt = new DateTime(2017, 2, 26);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(26));
		}

		[Test]
		public void CalcDate()
		{
			var expectedDate1 = new DateTime(2017, 2, 13);
			var expectedDate2 = new DateTime(2017, 2, 19);
			Expect(ChsuCalendarCalculator.CalcDate(new DateTime(2017, 2, 16), expectedDate1.DayOfWeek), Is.EqualTo(expectedDate1));
			Expect(ChsuCalendarCalculator.CalcDate(new DateTime(2017, 2, 16), expectedDate2.DayOfWeek), Is.EqualTo(expectedDate2));
		}
	}
}
