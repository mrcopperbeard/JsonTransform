using System;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	internal class TransformationFactory
	{
		private static readonly Regex TransformationPatternRegex = new Regex($"{TransformationPrefix}{Separator}[a-zA-Z]+{Separator}");

		/// <summary>
		/// Префикс трансформаций.
		/// </summary>
		internal const string TransformationPrefix = "transform";

		/// <summary>
		/// Знак разделитель.
		/// </summary>
		internal const char Separator = '-';

		public static ITransformation Create(JProperty property)
		{
			var propertyNameParts = property.Name.Split(Separator);
			if (propertyNameParts.Length < 3 || propertyNameParts[0] != TransformationPrefix)
			{
				return null;
			}

			var targetPath = TransformationPatternRegex.Replace(property.Path, string.Empty);

			switch (propertyNameParts[1])
			{
				case "remove":
					return new RemoveTransformation(targetPath);
				case "copy":
					var sourcePath = property.Value.Value<string>();

					return new CopyTransformation(sourcePath, targetPath);
				case "foreach":
					return new ForEachTransformation(targetPath, (JObject)property.Value);
				case "union":
					return new UnionTransformation(targetPath, property.Value);
				case "setnull":
					return new SetNullTransformation(targetPath);
				default:
					return null;
			}
		}
	}
}