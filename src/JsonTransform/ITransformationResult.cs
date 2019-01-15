using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Transformation result.
	/// </summary>
	public interface ITransformationResult
	{
		/// <summary>
		/// Is transformation performed succesfully.
		/// </summary>
		bool Success { get; }

		/// <summary>
		/// Result in JObject.
		/// </summary>
		JObject JObject { get; }

		/// <summary>
		/// Errors during validation.
		/// </summary>
		IEnumerable<string> Errors { get; }
	}
}