using System;
using System.ComponentModel;

namespace ChsuSchedule.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
