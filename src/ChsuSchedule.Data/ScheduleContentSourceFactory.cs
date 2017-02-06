using System;

using ChsuSchedule.Data.Html;

namespace ChsuSchedule.Data
{
	public static class ScheduleContentSourceFactory
	{
		public static IScheduleContentSource CreateWebSource()
		{
			return new WebScheduleContentSource();
		}

		public static IScheduleContentSource CreateDefaultSource()
		{
			return CreateWebSource();
		}
	}
}
