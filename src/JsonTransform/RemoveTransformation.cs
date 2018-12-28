using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Delete node transformation.
	/// </summary>
	internal class RemoveTransformation : BaseTransformation
	{
		/// <inheritdoc />
		public RemoveTransformation(ITransformationCreateContext context)
			: base(context)
		{
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			var token = target.SelectToken(TargetPath);
			if (token == null)
			{
				return;
			}

			try
			{
				token.Parent.Remove();
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Error while removing {token.Path}. {e.Message}", e);
			}
		}
	}
}