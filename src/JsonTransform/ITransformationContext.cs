using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Контекст трансформации.
	/// </summary>
	public interface ITransformationContext
	{
		/// <summary>
		/// Объект до трансформации.
		/// </summary>
		JToken Source { get; }
	}
}