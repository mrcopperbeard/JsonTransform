using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
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
			var targetToken = target.SelectToken(TargetPath);

			if (copyingToken == null)
			{
				throw new InvalidOperationException($"Unable to find node \"{_sourcePath}\" to copy from it.");
			}

			if (targetToken == null)
			{
				throw new InvalidOperationException($"Unable to find node \"{TargetPath}\" to copy there.");
			}

			targetToken.Replace(copyingToken);
		}
	}
}