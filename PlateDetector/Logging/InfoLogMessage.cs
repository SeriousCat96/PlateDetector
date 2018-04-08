using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Logging
{
	public sealed class InfoLogMessage : ILogMessage
	{
		public InfoLogMessage(string text)
		{
			Text			 = text;
			Type			 = MessageType.Info;
			Color			 = Color.DarkSlateGray;
			FormattedMessage = ToString();
		}

		public Color Color { get; }
		public string FormattedMessage { get; }
		public string Text { get; }
		public MessageType Type { get; }

		public override string ToString()
		{
			return $"[{DateTime.Now}][{Type.ToString().ToUpper()}]: {Text}";
		}
	}
}
