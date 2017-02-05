using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Globalization;
using System.Linq;

using AngleSharp.Parser.Html;

namespace ChsuSchedule.Data
{
	public class ScheduleRepository
	{
		public async Task<string> GetSheduleContent()
		{
			using (var client = new HttpClient())
			{
				var group = EncodingUtility.UrlWindowsEncode("1ПИб-01-41оп");
				var sem = 1;
				var mode = EncodingUtility.UrlWindowsEncode("Расписание занятий");
				var content = new StringContent(
					$"gr={group}&ss={sem}&mode={mode}",
					EncodingUtility.Encoding,
					"application/x-www-form-urlencoded");

				var response = await client.PostAsync(_url, content);
				response.EnsureSuccessStatusCode();

				var html = await response.Content.ReadAsStringAsync();

				var list = new List<string>();
				var parser = new HtmlParser();
				var document = parser.Parse(html);

				var records = document.QuerySelectorAll("tr")
					.Skip(3)
					.Where(tr => tr.QuerySelectorAll("td").Count() == 14 
							  && tr.QuerySelectorAll("td").Count(td => td.TextContent != "\n") != 0)
					.Select(tr => new
					{
						Weekday = tr.QuerySelector("td").TextContent.Trim(),
						Duration = tr.QuerySelectorAll("td")
							.Skip(1 * 2)
							.FirstOrDefault()
							.TextContent.Trim(),
						Subject = tr.QuerySelectorAll("td")
							.Skip(2 * 2)
							.FirstOrDefault()
							.TextContent.Trim(),
						Weeks = tr.QuerySelectorAll("td")
							.Skip(3 * 2)
							.FirstOrDefault()
							.TextContent.Trim(),
						Periodicity = tr.QuerySelectorAll("td")
							.Skip(4 * 2)
							.FirstOrDefault()
							.TextContent.Trim(),
						Teacher = tr.QuerySelectorAll("td")
							.Skip(5 * 2)
							.FirstOrDefault()
							.TextContent.Trim(),
						Classroom = tr.QuerySelectorAll("td")
							.Skip(6 * 2)
							.FirstOrDefault()
							.TextContent.Trim()
					});

				return "";
			}
		}

		private string _url = @"https://rasp.chsu.ru/_student.php/";
	}
}
