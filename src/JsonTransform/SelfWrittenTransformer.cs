using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	public class SelfWrittenTransformer : IJsonTransformer
	{
		private readonly TransformationFactory _transformationFactory;

		private readonly Stack<ITransformation> _transformations;

		public SelfWrittenTransformer(TransformationFactory transformationFactory)
		{
			_transformationFactory = transformationFactory;
			_transformations = new Stack<ITransformation>();
		}

		/// <inheritdoc />
		public string Transform(string source, string transformDescription)
		{
			var resultObject = JObject.Parse(source);
			var sourceObject = resultObject.DeepClone();
			var transformationObject = JObject.Parse(transformDescription);
			var transformContext = new TransformationContext
			{
				Source = sourceObject,
			};

			Walk(transformationObject, new StringBuilder());

			resultObject.Merge(
				transformationObject,
				new JsonMergeSettings
				{
					MergeArrayHandling = MergeArrayHandling.Replace,
					MergeNullValueHandling = MergeNullValueHandling.Merge,
				});

			var stackSize = _transformations.Count;
			for (var i = 0; i < stackSize; i++)
			{
				_transformations.Pop().ApplyTo(resultObject, transformContext);
			}

			return resultObject.ToString();
		}

		private void Walk(JToken token, StringBuilder pathBuilder)
		{
			switch (token.Type)
			{
				case JTokenType.Object:
					foreach (var property in ((JObject)token).Properties())
					{
						Walk(property, pathBuilder.AppendNode(property.Name));
					}
					break;
				case JTokenType.Array:
					var array = (JArray)token;
					for (var i = 0; i < array.Count; i++)
					{
						Walk(array[i], pathBuilder.Append($"[{i}]"));
					}
					break;
				case JTokenType.Property:
					var prop = (JProperty)token;
					Walk(prop.Value, pathBuilder);
					break;
				case JTokenType.String:
					var command = _transformationFactory.Create(token.Value<string>(), pathBuilder.ToString());
					if (command != null)
					{
						_transformations.Push(command);
					}

					break;
				case JTokenType.Integer:
				case JTokenType.Float:
				case JTokenType.Boolean:
				case JTokenType.Null:
				case JTokenType.Undefined:
				case JTokenType.Date:
				case JTokenType.Raw:
				case JTokenType.Bytes:
				case JTokenType.Guid:
				case JTokenType.Uri:
				case JTokenType.TimeSpan:
					break;
				default:
					throw new NotImplementedException($"{pathBuilder}: {Enum.GetName(typeof(JTokenType), token.Type)} type is not implemented yet.");
			}
		}
	}
}