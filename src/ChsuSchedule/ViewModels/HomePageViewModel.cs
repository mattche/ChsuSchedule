using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;

using Xamarin.Forms;

using ChsuSchedule.Data;
using ChsuSchedule.Models;

namespace ChsuSchedule.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
		public HomePageViewModel()
		{
			_scheduleConfig = new ScheduleConfiguration();
			ScheduleDays = new ObservableCollection<DayScheduleViewModel>();
			FillSchedule();
		}

		#region Properties

		public string SelectedGroup
		{
			get { return _scheduleConfig.Group; }
			set
			{
				_scheduleConfig.Group = value;
				OnPropertyChanged(nameof(SelectedGroup));
			}
		}

		public DateTime SelectedDate
		{
			get { return _scheduleConfig.Date; }
			set
			{
				_scheduleConfig.Date = value;
				OnPropertyChanged(nameof(SelectedDate));
			}
		}

		public bool ShowAllWeek
		{
			get { return _scheduleConfig.ShowAllWeek; }
			set
			{
				_scheduleConfig.ShowAllWeek = value;
				OnPropertyChanged(nameof(ShowAllWeek));
			}
		}

		public ObservableCollection<DayScheduleViewModel> ScheduleDays { get; set; }

		public ICommand FillScheduleCommand { protected set; get; }

		#endregion

		private async void FillSchedule()
		{
			ScheduleDays.Clear();
			var rep = new StudentScheduleRepository();
			var schedule = await rep.FetchScheduleAsync(SelectedGroup, SelectedDate);
			if (schedule == null || schedule?.DaySchedules == null) return;
			foreach (var day in schedule.DaySchedules)
			{
				ScheduleDays.Add(new DayScheduleViewModel(day));
			}
		}

		private ScheduleConfiguration _scheduleConfig;
	}
}
