using System;

namespace JsonTransform
{
	public class TransformationFactory
	{
		public ITransformation Create(string value)
		{
			switch (value)
			{
				case "#remove":
					return new RemoveTransformation();
				default:
					return null;
			}
		}
	}
}