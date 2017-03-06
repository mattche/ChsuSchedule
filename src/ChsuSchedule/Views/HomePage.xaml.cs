using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ChsuSchedule.ViewModels;

namespace ChsuSchedule.Views
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
			var vm = new HomePageViewModel() { Navigation = this.Navigation };
			BindingContext = vm;
			vm.AlertOccured += OnAlertOccured;
		}

		private async void OnAlertOccured(object sender, string e)
		{
			await DisplayAlert("Ошибка", e, "OK");
		}
	}
}
