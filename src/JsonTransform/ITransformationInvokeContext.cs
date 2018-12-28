using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Контекст трансформации.
	/// </summary>
	public interface ITransformationInvokeContext
	{
		/// <summary>
		/// Объект до трансформации.
		/// </summary>
		JToken Source { get; }
	}
}