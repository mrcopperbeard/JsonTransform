using System;
using Newtonsoft.Json.Linq;

namespace JsonTransform
{
	public class TransformationFactory
	{
		public ITransformation Create(string value, string path)
		{
			switch (value)
			{
				case "#remove":
					return new RemoveTransformation(path);
				default:
					return null;
			}
		}
	}
}