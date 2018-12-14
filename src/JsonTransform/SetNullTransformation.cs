using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Set null to current node.
	/// </summary>
	internal class SetNullTransformation : BaseTransformation, ITransformation
	{
		/// <inheritdoc />
		public SetNullTransformation(string targetPath)
			: base(targetPath)
		{
		}

		/// <inheritdoc />
		public void ApplyTo(JObject target, ITransformationContext context)
		{
			var targetToken = (JValue)target.SelectToken(TargetPath);

			targetToken.Value = null;
		}
	}
}