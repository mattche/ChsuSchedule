using System;
using System.Threading.Tasks;

namespace ChsuSchedule.Data
{
	/// <summary>Хранилище расписаний преподавателей.</summary>
	public interface ITeacherScheduleRepository
	{
		/// <summary>Возвращает расписание преподавателя на неделю, содержащую выбраную дату.</summary>
		/// <param name="group">ФИО преподавателя.</param>
		/// <param name="date">Дата.</param>
		/// <returns>Расписание на неделю.</returns>
		TeacherWeekSchedule FetchSchedule(string teacher, DateTime date);

		/// <summary>Асинхронно возвращает расписание преподавателя на неделю, содержащую выбраную дату.</summary>
		/// <param name="group">ФИО преподавателя.</param>
		/// <param name="date">Дата.</param>
		/// <returns>Задача, возвращающая расписание на неделю.</returns>
		Task<TeacherWeekSchedule> FetchScheduleAsync(string teacher, DateTime date);
	}
}