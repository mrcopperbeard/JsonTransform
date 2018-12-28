using Newtonsoft.Json.Linq;

namespace JsonTransform.Tests.CustomTransform
{
	public class TestTransformation : BaseTransformation
	{
		public const string Expected = nameof(Expected);

		public TestTransformation(ITransformationCreateContext context) : base(context)
		{
		}

		public override void ApplyTo(JObject target, ITransformationInvokeContext context)
		{
			var targetToken = (JValue)target.SelectToken(TargetPath);

			targetToken.Value = Expected;
		}

	}
}