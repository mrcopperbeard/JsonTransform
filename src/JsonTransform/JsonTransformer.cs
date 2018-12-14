using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	public sealed class JsonTransformer : IJsonTransformer
	{
		/// <summary>
		/// Merge settings.
		/// </summary>
		private static readonly JsonMergeSettings MergeSettings = new JsonMergeSettings
		{
			MergeArrayHandling = MergeArrayHandling.Merge,
			MergeNullValueHandling = MergeNullValueHandling.Ignore,
		};

		private readonly Stack<ITransformation> _transformations;

		public JsonTransformer()
		{
			_transformations = new Stack<ITransformation>();
		}

		/// <inheritdoc />
		public JObject Transform(JObject source, JObject transformation)
		{
			return Transform(source, transformation, null);
		}

		/// <summary>
		/// Преобразовать исходный JSON-объект при помощи указанной трансформации.
		/// </summary>
		/// <param name="source">Исходный JSON-объект.</param>
		/// <param name="transformation">Объект с трансформацией.</param>
		/// <param name="transformContext">Контекст трансформации.</param>
		/// <returns>Трансформированный JSON-объект.</returns>
		internal JObject Transform(JObject source, JObject transformation, ITransformationContext transformContext)
		{
			var resultObject = (JObject)source.DeepClone();
			transformContext = transformContext ?? new TransformationContext
			{
				Source = source,
			};

			Walk(transformation);

			resultObject.Merge(transformation, MergeSettings);

			while(_transformations.Count > 0)
			{
				_transformations.Pop().ApplyTo(resultObject, transformContext);
			}

			return resultObject;
		}

		/// <inheritdoc />
		public JObject Transform(string source, string transformDescription)
		{
			var sourceObject = JObject.Parse(source);
			var transformationObject = JObject.Parse(transformDescription);

			return Transform(sourceObject, transformationObject);
		}

		private void Walk(JToken token)
		{
			switch (token.Type)
			{
				case JTokenType.Object:
					var properties = ((JObject) token).Properties().ToArray();
					foreach (var property in properties)
					{
						Walk(property);
					}

					break;
				case JTokenType.Array:
					foreach (var item in (JArray)token)
					{
						Walk(item);
					}
					break;
				case JTokenType.Property:
					var prop = (JProperty)token;
					var command = TransformationFactory.Create(prop);
					if (command != null)
					{
						_transformations.Push(command);

						var cleanName = prop.Name.Split(TransformationFactory.Separator).Last();
						prop.Replace(new JProperty(cleanName, null));

						return;
					}

					Walk(prop.Value);
					break;
			}
		}
	}
}