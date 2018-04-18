namespace PlateDetector.Logging
{
	/// <summary> Типы сообщений лога.</summary>
	public enum LogMessageType : int
	{
		/// <summary> Информационное сообщение.</summary>
		Info,
		/// <summary> Сообщение об ошибке.</summary>
		Error,
		/// <summary> Сообщение о детектировании.</summary>
		Detection,
	}
}
