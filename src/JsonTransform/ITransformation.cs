using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Трансформация над объектом <see cref="JObject"/>.
	/// </summary>
	public interface ITransformation
	{
		/// <summary>
		/// Применить трансформацию.
		/// </summary>
		/// <param name="token">Узел, к которому применяется трансформация.</param>
		void ApplyTo(JToken token);
	}
}