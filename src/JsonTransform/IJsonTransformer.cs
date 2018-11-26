namespace JsonTransform
{
	/// <summary>
	/// Класс-преобразователь JSON-файлов при помощи JSON-трансформаций.
	/// </summary>
	public interface IJsonTransformer
	{
		/// <summary>
		/// Преобразовать исходную строку при помощи указанной трансформации.
		/// </summary>
		/// <param name="source">Исходная строка.</param>
		/// <param name="transformation">Строка с трансформацией.</param>
		/// <returns></returns>
		string Transform(string source, string transformation);
	}
}