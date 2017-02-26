using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using ChsuSchedule.Data;
using ChsuSchedule.Models;

namespace ChsuSchedule.ViewModels
{
	public class SchedulePageViewModel : ViewModelBase
	{
		public SchedulePageViewModel(ScheduleConfiguration configuration)
		{
			_configuration = configuration;
			ScheduleDays = new ObservableCollection<Grouping<DateTime, StudentDayScheduleRow>>();
			FillSchedule();
		}

		public ObservableCollection<Grouping<DateTime, StudentDayScheduleRow>> ScheduleDays { get; }

		private ScheduleConfiguration _configuration;

		private async void FillSchedule()
		{
			ScheduleDays.Clear();
			var rep = new StudentScheduleRepository();
			var schedule = await rep.FetchScheduleAsync(_configuration.Group, _configuration.Date);
			if (schedule == null || schedule?.DaySchedules == null) return;
			var groups = schedule.DaySchedules
				.Select(s => new Grouping<DateTime, StudentDayScheduleRow>(s.Date, s.Rows));
			foreach (var g in groups)
			{
				ScheduleDays.Add(g);
			}
		}
	}
}
