using System;

using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Тесты для <see cref="IJsonTransformer"/>.
	/// </summary>
	[TestFixture]
	public class TransformTests
	{
		/// <summary>
		/// Тестируемый объект.
		/// </summary>
		private IJsonTransformer _transformer;

		[SetUp]
		public void Setup()
		{
			_transformer = new SelfWrittenTransformer(new TransformationFactory());
		}

		[TestCase(JsonTemplates.SetConstBool.Source, JsonTemplates.SetConstBool.Transformation, true)]
		[TestCase(JsonTemplates.SetConstString.Source, JsonTemplates.SetConstString.Transformation, "one")]
		[TestCase(JsonTemplates.EmptySource, JsonTemplates.SetConstString.Transformation, "one")]
		public void Transform_SetConstant_ShouldWork<TExpected>(string source, string transformation, TExpected expected)
		{
			// act
			var resultString = _transformer.Transform(source, transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["first"]["value"].Value<TExpected>().Should().Be(expected);
		}

		[TestCase(JsonTemplates.EmptySource)]
		[TestCase(JsonTemplates.SetConstToArray.Source)]
		public void Transform_SetConstantInArray_ShouldWork(string source)
		{
			// act
			var resultString = _transformer.Transform(source, JsonTemplates.SetConstToArray.Transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["array"][0]["value"].Value<string>().Should().Be("one");
			resultObject["array"][1]["value"].Value<string>().Should().Be("two");
		}

		[Test]
		public void Transform_SetNull_ShouldWork()
		{
			// act
			var resultString = _transformer.Transform(JsonTemplates.SetNull.Source, JsonTemplates.SetNull.Transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["first"]["value"].Value<string>().Should().BeNull();
			resultObject["first"]["reallyNullValue"].Value<string>().Should().BeNull();
		}

		[Test]
		public void Remove_FirstLevel_ShouldWork()
		{
			// act
			var resultString = _transformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveFirstLevel);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject.Property("firstLevel").Should().BeNull();
			resultObject["firstLevel1"].Value<bool>().Should().BeTrue();
		}

		[Test]
		public void Remove_SecondLevel_ShouldWork()
		{
			// act
			var resultString = _transformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveSecondLevel);
			var resultObject = JObject.Parse(resultString);

			// assert
			((JObject)resultObject["firstLevel"]).Property("secondLevel").Should().BeNull();
			resultObject["firstLevel"]["secondLevel1"].Value<bool>().Should().BeTrue();
		}

		[Test]
		public void Copy_ExistingObject_ShouldCopyAndLeaveSourceUntouched()
		{
			// act
			var resultString = _transformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.Transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["target"]["value"].Value<bool>().Should().BeTrue();
			resultObject["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
		}

		[Test]
		public void Copy_ExistingString_ShouldCopyAndLeaveSourceUntouched()
		{
			// arrange
			const string Expected = "test";

			// act
			var resultString = _transformer.Transform(JsonTemplates.CopyNode.SourceWithString, JsonTemplates.CopyNode.Transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["target"].Value<string>().Should().Be(Expected);
			resultObject["source"]["inner"].Value<string>().Should().Be(Expected);
		}

		[Test]
		public void CopyRoot_ShouldCopyRoot()
		{
			// act
			var resultString = _transformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.CopyRootTransformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["target"]["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
		}

		[Test]
		public void Copy_WithInvalidPath_ShouldThrowCorrectException()
		{
			_transformer
				.Invoking(_ => _.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.TransformationWitInvalidPath))
				.Should()
				.ThrowExactly<ArgumentException>()
				.Which
				.Message
				.Should()
				.Contain(InternalConstants.AvailablePathSymbols);
		}
	}
}
