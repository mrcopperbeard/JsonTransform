using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class TransformationCreateContext : ITransformationCreateContext
	{
		/// <inheritdoc />
		public string TargetPath { get; set; }

		/// <inheritdoc />
		public JProperty Property { get; set; }
	}
}