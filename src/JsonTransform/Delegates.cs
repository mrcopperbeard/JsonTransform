namespace JsonTransform
{
	/// <summary>
	/// Transformation activator.
	/// </summary>
	/// <param name="context">Context of transformation creation.</param>
	/// <returns>Created transformation.</returns>
	public delegate ITransformation TransformationActivator(ITransformationCreateContext context);

	/// <summary>
	/// Transformation error handler.
	/// </summary>
	/// <param name="path">Path to node.</param>
	/// <param name="message">Error message.</param>
	public delegate void TransformationErrorHandler(string path, string message);
}