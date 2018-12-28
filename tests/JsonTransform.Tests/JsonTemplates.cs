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
	""rootItem"": ""root item"",
	""array"": [{
			""value"": 1,
			""otherValue"": 2,
			""inner"": {
				""innerValue1"": ""inner value 1""
			},
			""innerArray"": [""First""]
		},{
			""inner"": {
				""innerValue4"": ""inner value 4""
			}
		}
	]
}";

			public const string Transformation = @"{
	""array"": [{
			""value"": ""one"",
			""inner"": {
				""innerValue2"": ""inner value 2""
			},
			""innerArray"": [""Second""]
		},{
			""value"": ""two"",
			""inner"": {
				""innerValue3"": ""inner value 3"",
				""odd"": ""some odd property""
			}
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
		""transform-setnull-value"": ""any value that will be ignored""
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
		""transform-remove-secondLevel"": null
	}
}";

			public const string RemoveFirstLevel = @"{ ""transform-remove-firstLevel"": null }";
		}

		public static class CopyNode
		{
			public const string SourceWithObject = @"{
	""source"": {
		""inner"": {
			""value"": true
		}
	},
	""target"": null
}";

			public const string SourceWithString = @"{
	""source"": {
		""inner"": ""test""
	},
	""target"": null
}";

			public const string SourceWithoutTarget = @"{
	""source"": {
		""inner"": ""test""
	}
}";

			public const string Transformation = @"{ ""transform-copy-target"": ""source.inner"" }";

			public const string CopyRootTransformation = @"{ ""transform-copy-target"": null }";
		}

		public static class ForEach
		{
			public const string Source = @"{
	""array"": [
		{
			""source"": ""Expected"",
			""removeMe"": ""Remove me""
		},{
			""removeMe"": ""Remove me too""
		},{
		}
	]
}";

			public const string Transformation = @"{
	""transform-foreach-array"": {
		""transform-remove-removeMe"": null,
		""transform-copy-target"": ""array[0].source"",
	}
}";
		}

		public static class Custom
		{
			public const string Source = @"{
	""root"": {
		""value"": 1
	}
}";

			public const string Transformation = @"{
	""root"": {
		""transform-custom-test-value"": null
	},
}";
		}
	}
}