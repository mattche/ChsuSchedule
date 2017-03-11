using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

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

		public string SelectedTeacher
		{
			get { return Configuration.Teacher; }
			set
			{
				Configuration.Teacher = value;
				RisePropertyChanged(nameof(SelectedTeacher));
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

		public bool IsTeacherSchedule
		{
			get { return Configuration.IsTeacherSchedule; }
			set
			{
				Configuration.IsTeacherSchedule = value;
				RisePropertyChanged(nameof(IsTeacherSchedule));
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

		private async Task LoadScheduleAsync(object param)
		{
			if (Configuration == null) return;

			WeekSchedule schedule = null;
			IsBusy = true;

			try
			{
				if (Configuration.IsTeacherSchedule)
				{
					var rep = new TeacherScheduleRepository();
					var data = await rep.FetchScheduleAsync(Configuration.Teacher, Configuration.Date);
					schedule = DomainScheduleConverter.ToWeekSchedule(data);
				}
				else
				{
					var rep = new StudentScheduleRepository();
					var data = await rep.FetchScheduleAsync(Configuration.Group, Configuration.Date);
					schedule = DomainScheduleConverter.ToWeekSchedule(data);
				}
			}
			catch (Exception)
			{
				RiseAlertOccured("Не удалось загрузить расписание.");
				IsBusy = false;
				return;
			}

			IsBusy = false;
			if (schedule == null || schedule?.DaySchedules == null || schedule.DaySchedules.Count == 0)
			{
				RiseAlertOccured("Не удалось отобразить расписание.");
				return;
			}

			if (!Configuration.ShowAllWeek)
			{
				schedule = new WeekSchedule(
					schedule.WeekNumber,
					schedule.DaySchedules.Where(s => s.Date == Configuration.Date));
			}
			await Navigation.PushAsync(new SchedulePage(new SchedulePageViewModel(schedule)));
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
