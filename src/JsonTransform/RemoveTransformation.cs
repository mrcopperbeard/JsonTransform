using System;

using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <inheritdoc />
	internal class RemoveTransformation : ITransformation
	{
		/// <summary>
		/// Путь к удаляемому элементу.
		/// </summary>
		private readonly string _path;

		/// <summary>
		/// Инициализация <see cref="RemoveTransformation"/>.
		/// </summary>
		/// <param name="path">Путь к удаляемому элементу.</param>
		public RemoveTransformation(string path)
		{
			_path = path;
		}

		/// <inheritdoc />
		public void ApplyTo(JObject obj, ITransformationContext context)
		{
			var token = obj.SelectToken(_path);
			if (token == null)
			{
				return;
			}

			try
			{
				token.Parent.Remove();
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Error while removing {token.Path}. {e.Message}", e);
			}
		}
	}
}