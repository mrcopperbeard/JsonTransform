namespace JsonTransform.Tests
{
	public static class JsonTemplates
	{
		public static class SetConstString
		{
			public const string EmptySource = @"{}";

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
	}
}