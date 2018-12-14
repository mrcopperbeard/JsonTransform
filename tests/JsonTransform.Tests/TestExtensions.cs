using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Test extensions.
	/// </summary>
	public static class TestExtensions
	{
		/// <summary>
		/// Transformation pattern.
		/// </summary>
		private static readonly string TransformPattern = $"{TransformationFactory.TransformationPrefix}{TransformationFactory.Separator}";

		public static void ShouldNotContainTransformations(this JObject obj)
		{
			obj
				.ToString()
				.Should()
				.NotContain(TransformPattern);
		}
	}
}