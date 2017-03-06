using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChsuSchedule.Data.Parser
{
	/// <summary>Запись расписания экзаменов преподавателя..</summary>
	class TeacherExamScheduleRecord
	{
		public string Date { get; set; }

		public string Weekday { get; set; }

		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Group { get; set; }

		public string Classroom { get; set; }
	}
}
