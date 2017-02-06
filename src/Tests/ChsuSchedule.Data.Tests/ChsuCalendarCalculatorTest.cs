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
			var dt = new DateTime(2017, 2, 7);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(24));
			dt = new DateTime(2017, 2, 9);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(24));
			dt = new DateTime(2017, 2, 13);
			Expect(ChsuCalendarCalculator.CalcWeekNumber(dt), Is.EqualTo(25));
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
