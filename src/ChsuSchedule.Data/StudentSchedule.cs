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
		public StudentDaySchedule(DateTime date, IEnumerable<StudentDayScheduleRow> rows)
		{
			Date = date;
			Rows = new List<StudentDayScheduleRow>(rows);
		}

		public DateTime Date { get; set; }

		public IList<StudentDayScheduleRow> Rows { get; }
	}

	public class StudentDayScheduleRow
	{
		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Teacher { get; set; }

		public string Classroom { get; set; }
	}
}
