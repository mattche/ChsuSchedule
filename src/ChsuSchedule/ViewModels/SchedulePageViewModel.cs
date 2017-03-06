using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using ChsuSchedule.Data;

namespace ChsuSchedule.ViewModels
{
	public class SchedulePageViewModel : ViewModelBase
	{
		public SchedulePageViewModel(StudentWeekSchedule weekSchedule)
		{
			_weekSchedule = weekSchedule;
			ScheduleDays = new ObservableCollection<Grouping<DateTime, StudentDayScheduleRow>>();
			FillSchedule();
		}

		public ObservableCollection<Grouping<DateTime, StudentDayScheduleRow>> ScheduleDays { get; }

		private StudentWeekSchedule _weekSchedule;

		private void FillSchedule()
		{
			ScheduleDays.Clear();
			var groups = _weekSchedule.DaySchedules
				.Select(s => new Grouping<DateTime, StudentDayScheduleRow>(s.Date, s.Rows));
			foreach (var g in groups)
			{
				ScheduleDays.Add(g);
			}
		}
	}
}
