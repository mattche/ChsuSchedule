using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace ChsuSchedule.ViewModels
{
	public class SchedulePageViewModel : ViewModelBase
	{
		public SchedulePageViewModel(WeekSchedule weekSchedule)
		{
			_weekSchedule = weekSchedule;
			ScheduleDays = new ObservableCollection<Grouping<DateTime, DayScheduleRow>>();
			FillSchedule();
		}

		public ObservableCollection<Grouping<DateTime, DayScheduleRow>> ScheduleDays { get; }

		private WeekSchedule _weekSchedule;

		private void FillSchedule()
		{
			ScheduleDays.Clear();
			var groupings = _weekSchedule.DaySchedules
				.Select(s => new Grouping<DateTime, DayScheduleRow>(s.Date, s.Rows));
			foreach (var g in groupings)
			{
				ScheduleDays.Add(g);
			}
		}
	}
}
