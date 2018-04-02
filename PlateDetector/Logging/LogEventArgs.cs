﻿using System;

namespace PlateDetector.Logging
{
	/// <summary> Предоставляет данные для события <see cref="Log.LogFileUpdated"/>. </summary>
	public class LogEventArgs : EventArgs
	{
		/// <summary> Создаёт <see cref="LogEventArgs"/>. </summary>
		/// <param name="message"> Сообщение лога. </param>
		public LogEventArgs(string message)
		{
			Message = message;
		}

		/// <summary> Сообщение лога. </summary>
		public string Message { get; }
	}
}
