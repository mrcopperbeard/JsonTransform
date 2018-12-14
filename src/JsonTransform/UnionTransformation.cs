using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Add element transformation.
	/// </summary>
	internal class UnionTransformation : BaseTransformation, ITransformation
	{
		/// <summary>
		/// Merge settings.
		/// </summary>
		private static readonly JsonMergeSettings MergeSettings = new JsonMergeSettings
		{
			MergeArrayHandling = MergeArrayHandling.Union,
		};

		/// <summary>
		/// Object to add.
		/// </summary>
		private readonly JToken _argument;

		/// <inheritdoc />
		public UnionTransformation(string targetPath, JToken argument)
			: base(targetPath)
		{
			_argument = argument;
		}

		/// <inheritdoc />
		public void ApplyTo(JObject target, ITransformationContext context)
		{
			var token = target.SelectToken(TargetPath);
			switch (token)
			{
				case JArray array:
					array.Merge(_argument, MergeSettings);
					break;
				default:
					throw new NotImplementedException($"\"Add\" transformation for type {token.Type} is not implemented yet.");
			}
		}
	}
}