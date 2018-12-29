using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform.Transformations
{
	/// <summary>
	/// Implement another transformation for each element of array.
	/// </summary>
	internal class ForEachTransformation : BaseTransformation
	{
		/// <summary>
		/// Transformation object.
		/// </summary>
		private readonly JObject _transformationObject;

		/// <inheritdoc />
		public ForEachTransformation(ITransformationCreateContext context)
			: base(context)
		{
			_transformationObject = (JObject)context.Property.Value.DeepClone();
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
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