using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	public class CopyTransformation : ITransformation
	{
		/// <summary>
		/// Путь, откуда нужно скопировать узел.
		/// </summary>
		private readonly string _sourcePath;

		/// <summary>
		/// Путь, куда нужно скопировать узел.
		/// </summary>
		private readonly string _targetPath;

		public CopyTransformation(string sourcePath, string targetPath)
		{
			_sourcePath = sourcePath;
			_targetPath = targetPath;
		}

		/// <inheritdoc />
		public void ApplyTo(JObject obj, ITransformationContext context)
		{
			var copyingToken = context.Source.SelectToken(_sourcePath);
			var targetToken = obj.SelectToken(_targetPath);

			if (copyingToken == null)
			{
				throw new InvalidOperationException($"Unable to find node \"{_targetPath}\" to copy from it.");
			}

			if (targetToken == null)
			{
				throw new InvalidOperationException($"Unable to find node \"{_targetPath}\" to copy there.");
			}

			targetToken.Replace(copyingToken);
		}
	}
}