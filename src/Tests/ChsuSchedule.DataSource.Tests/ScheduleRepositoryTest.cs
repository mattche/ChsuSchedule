using System;

using NUnit.Framework;

namespace ChsuSchedule.Data.Tests
{
	[TestFixture]
	public class ScheduleRepositoryTest : AssertionHelper
	{
		[Test]
		public void GetSheduleContentTest()
		{
			var repository = new ScheduleRepository();
			var content = repository.GetSheduleContent().Result;
			Expect(content, Is.Not.Null);
		}
	}
}
