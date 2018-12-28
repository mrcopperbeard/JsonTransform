using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class TransformationInvokeContext : ITransformationInvokeContext
	{
		/// <inheritdoc />
		public JToken Source { get; set; }
	}
}