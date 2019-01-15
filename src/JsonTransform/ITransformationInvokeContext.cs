using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Transformation invoke context.
	/// </summary>
	public interface ITransformationInvokeContext
	{
		/// <summary>
		/// Source object.
		/// </summary>
		JToken Source { get; }
	}
}