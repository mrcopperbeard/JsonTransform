using System.Reflection;
using System.Text;

namespace JsonTransform.Tests
{
	/// <summary>
	/// Workaround for supproting embedded resources in Microsoft.NET.Sdk.
	/// </summary>
	public static class Resources
	{
		/// <summary>
		/// Current assembly.
		/// </summary>
		private static readonly Assembly Assembly = typeof(Resources).Assembly;

		/// <summary>
		/// Resource prefix..
		/// </summary>
		private static readonly string Prefix = typeof(Resources).FullName;

		/// <summary>
		/// Complex transformation.
		/// </summary>
		public class ComplexTransform
		{
			/// <summary>
			/// Expected JSON.
			/// </summary>
			public static string Expected => GetString(@"ComplexTransformation.Expected.json");

			/// <summary>
			/// Source JSON.
			/// </summary>
			public static string Source => GetString(@"ComplexTransformation.Source.json");

			/// <summary>
			/// JSON with transformation.
			/// </summary>
			public static string Transformation => GetString(@"ComplexTransformation.Transformation.json");
		}

		/// <summary>
		/// Read resource file as string.
		/// </summary>
		/// <param name="filename">Resource filename.</param>
		/// <returns>String with file content.</returns>
		private static string GetString(string filename)
		{
			using (var stream = Assembly.GetManifestResourceStream($"{Prefix}.{filename}"))
			{
				if (stream == null)
				{
					return null;
				}

				var buffer = new byte[stream.Length];
				stream.Read(buffer, 0, buffer.Length);

				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}