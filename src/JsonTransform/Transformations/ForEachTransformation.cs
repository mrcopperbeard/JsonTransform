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
			try
			{
				_transformationObject = (JObject)context.Property.Value.DeepClone();
			}
			catch (Exception)
			{
			}
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			if (_transformationObject == null)
			{
				OnErrorInternal("Unable to read foreach transformation.");

				return;
			}

			var token = target.SelectToken(Context.TargetPath);
			if (token is JArray targetArray)
			{
				for (var i = 0; i < targetArray.Count; i++)
				{
					if (targetArray[i] is JObject targetObject)
					{
						// TODO: Analyze errors.
						var transformedItem = JsonTransformer.Transform(
							targetObject,
							(JObject) _transformationObject.DeepClone(),
							context,
							(sender, args) => OnItemTransformError(i, args));

						targetObject.Replace(transformedItem);
					}
					else
					{
						OnErrorInternal($"Can not apply ForEach transformation to {Context.TargetPath}. Target item must be a JObject");
					}
				}
			}
			else
			{
				OnErrorInternal($"Can not apply ForEach transformation to {Context.TargetPath}. Target must be a JObject, but type is {token.Type}");
			}
		}

		private void OnItemTransformError(int index, TransformErrorEventArgs innerErrorArgs)
		{
			var path = $"{Context.TargetPath}[{index}].{innerErrorArgs.TargetPath}";
			OnErrorInternal(path, innerErrorArgs.Message);
		}
	}
}