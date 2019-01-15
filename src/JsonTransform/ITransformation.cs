using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Transformation of <see cref="JObject"/>.
	/// </summary>
	public interface ITransformation
	{
		/// <summary>
		/// Apply transformation.
		/// </summary>
		/// <param name="target">Target object.</param>
		/// <param name="context">Transformation context.</param>
		void ApplyTo(JObject target, ITransformationInvokeContext context);

		/// <summary>
		/// Event triggering when error caused.
		/// </summary>
		event EventHandler<TransformErrorEventArgs> OnError;
	}
}