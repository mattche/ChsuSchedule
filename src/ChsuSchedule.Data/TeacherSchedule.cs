using System;
using System.Collections.Generic;

namespace ChsuSchedule.Data
{
	public class TeacherWeekSchedule
	{
		public TeacherWeekSchedule(int weekNumber, IEnumerable<TeacherDaySchedule> daySchedules)
		{
			WeekNumber = weekNumber;
			DaySchedules = new List<TeacherDaySchedule>(daySchedules);
		}

		public int WeekNumber { get; set; }

		public IList<TeacherDaySchedule> DaySchedules { get; }
	}

	public class TeacherDaySchedule
	{
		public TeacherDaySchedule(DateTime date, IEnumerable<TeacherDayScheduleRow> rows)
		{
			Date = date;
			Rows = new List<TeacherDayScheduleRow>(rows);
		}

		public DateTime Date { get; set; }

		public IList<TeacherDayScheduleRow> Rows { get; }
	}

	public class TeacherDayScheduleRow
	{
		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Group { get; set; }

		public string Classroom { get; set; }

		public string DurationAndSubject => $"{Duration} {Subject}";
	}
}
