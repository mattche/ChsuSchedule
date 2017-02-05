using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace ChsuSchedule.Data.Tests
{
	[TestFixture]
	class EncodingUtilityTest : AssertionHelper
	{
		[Test]
		public static void UrlWindowsEncodeTest()
		{
			var cases = new Dictionary<string, string>
			{
				{ "1ПИб-01-41оп", "1%CF%C8%E1-01-41%EE%EF" },
				{ "02.06.01-04-21оп", "02.06.01-04-21%EE%EF" },
				{ "Расписание занятий", "%D0%E0%F1%EF%E8%F1%E0%ED%E8%E5+%E7%E0%ED%FF%F2%E8%E9" },
				{ "1", "1" }
			};

			foreach(var item in cases)
			{
				Expect(EncodingUtility.UrlWindowsEncode(item.Key), Is.EqualTo(item.Value));
			}
		}
	}
}
