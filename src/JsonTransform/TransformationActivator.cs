namespace JsonTransform
{
	/// <summary>
	/// Transformation activator.
	/// </summary>
	/// <param name="context">Context of transformation creation.</param>
	/// <returns>Created transformation.</returns>
	public delegate ITransformation TransformationActivator(ITransformationCreateContext context);
}