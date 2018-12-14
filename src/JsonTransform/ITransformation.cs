using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	/// <summary>
	/// Трансформация над объектом <see cref="JObject"/>.
	/// </summary>
	internal interface ITransformation
	{
		/// <summary>
		/// Применить трансформацию.
		/// </summary>
		/// <param name="target">Объект, к которому применяется трансформация.</param>
		/// <param name="context">Контекст трансформации.</param>
		void ApplyTo(JObject target, ITransformationContext context);
	}
}