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
		/// <param name="obj">Объект, к которому применяется трансформация.</param>
		void ApplyTo(JObject obj);
	}
}