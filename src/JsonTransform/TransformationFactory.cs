using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Transformation factory.
	/// </summary>
	internal class TransformationFactory
	{
		/// <summary>
		/// Available transformations.
		/// </summary>
		private readonly IDictionary<string, TransformationActivator> _transformations;

		public TransformationFactory(IDictionary<string, TransformationActivator> transformations)
		{
			_transformations = transformations;
		}

		/// <summary>
		/// Create transformation.
		/// </summary>
		/// <param name="property">Analyzing property of transformation description.</param>
		/// <returns>Transformation or <c>null</c>.</returns>
		public ITransformation Create(JProperty property)
		{
			if (!property.Name.StartsWith(Constants.TransformPrefix) || !TryGetCodeAndName(property.Name, out var code, out var pureName))
			{
				return null;
			}

			var targetPath = property.Path.Replace(property.Name, pureName);
			var creationContext = new TransformationCreateContext
			{
				TargetPath = targetPath,
				Property = property,
			};

			if (_transformations.TryGetValue(code, out var activator))
			{
				property.Replace(new JProperty(pureName, null));

				return activator(creationContext);
			}

			return null;
		}

		/// <summary>
		/// Try get code and name from property name.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		/// <param name="code">Transformation code.</param>
		/// <param name="pureName">Pure property name.</param>
		/// <returns>Parsing result.</returns>
		internal static bool TryGetCodeAndName(string propertyName, out string code, out string pureName)
		{
			code = string.Empty;
			pureName = string.Empty;

			if (string.IsNullOrEmpty(propertyName))
			{
				return false;
			}

			var length = propertyName.Length;
			for (var i = length - 1; i > 0; i--)
			{
				if (propertyName[i] == Constants.Separator)
				{
					pureName = propertyName.Substring(i + 1);
					code = propertyName.Substring(0, i);

					return !string.IsNullOrEmpty(pureName) && !string.IsNullOrEmpty(code);
				}
			}

			return false;
		}
	}
}