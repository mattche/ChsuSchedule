using System;
using System.Collections.Generic;

namespace ChsuSchedule
{
	public class WeekSchedule
	{
		public WeekSchedule(int weekNumber, IEnumerable<DaySchedule> daySchedules)
		{
			WeekNumber = weekNumber;
			DaySchedules = new List<DaySchedule>(daySchedules);
		}

		public int WeekNumber { get; set; }

		public IList<DaySchedule> DaySchedules { get; }
	}

	public class DaySchedule
	{
		public DaySchedule(DateTime date, IEnumerable<DayScheduleRow> rows)
		{
			Date = date;
			Rows = new List<DayScheduleRow>(rows);
		}

		public DateTime Date { get; set; }

		public IList<DayScheduleRow> Rows { get; set; }
	}

	public class DayScheduleRow
	{
		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Participant { get; set; }

		public string Classroom { get; set; }

		public string DurationAndSubject => $"{Duration} {Subject}";
	}
}
