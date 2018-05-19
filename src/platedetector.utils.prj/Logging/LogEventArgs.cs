using System;

namespace Platedetector.Utils.Logging
{
	/// <summary> Предоставляет данные для события <see cref="Log.LogFileUpdated"/>. </summary>
	public class LogEventArgs : EventArgs
	{
		/// <summary> Создаёт <see cref="LogEventArgs"/>. </summary>
		/// <param name="message"> Сообщение лога. </param>
		public LogEventArgs(ILogMessage message)
		{
			Message = message;
		}

		/// <summary> Сообщение лога. </summary>
		public ILogMessage Message { get; }
	}
}
