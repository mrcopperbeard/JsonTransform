using JUST;

namespace JsonTransform
{
	/// <inheritdoc />
	public class JustTransformer : IJsonTransformer
	{
		/// <inheritdoc />
		public string Transform(string source, string transformation)
		{
			return JsonTransformer.Transform(transformation, source);
		}
	}
}