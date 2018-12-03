using System;
using System.Text.RegularExpressions;

namespace JsonTransform
{
	public class TransformationFactory
	{
		/// <summary>
		/// Регулярное выражение для извлечения аргументов трансформации.
		/// </summary>
		private static readonly Regex TransformationSingleArgumentRegex = new Regex($@"^\#[a-zA-Z]+\(({InternalConstants.AvailablePathSymbols})\)$");

		public ITransformation Create(string value, string path)
		{
			if (value == "#remove")
			{
				return new RemoveTransformation(path);
			}

			if (value.StartsWith("#copyFrom"))
			{
				var matches = TransformationSingleArgumentRegex.Matches(value);
				var sourcePath = matches.Count > 0
					? matches[0].Groups[1].Value
					: throw new ArgumentException($"Can not parse path to copying node \"{path}\". Path may contain only \"{InternalConstants.AvailablePathSymbols}\".");

				return new CopyTransformation(sourcePath, path);
			}

			return null;
		}
	}
}