namespace Platedetector.Utils.Logging
{
    public sealed class LogItem
	{
		public LogItem(ILogMessage message)
		{
			Message = message;
		}

		public ILogMessage Message { get; }
	}
}
