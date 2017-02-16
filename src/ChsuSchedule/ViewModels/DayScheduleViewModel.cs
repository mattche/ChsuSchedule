using System;
using System.Collections.Generic;

using ChsuSchedule.Data;

namespace ChsuSchedule.ViewModels
{
	class DayScheduleViewModel : ViewModelBase
	{
		public DateTime Date
		{
			get { return Model.Date; }
		}

		public IList<StudentDayScheduleRow> ScheduleRows
		{
			get { return Model.Rows; }
		}

		public StudentDaySchedule Model { get; set; }
	}
}
