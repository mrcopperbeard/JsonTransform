using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Implement another transformation for each element of array.
	/// </summary>
	internal class ForEachTransformation : BaseTransformation, ITransformation
	{
		/// <summary>
		/// Transformation object.
		/// </summary>
		private readonly JObject _transformationObject;

		/// <inheritdoc />
		public ForEachTransformation(string targetPath, JObject transformationObject)
			: base(targetPath)
		{
			_transformationObject = (JObject)transformationObject.DeepClone();
		}

		public void ApplyTo(JObject target, ITransformationContext context)
		{
			var token = target.SelectToken(TargetPath);
			if (token is JArray targetArray)
			{
				for (var i = 0; i < targetArray.Count; i++)
				{
					if (targetArray[i] is JObject targetObject)
					{
						targetObject.Replace(JsonTransformer.Transform(targetObject, (JObject)_transformationObject.DeepClone(), context));
					}
					else
					{
						throw new InvalidOperationException($"Can not apply ForEach transformation to {TargetPath}. Target item must be a JObject");
					}
				}
			}
			else
			{
				throw new InvalidOperationException($"Can not apply ForEach transformation to {TargetPath}. Target must be a JObject, but type is {token.Type}");
			}
		}
	}
}