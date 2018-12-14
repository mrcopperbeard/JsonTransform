using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Класс-преобразователь JSON-файлов при помощи JSON-трансформаций.
	/// </summary>
	public interface IJsonTransformer
	{
		/// <summary>
		/// Преобразовать исходный JSON-объект при помощи указанной трансформации.
		/// </summary>
		/// <param name="source">Исходный JSON-объект.</param>
		/// <param name="transformation">Объект с трансформацией.</param>
		/// <returns>Трансформированный JSON-объект.</returns>
		JObject Transform(JObject source, JObject transformation);

		/// <summary>
		/// Преобразовать исходную строку при помощи указанной трансформации.
		/// </summary>
		/// <param name="source">Исходная строка.</param>
		/// <param name="transformation">Строка с трансформацией.</param>
		/// <returns>Трансформированный JSON-объект.</returns>
		JObject Transform(string source, string transformation);
	}
}