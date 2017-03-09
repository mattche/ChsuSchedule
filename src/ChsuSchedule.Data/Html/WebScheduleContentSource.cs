using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ChsuSchedule.Data.Html
{
	sealed class WebScheduleContentSource : IScheduleContentSource
	{
		public async Task<string> GetStudentScheduleContentAsync(string group, int semester, ScheduleMode mode)
		{
			var groupEnc = EncodingUtility.UrlWindowsEncode(group);
			var modeEnc = EncodingUtility.UrlWindowsEncode(mode.GetName());
			var content = new StringContent(
				$"gr={groupEnc}&ss={semester}&mode={modeEnc}",
				EncodingUtility.Encoding,
				"application/x-www-form-urlencoded");

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BASE_URL);
				var response = await client.PostAsync(STUDENT_URL, content).ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		public async Task<string> GetTeacherScheduleContentAsync(string teacher, int semester, ScheduleMode mode)
		{
			var teacherEnc = EncodingUtility.UrlWindowsEncode(teacher);
			var modeEnc = EncodingUtility.UrlWindowsEncode(mode.GetName());
			var content = new StringContent(
				$"pr={teacherEnc}&sss={semester}&mode={modeEnc}",
				EncodingUtility.Encoding,
				"application/x-www-form-urlencoded");

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BASE_URL);
				var response = await client.PostAsync(TEACHER_URL, content).ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		public string GetStudentScheduleContent(string group, int semester, ScheduleMode mode)
		{
			return GetStudentScheduleContentAsync(group, semester, mode).Result;
		}

		public string GetTeacherScheduleContent(string teacher, int semester, ScheduleMode mode)
		{
			return GetTeacherScheduleContentAsync(teacher, semester, mode).Result;
		}

		private static readonly string BASE_URL = @"https://rasp.chsu.ru/";

		private static readonly string STUDENT_URL = @"_student.php/";

		private static readonly string TEACHER_URL = @"_prepod.php/";
	}
}
