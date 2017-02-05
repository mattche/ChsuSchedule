using System;

namespace ChsuSchedule.Data
{
	public enum ScheduleMode
	{
		ClassesSchedule,
		ExamSchedule
	}

	public static class ScheduleModeExtensions
	{
		public static string GetName(this ScheduleMode mode)
		{
			switch(mode)
			{
				case ScheduleMode.ClassesSchedule:
					return "Расписание занятий";
				case ScheduleMode.ExamSchedule:
					return "Расписание экзаменов";
				default:
					return string.Empty;
			}
		}
	}
}
