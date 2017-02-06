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
			var delta = date.Subtract(new DateTime(begYear, 9, 1));
			var number = delta.Days / 7;
			if (delta.Days % 7 > 0)
			{
				++number;
			}
			return ++number;
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
