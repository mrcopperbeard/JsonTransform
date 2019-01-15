using FluentAssertions;
using NUnit.Framework;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Error handling tests.
	/// </summary>
	[TestFixture]
	public class ErrorHandlingTests
	{
		/// <summary>
		/// Transform invalid JSON should have errors.
		/// </summary>
		[Test]
		public void Transform_InvalidJson_ShouldHaveErrors()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.Invalid.Source, JsonTemplates.Invalid.Transformation);

			// arrange
			result.Success.Should().BeFalse();
			result.Errors.Should().HaveCount(2);
		}

		/// <summary>
		/// Transform invalid JSON should have errors.
		/// </summary>
		[Test]
		public void Transform_JsonWithInvalidForeachTransformation_ShouldHaveErrors()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.InvalidForEach.Source, JsonTemplates.InvalidForEach.Transformation);

			// arrange
			result.Success.Should().BeFalse();
			result.Errors.Should().HaveCount(3);
		}
	}
}