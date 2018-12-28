using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Base transformation.
	/// </summary>
	public abstract class BaseTransformation : ITransformation
	{
		/// <summary>
		/// Initialize of <see cref="BaseTransformation"/>.
		/// </summary>
		/// <param name="context">Transformation create context.</param>
		protected BaseTransformation(ITransformationCreateContext context)
		{
			TargetPath = context.TargetPath;
		}

		/// <summary>
		/// Path to node.
		/// </summary>
		protected string TargetPath { get; }

		/// <inheritdoc />
		public abstract void ApplyTo(JObject target, ITransformationInvokeContext context);
	}
}