using System;

using FluentAssertions;
using JsonTransform.Tests.Properties;
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
			_transformer = new JsonTransformer();
		}

		[TestCase(JsonTemplates.SetConstBool.Source, JsonTemplates.SetConstBool.Transformation, true)]
		[TestCase(JsonTemplates.SetConstString.Source, JsonTemplates.SetConstString.Transformation, "one")]
		[TestCase(JsonTemplates.EmptySource, JsonTemplates.SetConstString.Transformation, "one")]
		public void Transform_SetConstant_ShouldWork<TExpected>(string source, string transformation, TExpected expected)
		{
			// act
			var resultObject = _transformer.Transform(source, transformation);

			// assert
			resultObject["first"]["value"].Value<TExpected>().Should().Be(expected);
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetConstantInEmptyArray_ShouldWork()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.EmptySource, JsonTemplates.SetConstToArray.Transformation);

			// assert
			resultObject["array"][0]["value"].Value<string>().Should().Be("one");
			resultObject["array"][1]["value"].Value<string>().Should().Be("two");
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetConstantInArray_ShouldWork()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.SetConstToArray.Source, JsonTemplates.SetConstToArray.Transformation);

			// assert
			resultObject["array"][0]["value"].Value<string>().Should().Be("one");
			resultObject["array"][0]["otherValue"].Value<int>().Should().Be(2);
			resultObject["array"][0]["inner"]["innerValue1"].Value<string>().Should().Be("inner value 1");
			resultObject["array"][0]["inner"]["innerValue2"].Value<string>().Should().Be("inner value 2");
			resultObject["array"][1]["value"].Value<string>().Should().Be("two");
			resultObject["array"][1]["inner"]["innerValue3"].Value<string>().Should().Be("inner value 3");
			resultObject["array"][1]["inner"]["innerValue4"].Value<string>().Should().Be("inner value 4");
			resultObject["array"][1]["inner"]["odd"].Value<string>().Should().Be("some odd property");

			// Because that way works MergeArrayHandling.Merge.
			resultObject["array"][0]["innerArray"][0].Value<string>().Should().Be("Second");

			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetNull_ShouldWork()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.SetNull.Source, JsonTemplates.SetNull.Transformation);

			// assert
			resultObject["first"]["value"].Value<string>().Should().BeNull();
			resultObject["first"]["reallyNullValue"].Value<string>().Should().BeNull();
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Remove_FirstLevel_ShouldWork()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveFirstLevel);

			// assert
			resultObject.Property("firstLevel").Should().BeNull();
			resultObject["firstLevel1"].Value<bool>().Should().BeTrue();
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Remove_SecondLevel_ShouldWork()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveSecondLevel);

			// assert
			((JObject)resultObject["firstLevel"]).Property("secondLevel").Should().BeNull();
			resultObject["firstLevel"]["secondLevel1"].Value<bool>().Should().BeTrue();
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Copy_ExistingObject_ShouldCopyAndLeaveSourceUntouched()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.Transformation);

			// assert
			resultObject["target"]["value"].Value<bool>().Should().BeTrue();
			resultObject["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
			resultObject.ShouldNotContainTransformations();
		}

		[TestCase(JsonTemplates.CopyNode.SourceWithString)]
		[TestCase(JsonTemplates.CopyNode.SourceWithoutTarget)]
		public void Copy_ExistingString_ShouldCopyAndLeaveSourceUntouched(string source)
		{
			// arrange
			const string Expected = "test";

			// act
			var resultObject = _transformer.Transform(source, JsonTemplates.CopyNode.Transformation);

			// assert
			resultObject["target"].Value<string>().Should().Be(Expected);
			resultObject["source"]["inner"].Value<string>().Should().Be(Expected);
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void CopyRoot_ShouldCopyRoot()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.CopyRootTransformation);

			// assert
			resultObject["target"]["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void ForEach_ShouldApplyTransformation_ToEachArrayItem()
		{
			// act
			var resultObject = _transformer.Transform(JsonTemplates.ForEach.Source, JsonTemplates.ForEach.Transformation);

			Console.Out.WriteLine(resultObject.ToString());

			// assert
			((JArray)resultObject["array"]).Count.Should().Be(3);
			resultObject.SelectToken("array[0].removeMe").Should().BeNull();
			resultObject.SelectToken("array[1].removeMe").Should().BeNull();
			resultObject.SelectToken("array[2].removeMe").Should().BeNull();

			resultObject["array"][0]["target"].Value<string>().Should().Be("Expected");
			resultObject["array"][1]["target"].Value<string>().Should().Be("Expected");
			resultObject["array"][2]["target"].Value<string>().Should().Be("Expected");

			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void ComplexTransformationTest()
		{
			// arrange
			var expectedString = Resources.ComplexTransformation_Expected;
			var expectedObject = JObject.Parse(expectedString);

			// act
			var resultObject = _transformer.Transform(Resources.ComplexTransformation_Source, Resources.ComplexTransformation_Transformation);

			// assert
			JToken.DeepEquals(resultObject, expectedObject).Should().BeTrue($"Expected to be {expectedString} but found {resultObject}");
			resultObject.ShouldNotContainTransformations();
		}

		[Test]
		public void DeepCloneTest()
		{
			// arrange
			var source = JObject.FromObject(new
			{
				root = new
				{
					inner = new
					{
						deeper = "value",
					},
				},
			});

			// act
			var result = source["root"].DeepClone();

			// assert
			result["root"].Should().BeNull();
			result["inner"]["deeper"].Value<string>().Should().Be("value");
		}
	}
}
