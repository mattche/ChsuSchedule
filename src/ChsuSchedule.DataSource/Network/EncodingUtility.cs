using System;
using System.Text;

namespace ChsuSchedule.Data
{
	/// <summary>Костыль для кодирования значений форм.</summary>
	static class EncodingUtility
	{
		public static string UrlWindowsEncode(string value)
		{
			var enc = Encoding;
			var msg = value.ToCharArray();
			var builder = new StringBuilder();

			for (var i = 0; i < msg.Length; i++)
			{
				if (char.IsDigit(msg[i]) || char.IsPunctuation(msg[i]))
				{
					builder.Append(msg[i]);
				}
				else if (msg[i] == ' ')
				{
					builder.Append('+');
				}
				else
				{
					var b = Encoding.GetBytes(msg, i, 1);
					builder.AppendFormat("%{0}", BitConverter.ToString(b));
				}
			}

			return builder.ToString();
		}

		public static Encoding Encoding => Encoding.GetEncoding("windows-1251");
	}
}