using System;

namespace ChsuSchedule.Data.Parser
{
	/// <summary>Запись расписания экзаменов студентов.</summary>
	class StudentExamScheduleRecord
	{
		public string Date { get; set; }

		public string Weekday { get; set; }

		public string Duration { get; set; }

		public string Subject { get; set; }

		public string Teacher { get; set; }

		public string Classroom { get; set; }
	}
}
