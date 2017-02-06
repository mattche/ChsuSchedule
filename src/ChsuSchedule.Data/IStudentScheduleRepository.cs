using System;
using System.Threading.Tasks;

namespace ChsuSchedule.Data
{
	/// <summary>Хранилище расписаний студентов.</summary>
	public interface IStudentScheduleRepository
	{
		/// <summary>Возвращает расписание группы на неделю, содержащую выбраную дату.</summary>
		/// <param name="group">Назване группы.</param>
		/// <param name="date">Дата.</param>
		/// <returns>Расписание на неделю.</returns>
		StudentWeekSchedule FetchSchedule(string group, DateTime date);

		/// <summary>Асинхронно возвращает расписание группы на неделю, содержащую выбраную дату.</summary>
		/// <param name="group">Название группы.</param>
		/// <param name="date">Дата.</param>
		/// <returns>Задача, возвращающая расписание на неделю.</returns>
		Task<StudentWeekSchedule> FetchScheduleAsync(string group, DateTime date);
	}
}