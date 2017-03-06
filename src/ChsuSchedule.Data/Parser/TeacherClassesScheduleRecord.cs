using System;

namespace ChsuSchedule.Data.Parser
{
	/// <summary>Запись расписания занятий преподавателей.</summary>
	class TeacherClassesScheduleRecord
	{
		public DayOfWeek Weekday { get; set; }

		public string Duration { get; set; }

		public string Subject { get; set; }

		public int WeeksStart { get; set; }

		public int WeeksEnd { get; set; }

		public Periodicity Periodicity { get; set; }

		public string Group { get; set; }

		public string Classroom { get; set; }
	}
}
