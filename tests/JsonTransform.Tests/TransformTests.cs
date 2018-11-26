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
		[TestCase(JsonTemplates.SetConstString.EmptySource, JsonTemplates.SetConstString.Transformation, "one")]
		public void Transform_SetConstant_ShouldWork<TExpected>(string source, string transformation, TExpected expected)
		{
			// act
			var resultString = _transformer.Transform(source, transformation);
			var resultObject = JObject.Parse(resultString);

			// assert
			resultObject["first"]["value"].Value<TExpected>().Should().Be(expected);
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
	}
}
