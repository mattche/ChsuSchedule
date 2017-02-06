using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChsuSchedule.Data.Parser
{
	/// <summary>Запись расписания занятий студентов.</summary>
	class StudentClassesScheduleRecord
	{
		public DayOfWeek Weekday { get; set; }

		public string Duration { get; set; }

		public string Subject { get; set; }

		public int WeeksStart { get; set; }

		public int WeeksEnd { get; set; }

		public Periodicity Periodicity { get; set; }

		public string Teacher { get; set; }

		public string Classroom { get; set; }
	}
}
