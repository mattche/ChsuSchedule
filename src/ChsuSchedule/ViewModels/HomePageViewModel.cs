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
using ChsuSchedule.Views;

namespace ChsuSchedule.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
		public HomePageViewModel()
		{
			Configuration = new ScheduleConfiguration();
			ShowScheduleCommand = new Command(ShowSchedule);
		}

		#region Properties

		public string SelectedGroup
		{
			get { return Configuration.Group; }
			set
			{
				Configuration.Group = value;
				OnPropertyChanged(nameof(SelectedGroup));
			}
		}

		public DateTime SelectedDate
		{
			get { return Configuration.Date; }
			set
			{
				Configuration.Date = value;
				OnPropertyChanged(nameof(SelectedDate));
			}
		}

		public bool ShowAllWeek
		{
			get { return Configuration.ShowAllWeek; }
			set
			{
				Configuration.ShowAllWeek = value;
				OnPropertyChanged(nameof(ShowAllWeek));
			}
		}

		public ICommand ShowScheduleCommand { protected set; get; }

		public INavigation Navigation { get; set; }

		public ScheduleConfiguration Configuration { get; set; }

		#endregion

		private void ShowSchedule(object scheduleConfiguration)
		{
			var config = scheduleConfiguration as ScheduleConfiguration;
			if (config != null)
			{
				Navigation.PushAsync(new SchedulePage(Configuration));
			}
		}
	}
}
