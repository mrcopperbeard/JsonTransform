using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform.Transformations
{
	/// <summary>
	/// Copy node transformation.
	/// </summary>
	internal class CopyTransformation : BaseTransformation
	{
		/// <summary>
		/// Path to copying node in source object.
		/// </summary>
		private readonly string _sourcePath;

		/// <inheritdoc />
		public CopyTransformation(ITransformationCreateContext context)
			: base(context)
		{
			_sourcePath = context.Property.Value.Value<string>();
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			var copyingToken = context.Source.SelectToken(_sourcePath ?? string.Empty);
			var targetToken = target.SelectToken(Context.TargetPath);

			if (copyingToken == null)
			{
				OnErrorInternal($"Unable to find node \"{_sourcePath}\" to copy from it.");

				return;
			}

			if (targetToken == null)
			{
				OnErrorInternal($"Unable to find node \"{Context.TargetPath}\" to copy there.");

				return;
			}

			targetToken.Replace(copyingToken);
		}
	}
}