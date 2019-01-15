using System;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Base transformation.
	/// </summary>
	internal abstract class BaseTransformation : ITransformation
	{
		/// <summary>
		/// Initialize of <see cref="BaseTransformation"/>.
		/// </summary>
		/// <param name="context">Transformation create context.</param>
		protected BaseTransformation(ITransformationCreateContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Path to node.
		/// </summary>
		protected ITransformationCreateContext Context { get; }

		/// <summary>
		/// Handling error.
		/// </summary>
		/// <param name="message">Error message.</param>
		protected void OnErrorInternal(string message)
		{
			OnErrorInternal(Context.TargetPath, message);
		}

		/// <summary>
		/// Handling error.
		/// </summary>
		/// <param name="path">Path to property.</param>
		/// <param name="message">Error message.</param>
		protected void OnErrorInternal(string path, string message)
		{
			OnError?.Invoke(this, new TransformErrorEventArgs
			{
				TargetPath = path,
				Message = message,
			});
		}

		/// <inheritdoc />
		public abstract void ApplyTo(JObject target, ITransformationInvokeContext context);

		/// <inheritdoc />
		public event EventHandler<TransformErrorEventArgs> OnError;
	}
}