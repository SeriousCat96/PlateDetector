using System;
using System.Drawing;

namespace Platedetector.Utils.Logging
{
	/// <summary> Реализует сообщение для лога об ошибке. </summary>
	public sealed class LogErrorMessage : ILogMessage
	{
		/// <summary> Создает <see cref="LogErrorMessage"/>.</summary>
		/// <param name="text"> Текст сообщения.</param>
		public LogErrorMessage(string text)
		{
			Text			 = text;
			Type			 = LogMessageType.Error;
			Color			 = Color.Red;
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
			return $"[{DateTime.Now}][{Type.ToString().ToUpper()}] {Text}";
		}
	}
}
