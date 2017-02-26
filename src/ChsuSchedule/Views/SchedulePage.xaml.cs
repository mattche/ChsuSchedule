using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ChsuSchedule.ViewModels;
using ChsuSchedule.Models;

namespace ChsuSchedule.Views
{
	public partial class SchedulePage : ContentPage
	{
		public SchedulePage(ScheduleConfiguration configuration)
		{
			InitializeComponent();

			BindingContext = new SchedulePageViewModel(configuration);
		}
	}
}
