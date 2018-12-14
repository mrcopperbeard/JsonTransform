using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Delete node transformation.
	/// </summary>
	internal class RemoveTransformation : BaseTransformation, ITransformation
	{
		/// <inheritdoc />
		public RemoveTransformation(string targetPath)
			: base(targetPath)
		{
		}

		/// <inheritdoc />
		public void ApplyTo(JObject target, ITransformationContext context)
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