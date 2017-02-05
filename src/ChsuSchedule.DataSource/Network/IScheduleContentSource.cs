using System.Threading.Tasks;

namespace ChsuSchedule.Data
{
	interface IScheduleContentSource
	{
		/// <summary>Получает содержимое HTML-страницы расписания студентов.</summary>
		/// <param name="group">Группа.</param>
		/// <param name="semester">Семестр.</param>
		/// <param name="mode">Режим (расписание занятий или расписание экзаменов).</param>
		/// <returns>Содержимое HTML-страницы расписания студентов.</returns>
		string GetStudentScheduleContent(string group, int semester, ScheduleMode mode);

		/// <summary>Асинхронно получает содержимое HTML-страницы расписания студентов.</summary>
		/// <param name="group">Группа.</param>
		/// <param name="semester">Семестр.</param>
		/// <param name="mode">Режим (расписание занятий или расписание экзаменов).</param>
		/// <returns>Задача, возвращающая содержимое HTML-страницы расписания студентов.</returns>
		Task<string> GetStudentScheduleContentAsync(string group, int semester, ScheduleMode mode);

		/// <summary>Получает содержимое HTML-страницы расписания преподавателей.</summary>
		/// <param name="group">Группа.</param>
		/// <param name="semester">Семестр.</param>
		/// <param name="mode">Режим (расписание занятий или расписание экзаменов).</param>
		/// <returns>Содержимое HTML-страницы расписания преподавателей.</returns>
		string GetTeacherScheduleContent(string group, int semester, ScheduleMode mode);

		/// <summary>Асинхронно получает содержимое HTML-страницы расписания преподавателей.</summary>
		/// <param name="group">Группа.</param>
		/// <param name="semester">Семестр.</param>
		/// <param name="mode">Режим (расписание занятий или расписание экзаменов).</param>
		/// <returns>Задача, возвращающая содержимое HTML-страницы расписания преподавателей.</returns>
		Task<string> GetTeacherScheduleContentAsync(string group, int semester, ScheduleMode mode);
	}
}