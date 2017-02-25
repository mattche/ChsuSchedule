using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChsuSchedule.Data
{
	public static class ChsuCalendarCalculator
	{
		/// <summary>Вычисляет номер семестра.</summary>
		/// <param name="date">Дата.</param>
		/// <returns>Номер семестра.</returns>
		public static int CalcSemester(DateTime date)
		{
			var first = 9;
			var second = 2;
			if (date.Month >= first && date.Month < second) return 1;
			else return 2;
		}

		/// <summary>Вычисляет номер учебной недели.</summary>
		/// <param name="date">Дата.</param>
		/// <returns>Номер учебной недели.</returns>
		public static int CalcWeekNumber(DateTime date)
		{
			var begYear = date.Month < 9 ? date.Year - 1 : date.Year;
			var begMonth = 9;
			var begDate = new DateTime(begYear, begMonth, 1);
			if(begDate.DayOfWeek != DayOfWeek.Monday)
			{
				var knownBegWeekDayNum = begDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)begDate.DayOfWeek;
				var begDay = begDate.Day - knownBegWeekDayNum + 1;
				
				if(begDay < 0)
				{
					--begMonth;
					begDay = DateTime.DaysInMonth(begYear, begMonth) - (-begDay);
				}

				begDate = new DateTime(begYear, begMonth, begDay);
			}
			var delta = date.Subtract(begDate);
			var number = delta.Days / 7 + 1;

			return number;
		}

		/// <summary>Вычисляет дату указанного дня недели, определяемой указанной датой.</summary>
		/// <param name="dayOfWeek">День недели.</param>
		/// <param name="knownDate">Известый день недели.</param>
		/// <returns>Дата.</returns>
		public static DateTime CalcDate(DateTime knownDate, DayOfWeek dayOfWeek)
		{
			if (knownDate.DayOfWeek == dayOfWeek) return knownDate;

			var knownDayNum = knownDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)knownDate.DayOfWeek;
			var dayNum = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
			return knownDate.AddDays(dayNum - knownDayNum);
		}
	}
}
