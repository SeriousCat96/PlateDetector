﻿namespace PlateDetector.Detection
{
	public sealed class NdArray<T> where T: struct
	{
		public NdArray(T[,,,] value)
		{
			Value = value;
		}

		public T[,,,] Value { get; set; }
	}
}