using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonTransform.Tests.CustomTransform
{
	/// <summary>
	/// Tests of using custom transformations functionality.
	/// </summary>
	[TestFixture]
	public class CustomTransformationsTests
	{
		/// <summary>
		/// One time setup.
		/// </summary>
		[OneTimeSetUp]
		public void Setup()
		{
			JsonTransformer.RegisterTransformation("test", ctx => new TestTransformation(ctx));
		}

		[Test]
		public void RegisterTransformation_SeveralTimes_ShouldNotThrow()
		{
			JsonTransformer.RegisterTransformation("test", ctx => new TestTransformation(ctx));
		}

		/// <summary>
		/// Applying <see cref="TestTransformation"/> test.
		/// </summary>
		[Test]
		public void ApplyCustomTransformTest()
		{
			// act
			var resultObject = JsonTransformer.Transform(JsonTemplates.Custom.Source, JsonTemplates.Custom.Transformation);

			// assert
			resultObject["root"]["value"].Value<string>().Should().Be(TestTransformation.Expected);
			resultObject.ShouldNotContainTransformations();
		}
	}
}