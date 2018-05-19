using System.Drawing;

namespace Platedetector.Utils.Logging
{
	/// <summary> Описывает типы сообщений лога.</summary>
	public interface ILogMessage
	{
		/// <summary> Цвет, которым выделяется текст в окне лога.</summary>
		Color Color { get; }
		/// <summary> Сообщение в форматированном виде, в котором оно выводится в лог.</summary>
		string FormattedMessage { get; }
		/// <summary> Тип сообщения лога.</summary>
		LogMessageType Type { get; }
	}
}
