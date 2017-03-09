using System;

namespace ChsuSchedule
{
	public class ScheduleConfiguration
	{
		static class Defaults
		{
			public const string GROUP = "1ПИб-01-41оп";
			public const string TEACHER = "Селяничев О.Л. доцент";
			public const bool SHOW_ALL_WEEK = true;
			public static readonly DateTime DATE = DateTime.Now;
			public const bool IS_THEACHER_SCHEDULE = false;
		}

		public ScheduleConfiguration()
		{
			SetDefaults();
		}

		public string Group { get; set; }

		public string Teacher { get; set; }

		public DateTime Date { get; set; }

		public bool ShowAllWeek { get; set; }

		public bool IsTeacherSchedule { get; set; }

		public void SetDefaults()
		{
			Group = Defaults.GROUP;
			Date = Defaults.DATE;
			ShowAllWeek = Defaults.SHOW_ALL_WEEK;
			IsTeacherSchedule = Defaults.IS_THEACHER_SCHEDULE;
			Teacher = Defaults.TEACHER;
		}
	}
}
