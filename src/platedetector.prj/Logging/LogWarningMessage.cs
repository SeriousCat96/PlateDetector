using System;
using System.Drawing;

namespace PlateDetector.Logging
{
	/// <summary> Реализует предупреждающее сообщение для лога. </summary>
	public sealed class LogWarningMessage : ILogMessage
	{
		/// <summary> Создает <see cref="LogWarningMessage"/>.</summary>
		/// <param name="text"> Текст сообщения.</param>
		public LogWarningMessage(string text)
		{
			Text = text;
			Type = LogMessageType.Warning;
			Color = Color.OrangeRed;
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
