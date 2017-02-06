using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChsuSchedule.Data
{
	public class StudentWeekSchedule
	{
		public StudentWeekSchedule(int weekNumber, IEnumerable<StudentDaySchedule> daySchedules)
		{
			WeekNumber = weekNumber;
			DaySchedules = new List<StudentDaySchedule>(daySchedules);
		}

		public int WeekNumber { get; set; }

		public IList<StudentDaySchedule> DaySchedules { get; }
	}

	public class StudentDaySchedule
	{
		public StudentDaySchedule(DayOfWeek weekday, IEnumerable<StudentDayScheduleRow> rows)
		{
			Weekday = weekday;
			Rows = new List<StudentDayScheduleRow>(rows);
		}

		public DayOfWeek Weekday { get; set; }

		public IList<StudentDayScheduleRow> Rows { get; }
	}

	public class StudentDayScheduleRow
	{
		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Teacher { get; set; }

		public string Classroom { get; set; }

		public DateTime Date { get; set; }
	}
}
