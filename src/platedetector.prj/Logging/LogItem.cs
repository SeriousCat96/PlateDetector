using PlateDetector.Logging;

namespace PlateDetector.Logging
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
