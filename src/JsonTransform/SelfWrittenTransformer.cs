using System;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	public class SelfWrittenTransformer : IJsonTransformer
	{
		private readonly TransformationFactory _transformationFactory;

		public SelfWrittenTransformer(TransformationFactory transformationFactory)
		{
			_transformationFactory = transformationFactory;
		}

		/// <inheritdoc />
		public string Transform(string source, string transformation)
		{
			var resultObject = JObject.Parse(source);

			Walk(resultObject, JObject.Parse(transformation), new StringBuilder());

			return resultObject.ToString();
		}

		private void Walk(JObject result, JToken token, StringBuilder path)
		{
			switch (token.Type)
			{
				case JTokenType.Object:
					foreach (var property in ((JObject)token).Properties())
					{
						Walk(result, property, path.AppendNode(property.Name));
					}
					break;
				case JTokenType.Array:
					var array = (JArray)token;
					for (var i = 0; i < array.Count; i++)
					{
						Walk(result, array[i], path.Append($"[{i}]"));
					}
					break;
				case JTokenType.Property:
					var prop = (JProperty)token;
					Walk(result, prop.Value, path);
					break;
				case JTokenType.String:
					var value = token.Value<string>();
					var command = _transformationFactory.Create(value);

					if (command == null)
					{
						SetValue(result, value, path.ToString());
					}
					else
					{
						command.ApplyTo(result.SelectToken(path.ToString()));
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
					SetValue(result, ((JValue)token).Value, path.ToString());
					break;
				default:
					throw new NotImplementedException($"{path}: {Enum.GetName(typeof(JTokenType), token.Type)} type is not implemented yet.");
			}
		}

		private static void SetValue(JToken source, object value, string path)
		{
			try
			{
				((JValue)source.SelectToken(path)).Value = value;
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Error settings value {value} to {path}. {e.Message}", e);
			}
		}
	}
}