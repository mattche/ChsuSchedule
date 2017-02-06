using System.Collections.Generic;

namespace ChsuSchedule.Data.Parser
{
	interface IScheduleParser
	{
		/// <summary>Выполняет анализ HTML-страницы расписания занятий студентов и возвращает записи расписания.</summary>
		/// <param name="htmlContent">HTML-страница расписания занятий студентов.</param>
		/// <returns>Записи расписания занятий студентов.</returns>
		IEnumerable<StudentClassesScheduleRecord> ParseStundentClassesSchedule(string htmlContent);
	}
}