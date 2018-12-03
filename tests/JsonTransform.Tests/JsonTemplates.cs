namespace JsonTransform.Tests
{
	public static class JsonTemplates
	{
		public const string EmptySource = @"{}";

		public static class SetConstString
		{
			public const string Source = @"{
	""first"": {
		""value"": 1
	},
	""second"": ""two""
}";

			public const string Transformation = @"{
	""first"": {
		""value"": ""one""
	},
}";
		}

		public static class SetConstToArray
		{
			public const string Source = @"{
	""array"": [{
			""value"": 1
		}
	]
}";

			public const string Transformation = @"{
	""array"": [{
			""value"": ""one""
		},{
			""value"": ""two""
		}
	]
}";
		}

		public static class SetConstBool
		{
			public const string Source = @"{
	""first"": {
		""value"": 1
	},
	""second"": ""two""
}";

			public const string Transformation = @"{
	""first"": {
		""value"": true
	},
}";
		}

		public static class SetNull
		{
			public const string Source = @"{
	""first"": {
		""value"": 1,
		""reallyNullValue"": null,
	},
	""second"": ""two""
}";

			public const string Transformation = @"{
	""first"": {
		""value"": null
	},
}";
		}

		public static class RemoveNode
		{
			public const string Source = @"{
	""firstLevel"": {
		""secondLevel"": {
			""value"": true
		},
		""secondLevel1"": true
	},
	""firstLevel1"": true
}";

			public const string RemoveSecondLevel = @"{
	""firstLevel"": {
		""secondLevel"": ""#remove""
	}
}";

			public const string RemoveFirstLevel = @"{ ""firstLevel"": ""#remove"" }";
		}

		public static class CopyNode
		{
			public const string SourceWithObject = @"{
	""source"": {
		""inner"": {
			""value"": true
		},
	},
	""target"": ""#copyFrom(source.inner)""
}";

			public const string SourceWithString = @"{
	""source"": {
		""inner"": ""test""
	},
	""target"": ""#copyFrom(source.inner)""
}";

			public const string Transformation = @"{ ""target"": ""#copyFrom(source.inner)"" }";

			public const string CopyRootTransformation = @"{ ""target"": ""#copyFrom()"" }";

			public const string TransformationWitInvalidPath = @"{ ""target"": ""#copyFrom(#)"" }";
		}
	}
}