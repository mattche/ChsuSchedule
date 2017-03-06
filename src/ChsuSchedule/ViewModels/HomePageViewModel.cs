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
using ChsuSchedule.Views;

namespace ChsuSchedule.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
		#region .ctor

		public HomePageViewModel()
		{
			Configuration = new ScheduleConfiguration();
			ShowScheduleCommand = new Command(async (param) => await LoadScheduleAsync(param));
		}

		#endregion

		#region Properties

		public string SelectedGroup
		{
			get { return Configuration.Group; }
			set
			{
				Configuration.Group = value;
				RisePropertyChanged(nameof(SelectedGroup));
			}
		}

		public DateTime SelectedDate
		{
			get { return Configuration.Date; }
			set
			{
				Configuration.Date = value;
				RisePropertyChanged(nameof(SelectedDate));
			}
		}

		public bool ShowAllWeek
		{
			get { return Configuration.ShowAllWeek; }
			set
			{
				Configuration.ShowAllWeek = value;
				RisePropertyChanged(nameof(ShowAllWeek));
			}
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RisePropertyChanged(nameof(IsBusy));
				RisePropertyChanged(nameof(IsNotBusy));
			}
		}

		public bool IsNotBusy => !IsBusy;

		public ICommand ShowScheduleCommand { protected set; get; }

		public INavigation Navigation { get; set; }

		public ScheduleConfiguration Configuration { get; set; }

		#endregion

		#region Events

		public event EventHandler<string> AlertOccured;

		#endregion

		#region Methods

		private async Task LoadScheduleAsync(object scheduleConfiguration)
		{
			var config = scheduleConfiguration as ScheduleConfiguration;
			if (config != null)
			{
				var rep = new StudentScheduleRepository();
				IsBusy = true;
				StudentWeekSchedule schedule = null;

				try
				{
					schedule = await rep.FetchScheduleAsync(config.Group, config.Date);
				}
				catch (Exception exc)
				{
					RiseAlertOccured("Не удалось загрузить расписание.");
					IsBusy = false;
					return;
				}

				IsBusy = false;

				if (schedule == null || schedule?.DaySchedules == null)
				{
					RiseAlertOccured("Не удалось получить расписание");
					return;
				}

				await Navigation.PushAsync(new SchedulePage(new SchedulePageViewModel(schedule)));
			}
		}

		private void RiseAlertOccured(string message)
		{
			AlertOccured?.Invoke(this, message);
		}

		#endregion

		#region Data

		private bool _isBusy;

		#endregion
	}
}
