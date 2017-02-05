using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ChsuSchedule.Data
{
	sealed class WebScheduleContentSource : IScheduleContentSource
	{
		public async Task<string> GetStudentScheduleContentAsync(string group, int semester, ScheduleMode mode)
		{
			return await GetScheduleContentCoreAsync(group, semester, mode, STUDENT_URL);
		}

		public async Task<string> GetTeacherScheduleContentAsync(string group, int semester, ScheduleMode mode)
		{
			return await GetScheduleContentCoreAsync(group, semester, mode, TEACHER_URL);
		}

		public string GetStudentScheduleContent(string group, int semester, ScheduleMode mode)
		{
			return GetStudentScheduleContentAsync(group, semester, mode).Result;
		}

		public string GetTeacherScheduleContent(string group, int semester, ScheduleMode mode)
		{
			return GetTeacherScheduleContentAsync(group, semester, mode).Result;
		}

		private async Task<string> GetScheduleContentCoreAsync(string group, int semester, ScheduleMode mode, string url)
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
				var response = await client.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		private static readonly string BASE_URL = @"https://rasp.chsu.ru/";

		private static readonly string STUDENT_URL = @"_student.php/";

		private static readonly string TEACHER_URL = @"_prepod.php/";
	}
}
