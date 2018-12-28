using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Test extensions.
	/// </summary>
	public static class TestExtensions
	{
		public static void ShouldNotContainTransformations(this JObject obj)
		{
			obj
				.ToString()
				.Should()
				.NotContain(Constants.TransformPrefix);
		}
	}
}