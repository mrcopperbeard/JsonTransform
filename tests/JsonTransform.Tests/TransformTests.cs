using System;

using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Тесты для <see cref="JsonTransformer"/>.
	/// </summary>
	[TestFixture]
	public class TransformTests
	{
		[TestCase(JsonTemplates.SetConstBool.Source, JsonTemplates.SetConstBool.Transformation, true)]
		[TestCase(JsonTemplates.SetConstString.Source, JsonTemplates.SetConstString.Transformation, "one")]
		[TestCase(JsonTemplates.EmptySource, JsonTemplates.SetConstString.Transformation, "one")]
		public void Transform_SetConstant_ShouldWork<TExpected>(string source, string transformation, TExpected expected)
		{
			// act
			var result = JsonTransformer.Transform(source, transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["first"]["value"].Value<TExpected>().Should().Be(expected);
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetConstantInEmptyArray_ShouldWork()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.EmptySource, JsonTemplates.SetConstToArray.Transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["array"][0]["value"].Value<string>().Should().Be("one");
			result.JObject["array"][1]["value"].Value<string>().Should().Be("two");
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetConstantInArray_ShouldWork()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.SetConstToArray.Source, JsonTemplates.SetConstToArray.Transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["array"][0]["value"].Value<string>().Should().Be("one");
			result.JObject["array"][0]["otherValue"].Value<int>().Should().Be(2);
			result.JObject["array"][0]["inner"]["innerValue1"].Value<string>().Should().Be("inner value 1");
			result.JObject["array"][0]["inner"]["innerValue2"].Value<string>().Should().Be("inner value 2");
			result.JObject["array"][1]["value"].Value<string>().Should().Be("two");
			result.JObject["array"][1]["inner"]["innerValue3"].Value<string>().Should().Be("inner value 3");
			result.JObject["array"][1]["inner"]["innerValue4"].Value<string>().Should().Be("inner value 4");
			result.JObject["array"][1]["inner"]["odd"].Value<string>().Should().Be("some odd property");

			// Because that way works MergeArrayHandling.Merge.
			result.JObject["array"][0]["innerArray"][0].Value<string>().Should().Be("Second");

			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Transform_SetNull_ShouldWork()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.SetNull.Source, JsonTemplates.SetNull.Transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["first"]["value"].Value<string>().Should().BeNull();
			result.JObject["first"]["reallyNullValue"].Value<string>().Should().BeNull();
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Remove_FirstLevel_ShouldWork()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveFirstLevel);

			// assert
			result.Success.Should().BeTrue();
			result.JObject.Property("firstLevel").Should().BeNull();
			result.JObject["firstLevel1"].Value<bool>().Should().BeTrue();
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Remove_SecondLevel_ShouldWork()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.RemoveNode.Source, JsonTemplates.RemoveNode.RemoveSecondLevel);

			// assert
			result.Success.Should().BeTrue();
			((JObject)result.JObject["firstLevel"]).Property("secondLevel").Should().BeNull();
			result.JObject["firstLevel"]["secondLevel1"].Value<bool>().Should().BeTrue();
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void Copy_ExistingObject_ShouldCopyAndLeaveSourceUntouched()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.Transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["target"]["value"].Value<bool>().Should().BeTrue();
			result.JObject["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
			result.JObject.ShouldNotContainTransformations();
		}

		[TestCase(JsonTemplates.CopyNode.SourceWithString)]
		[TestCase(JsonTemplates.CopyNode.SourceWithoutTarget)]
		public void Copy_ExistingString_ShouldCopyAndLeaveSourceUntouched(string source)
		{
			// arrange
			const string Expected = "test";

			// act
			var result = JsonTransformer.Transform(source, JsonTemplates.CopyNode.Transformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["target"].Value<string>().Should().Be(Expected);
			result.JObject["source"]["inner"].Value<string>().Should().Be(Expected);
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void CopyRoot_ShouldCopyRoot()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.CopyNode.SourceWithObject, JsonTemplates.CopyNode.CopyRootTransformation);

			// assert
			result.Success.Should().BeTrue();
			result.JObject["target"]["source"]["inner"]["value"].Value<bool>().Should().BeTrue();
			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void ForEach_ShouldApplyTransformation_ToEachArrayItem()
		{
			// act
			var result = JsonTransformer.Transform(JsonTemplates.ForEach.Source, JsonTemplates.ForEach.Transformation);

			// assert
			result.Success.Should().BeTrue();
			((JArray)result.JObject["array"]).Count.Should().Be(3);
			result.JObject.SelectToken("array[0].removeMe").Should().BeNull();
			result.JObject.SelectToken("array[1].removeMe").Should().BeNull();
			result.JObject.SelectToken("array[2].removeMe").Should().BeNull();

			result.JObject["array"][0]["target"].Value<string>().Should().Be("Expected");
			result.JObject["array"][1]["target"].Value<string>().Should().Be("Expected");
			result.JObject["array"][2]["target"].Value<string>().Should().Be("Expected");

			result.JObject.ShouldNotContainTransformations();
		}

		[Test]
		public void ComplexTransformationTest()
		{
			// arrange
			var expectedString = Resources.ComplexTransform.Expected;
			var expectedObject = JObject.Parse(expectedString);

			// act
			var result = JsonTransformer.Transform(Resources.ComplexTransform.Source, Resources.ComplexTransform.Transformation);
			var resultObject = result.JObject;

			// assert
			result.Success.Should().BeTrue();
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
