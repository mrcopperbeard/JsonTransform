using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class RemoveTransformation : ITransformation
	{
		/// <inheritdoc />
		public void ApplyTo(JToken token)
		{
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