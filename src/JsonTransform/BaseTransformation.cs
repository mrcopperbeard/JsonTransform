namespace JsonTransform
{
	/// <summary>
	/// Реализация базовой трансформации.
	/// </summary>
	internal abstract class BaseTransformation
	{
		/// <summary>
		/// Initialize of <see cref="BaseTransformation"/>.
		/// </summary>
		/// <param name="targetPath">Path to node.</param>
		protected BaseTransformation(string targetPath)
		{
			TargetPath = targetPath;
		}

		/// <summary>
		/// Path to node.
		/// </summary>
		protected string TargetPath { get; }
	}
}