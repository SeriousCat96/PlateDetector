namespace Platedetector.Utils.Logging
{
	/// <summary> Типы сообщений лога.</summary>
	public enum LogMessageType : int
	{
		/// <summary> Информационное сообщение.</summary>
		Info,
		/// <summary> Сообщение об ошибке.</summary>
		Error,
		/// <summary> Предупреждающее сообщение.</summary>
		Warning,
		/// <summary> Сообщение о детектировании.</summary>
		Detection,
	}
}
