using System.Text;

namespace JsonTransform
{
	/// <summary>
	/// Класс расширений.
	/// </summary>
	internal static class InternalExtensions
	{
		/// <summary>
		/// Добавить узел в путь к свойству JSON-объекта.
		/// </summary>
		/// <param name="builder">Текущий экземпляр <see cref="StringBuilder"/>.</param>
		/// <param name="node">Наименование узла.</param>
		/// <returns>Текущий экземпляр <see cref="StringBuilder"/>.</returns>
		public static StringBuilder AppendNode(this StringBuilder builder, string node)
		{
			const char Separator = '.';
			if (builder.Length > 0)
			{
				builder.Append(Separator);
			}

			builder.Append(node);

			return builder;
		}
	}
}