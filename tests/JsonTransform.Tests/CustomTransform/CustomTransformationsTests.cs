using System;
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
			var result = JsonTransformer.Transform(JsonTemplates.Custom.Source, JsonTemplates.Custom.Transformation);

			// assert
			result.Success.Should().BeTrue(string.Join(Environment.NewLine, result.Errors));
			result.JObject["root"]["value"].Value<string>().Should().Be(TestTransformation.Expected);
			result.JObject.ShouldNotContainTransformations();
		}
	}
}