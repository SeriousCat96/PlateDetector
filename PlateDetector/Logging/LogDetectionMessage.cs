using OpenCvSharp;

using System;
using System.Drawing;

namespace PlateDetector.Logging
{
	/// <summary> Реализует сообщение о детектировании для лога. </summary>
	public sealed class LogDetectionMessage : ILogMessage
	{
		/// <summary> Создает <see cref="LogDetectionMessage"/>.</summary>
		/// <param name="detection"> Найденный объект.</param>
		public LogDetectionMessage(Detection.Detection detection)
		{
			Detection			 = detection;
			Type			 = LogMessageType.Detection;
			Color			 = Color.Green;
			FormattedMessage = ToString();
		}

		/// <summary> Цвет, которым выделяется текст в окне лога.</summary>
		public Color Color { get; }
		/// <summary> Найденный объект.</summary>
		public Detection.Detection Detection { get; }
		/// <summary> Сообщение в форматированном виде, в котором оно выводится в лог.</summary>
		public string FormattedMessage { get; }
		/// <summary> Тип сообщения лога.</summary>
		public LogMessageType Type { get; }

		/// <summary> Строковое предстваление объекта.</summary>
		/// <returns> Форматированное сообщение в виде строки.</returns>
		public override string ToString()
		{
			return $"[{DateTime.Now}][{Type.ToString().ToUpper()}]: {Detection}";
		}
	}
}
