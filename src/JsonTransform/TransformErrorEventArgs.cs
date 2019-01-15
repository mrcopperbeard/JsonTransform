using System;

namespace JsonTransform
{
	/// <inheritdoc />
	public class TransformErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Target path.
		/// </summary>
		public string TargetPath { get; set; }

		/// <summary>
		/// Error message.
		/// </summary>
		public string Message { get; set; }
	}
}