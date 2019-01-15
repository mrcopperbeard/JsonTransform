using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class TransformationResult : ITransformationResult
	{
		public TransformationResult(JObject obj, IEnumerable<string> errors)
		{
			JObject = obj;
			Errors = errors ?? Enumerable.Empty<string>();
			Success = !Errors.Any();
		}

		/// <inheritdoc />
		public bool Success { get; }

		/// <inheritdoc />
		public JObject JObject { get; }

		/// <inheritdoc />
		public IEnumerable<string> Errors { get; }

		/// <inheritdoc />
		public override string ToString() => JObject.ToString();
	}
}