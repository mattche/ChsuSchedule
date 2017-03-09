using System;
using System.Collections.ObjectModel;

namespace ChsuSchedule.ViewModels
{
	public class DayScheduleViewModel : ViewModelBase
	{
		public DayScheduleViewModel(DaySchedule daySchedule)
		{
			DaySchedule = daySchedule;
			ScheduleRows = new ObservableCollection<DayScheduleRow>(daySchedule.Rows);
		}

		public DateTime Date
		{
			get { return DaySchedule.Date; }
		}

		public ObservableCollection<DayScheduleRow> ScheduleRows { get; }

		private DaySchedule DaySchedule { get; set; }
	}
}
