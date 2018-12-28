using FluentAssertions;
using NUnit.Framework;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Tests of parsing commands.
	/// </summary>
	[TestFixture]
	public class CommandParsingTests
	{
		/// <summary>
		/// Getting property and code should return expected values or empty strings.
		/// </summary>
		/// <param name="propertyName">Analyzing property name.</param>
		/// <param name="expectedResult">Expected parsing result.</param>
		/// <param name="expectedPropertyName">Expected property name.</param>
		[TestCase("transform-remove-someProp", true, "someProp")]
		[TestCase("transform-remove--someProp", true, "someProp")]
		[TestCase("transform-remove-", false, "")]
		[TestCase("-someProp", false, "")]
		[TestCase("someProp", false, "")]
		[TestCase("-", false, "")]
		[TestCase("", false, "")]
		[TestCase(null, false, "")]
		public void TryGetCodeAndName_ShouldGetExpected(
			string propertyName,
			bool expectedResult,
			string expectedPropertyName)
		{
			// arrange
			var result = TransformationFactory.TryGetCodeAndName(propertyName, out _, out var name);

			// assert
			result.Should().Be(expectedResult);
			name.Should().Be(expectedPropertyName);
		}
	}
}