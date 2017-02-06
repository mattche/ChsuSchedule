using System;
using System.Collections.Generic;
using System.Linq;

using AngleSharp.Parser.Html;

namespace ChsuSchedule.Data.Parser
{
	sealed class ScheduleParser : IScheduleParser
	{
		public IEnumerable<StudentClassesScheduleRecord> ParseStundentClassesSchedule(string htmlContent)
		{
			var parser = new HtmlParser();
			var document = parser.Parse(htmlContent);

			var rawRecords = document.QuerySelectorAll("tr")
				.Skip(3)
				.Where(tr => tr.QuerySelectorAll("td").Count() == 14
						  && tr.QuerySelectorAll("td").Count(td => td.TextContent != "\n") != 0)
				.Select(tr => new 
				{
					Weekday = tr.QuerySelector("td").TextContent.Trim(),
					Duration = tr.QuerySelectorAll("td")
						.Skip(1 * 2)
						.FirstOrDefault()
						.TextContent.Trim(),
					Subject = tr.QuerySelectorAll("td")
						.Skip(2 * 2)
						.FirstOrDefault()
						.TextContent.Trim(),
					Weeks = tr.QuerySelectorAll("td")
						.Skip(3 * 2)
						.FirstOrDefault()
						.TextContent.Trim(),
					Periodicity = tr.QuerySelectorAll("td")
						.Skip(4 * 2)
						.FirstOrDefault()
						.TextContent.Trim(),
					Teacher = tr.QuerySelectorAll("td")
						.Skip(5 * 2)
						.FirstOrDefault()
						.TextContent.Trim(),
					Classroom = tr.QuerySelectorAll("td")
						.Skip(6 * 2)
						.FirstOrDefault()
						.TextContent.Trim()
				});

			var records = new List<StudentClassesScheduleRecord>();
			foreach(var rec in rawRecords)
			{
				int weeksStart, weeksEnd;
				ParseWeeks(rec.Weeks, out weeksStart, out weeksEnd);

				Periodicity periodicity;
				try
				{
					periodicity = ParsePeriodicity(rec.Periodicity);
				}
				catch { return null; }

				DayOfWeek weekday;
				try
				{
					weekday = ParseDayOfWeak(rec.Weekday);
				}
				catch { return null; }

				records.Add(new StudentClassesScheduleRecord
				{
					Classroom = rec.Classroom,
					Duration = rec.Duration,
					Periodicity = periodicity,
					Subject = rec.Subject,
					Teacher = rec.Teacher,
					Weekday = weekday,
					WeeksStart = weeksStart,
					WeeksEnd = weeksEnd
				});
			}

			return records;
		}

		private void ParseWeeks(string value, out int weeksStart, out int weeksEnd)
		{
			var substrings = value.Split(' ');
			int temp;
			if (int.TryParse(substrings[1], out temp))
				weeksStart = temp;
			else
				weeksStart = 0;

			if (int.TryParse(substrings[3], out temp))
				weeksEnd = temp;
			else
				weeksEnd = 0;
		}

		private Periodicity ParsePeriodicity(string value)
		{
			switch (value)
			{
				case "чет":
					return Periodicity.Even;
				case "нечет":
					return Periodicity.Odd;
				case "ежен":
					return Periodicity.Weekly;
				default:
					throw new ArgumentException("Uncorrect periodicity value", nameof(value));
			}
		}

		private DayOfWeek ParseDayOfWeak(string value)
		{
			switch(value)
			{
				case "понедельник":
					return DayOfWeek.Monday;
				case "вторник":
					return DayOfWeek.Tuesday;
				case "среда":
					return DayOfWeek.Wednesday;
				case "четверг":
					return DayOfWeek.Thursday;
				case "пятница":
					return DayOfWeek.Friday;
				case "суббота":
					return DayOfWeek.Saturday;
				case "воскресенье":
					return DayOfWeek.Sunday;
				default:
					throw new ArgumentException("Uncorrect day of weak value", nameof(value));
			}
		}
	}
}
