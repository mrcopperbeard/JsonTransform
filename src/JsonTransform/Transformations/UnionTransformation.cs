using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform.Transformations
{
	/// <summary>
	/// Add element transformation.
	/// </summary>
	internal class UnionTransformation : BaseTransformation
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
		public UnionTransformation(ITransformationCreateContext context)
			: base(context)
		{
			_argument = context.Property.Value;
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
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