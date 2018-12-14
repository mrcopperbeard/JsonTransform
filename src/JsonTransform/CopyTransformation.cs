using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Copy node transformation.
	/// </summary>
	internal class CopyTransformation : BaseTransformation, ITransformation
	{
		/// <summary>
		/// Путь, откуда нужно скопировать узел.
		/// </summary>
		private readonly string _sourcePath;

		/// <inheritdoc />
		public CopyTransformation(string sourcePath, string targetPath)
			: base(targetPath)
		{
			_sourcePath = sourcePath;
		}

		/// <inheritdoc />
		public void ApplyTo(JObject target, ITransformationContext context)
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