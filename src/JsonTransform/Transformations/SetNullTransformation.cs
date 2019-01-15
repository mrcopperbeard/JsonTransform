using Newtonsoft.Json.Linq;

namespace JsonTransform.Transformations
{
	/// <summary>
	/// Set null to current node.
	/// </summary>
	internal class SetNullTransformation : BaseTransformation
	{
		/// <inheritdoc />
		public SetNullTransformation(ITransformationCreateContext context)
			: base(context)
		{
		}

		/// <inheritdoc />
		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			var targetToken = (JValue)target.SelectToken(Context.TargetPath);

			targetToken.Value = null;
		}
	}
}