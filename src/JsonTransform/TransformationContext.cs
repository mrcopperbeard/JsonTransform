using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class TransformationContext : ITransformationContext
	{
		/// <inheritdoc />
		public JToken Source { get; set; }
	}
}