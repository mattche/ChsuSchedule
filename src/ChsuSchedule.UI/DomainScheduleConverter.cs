using System;
using System.Linq;

using ChsuSchedule.Data;

namespace ChsuSchedule
{
	public static class DomainScheduleConverter
	{
		public static WeekSchedule ToWeekSchedule(StudentWeekSchedule studentWeekSchedule)
		{
			var daySchedules = studentWeekSchedule.DaySchedules
				.Select(daySchedule => ToDaySchedule(daySchedule));
			return new WeekSchedule(studentWeekSchedule.WeekNumber, daySchedules);
		}

		public static DaySchedule ToDaySchedule(StudentDaySchedule studentDaySchedule)
		{
			var rows = studentDaySchedule.Rows
				.Select(row =>
				{
					return new DayScheduleRow
					{
						Classroom = row.Classroom,
						Duration = row.Duration,
						Participant = row.Teacher,
						Subject = row.Subject
					};
				});
			return new DaySchedule(studentDaySchedule.Date, rows);
		}

		public static WeekSchedule ToWeekSchedule(TeacherWeekSchedule teacherWeekSchedule)
		{
			var daySchedules = teacherWeekSchedule.DaySchedules
				.Select(daySchedule => ToDaySchedule(daySchedule));
			return new WeekSchedule(teacherWeekSchedule.WeekNumber, daySchedules);
		}

		public static DaySchedule ToDaySchedule(TeacherDaySchedule teacherDaySchedule)
		{
			var rows = teacherDaySchedule.Rows
				.Select(row =>
				{
					return new DayScheduleRow
					{
						Classroom = row.Classroom,
						Duration = row.Duration,
						Participant = row.Group,
						Subject = row.Subject
					};
				});
			return new DaySchedule(teacherDaySchedule.Date, rows);
		}
	}
}
