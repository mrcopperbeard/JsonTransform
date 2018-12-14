using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Static JSON transformer.
	/// </summary>
	public static class JsonTransformer
	{
		/// <summary>
		/// Merge settings.
		/// </summary>
		private static readonly JsonMergeSettings MergeSettings = new JsonMergeSettings
		{
			MergeArrayHandling = MergeArrayHandling.Merge,
			MergeNullValueHandling = MergeNullValueHandling.Ignore,
		};

		/// <summary>
		/// Transform specified JSON-object with specified transformation.
		/// </summary>
		/// <param name="source">Source JSON-object.</param>
		/// <param name="transformation">Transformation meta object.</param>
		/// <returns>Transformed JSON-object.</returns>
		public static JObject Transform(JObject source, JObject transformation)
		{
			return Transform(source, transformation, null);
		}

		/// <summary>
		/// Transform specified string with JSON with specified transformation.
		/// </summary>
		/// <param name="source">Source JSON string.</param>
		/// <param name="transformDescription">String with transformation meta.</param>
		/// <returns>Transformed JSON-string.</returns>
		public static JObject Transform(string source, string transformDescription)
		{
			var sourceObject = JObject.Parse(source);
			var transformationObject = JObject.Parse(transformDescription);

			return Transform(sourceObject, transformationObject);
		}

		/// <summary>
		/// Преобразовать исходный JSON-объект при помощи указанной трансформации.
		/// </summary>
		/// <param name="source">Исходный JSON-объект.</param>
		/// <param name="transformation">Объект с трансформацией.</param>
		/// <param name="transformContext">Контекст трансформации.</param>
		/// <returns>Трансформированный JSON-объект.</returns>
		internal static JObject Transform(JObject source, JObject transformation, ITransformationContext transformContext)
		{
			var resultObject = (JObject)source.DeepClone();
			var transformations = new Stack<ITransformation>();
			transformContext = transformContext ?? new TransformationContext
			{
				Source = source,
			};

			Walk(transformation, transformations);

			resultObject.Merge(transformation, MergeSettings);

			while(transformations.Count > 0)
			{
				transformations.Pop().ApplyTo(resultObject, transformContext);
			}

			return resultObject;
		}

		/// <summary>
		/// Walk around transformation object, and collect all transformations.
		/// </summary>
		/// <param name="token">Transformation object.</param>
		/// <param name="transformations">Stack with transformations.</param>
		private static void Walk(JToken token, Stack<ITransformation> transformations)
		{
			switch (token.Type)
			{
				case JTokenType.Object:
					var properties = ((JObject) token).Properties().ToArray();
					foreach (var property in properties)
					{
						Walk(property, transformations);
					}

					break;
				case JTokenType.Array:
					foreach (var item in (JArray)token)
					{
						Walk(item, transformations);
					}
					break;
				case JTokenType.Property:
					var prop = (JProperty)token;
					var command = TransformationFactory.Create(prop);
					if (command != null)
					{
						transformations.Push(command);

						var cleanName = prop.Name.Split(TransformationFactory.Separator).Last();
						prop.Replace(new JProperty(cleanName, null));

						return;
					}

					Walk(prop.Value, transformations);
					break;
			}
		}
	}
}