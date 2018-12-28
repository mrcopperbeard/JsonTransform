using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Context of transformation creation.
	/// </summary>
	public interface ITransformationCreateContext
	{
		/// <summary>
		/// Target path of transformation.
		/// </summary>
		string TargetPath { get; }

		/// <summary>
		/// Property that contains transformation info.
		/// </summary>
		JProperty Property { get; }
	}
}