using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChsuSchedule.Models
{
	public class ScheduleConfiguration
	{
		static class Defaults
		{
			public const string GROUP = "1ПИб-01-41-оп";
			public const bool SHOW_ALL_WEEK = true;
			public static readonly DateTime DATE = DateTime.Now;
		}

		public ScheduleConfiguration()
		{
			Group = Defaults.GROUP;
			Date = Defaults.DATE;
			ShowAllWeek = Defaults.SHOW_ALL_WEEK;
		}

		public string Group { get; set; }

		public DateTime Date { get; set; }

		public bool ShowAllWeek { get; set; }
	}
}
