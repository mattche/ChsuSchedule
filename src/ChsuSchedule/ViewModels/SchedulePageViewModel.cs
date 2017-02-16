using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChsuSchedule.Data;

namespace ChsuSchedule.ViewModels
{
	class SchedulePageViewModel : ViewModelBase
	{
		public string SelectedGroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = value;
				OnPropertyChanged(nameof(SelectedGroupName));
			}
		}

		public DateTime SelectedDate
		{
			get { return _date; }
			set
			{
				_date = value;
				OnPropertyChanged(nameof(SelectedDate));
			}
		}

		public bool ShowAllWeek
		{
			get { return _showAllWeek; }
			set
			{
				_showAllWeek = value;
				OnPropertyChanged(nameof(ShowAllWeek));
			}
		}

		public ObservableCollection<DayScheduleViewModel> ScheduleDays { get; set; }

		private async void FillSchedule()
		{
			var rep = new StudentScheduleRepository();
			var schedule = await rep.FetchScheduleAsync(SelectedGroupName, SelectedDate);
			foreach (var day in schedule.DaySchedules)
			{
				ScheduleDays.Add(new DayScheduleViewModel { Model = day });
			}
		}

		private string _groupName;
		private DateTime _date;
		private bool _showAllWeek;
	}
}
