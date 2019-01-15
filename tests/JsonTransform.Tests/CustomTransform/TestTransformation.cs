using Newtonsoft.Json.Linq;

namespace JsonTransform.Tests.CustomTransform
{
	internal class TestTransformation : BaseTransformation
	{
		public const string Expected = nameof(Expected);

		public TestTransformation(ITransformationCreateContext context) : base(context)
		{
		}

		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			var targetToken = (JValue)target.SelectToken(Context.TargetPath);

			targetToken.Value = Expected;
		}

	}
}