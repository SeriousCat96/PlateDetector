using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Logging
{
	/// <summary> Описывает типы сообщений лога. </summary>
	public interface ILogMessage
	{
		Color Color { get; }
		string FormattedMessage { get; }
		string Text { get; }
		MessageType Type { get; }
	}
}
