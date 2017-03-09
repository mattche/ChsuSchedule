using System;
using System.ComponentModel;

namespace ChsuSchedule.ViewModels
{
	/// <summary>Базовый класс для модели представления.</summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RisePropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
