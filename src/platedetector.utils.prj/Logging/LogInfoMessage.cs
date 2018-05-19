using System;
using System.Drawing;

namespace Platedetector.Utils.Logging
{
	/// <summary> Реализует информационное сообщение для лога. </summary>
	public sealed class LogInfoMessage : ILogMessage
	{
		/// <summary> Создает <see cref="LogInfoMessage"/>.</summary>
		/// <param name="text"> Текст сообщения.</param>
		public LogInfoMessage(string text)
		{
			Text			 = text;
			Type			 = LogMessageType.Info;
			Color			 = Color.DarkSlateGray;
			FormattedMessage = ToString();
		}

		/// <summary> Цвет, которым выделяется текст в окне лога.</summary>
		public Color Color { get; }
		/// <summary> Сообщение в форматированном виде, в котором оно выводится в лог.</summary>
		public string FormattedMessage { get; }
		/// <summary> Текст сообщения лога.</summary>
		public string Text { get; }
		/// <summary> Тип сообщения лога.</summary>
		public LogMessageType Type { get; }

		/// <summary> Строковое предстваление объекта.</summary>
		/// <returns> Форматированное сообщение в виде строки.</returns>
		public override string ToString()
		{
			return $"[{DateTime.Now}][{Type.ToString().ToUpper()}]: {Text}";
		}
	}
}
