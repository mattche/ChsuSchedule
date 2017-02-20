using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ChsuSchedule.Data;

namespace ChsuSchedule.ViewModels
{
	public class DayScheduleViewModel : ViewModelBase
	{
		public DayScheduleViewModel(StudentDaySchedule daySchedule)
		{
			DaySchedule = daySchedule;
			ScheduleRows = new ObservableCollection<StudentDayScheduleRow>(daySchedule.Rows);
		}

		public DateTime Date
		{
			get { return DaySchedule.Date; }
		}

		public ObservableCollection<StudentDayScheduleRow> ScheduleRows { get; }

		private StudentDaySchedule DaySchedule { get; set; }
	}
}
