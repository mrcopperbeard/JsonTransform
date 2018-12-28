# JsonTransform
Provides a power of a [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/)
to implement the functionality of transforming one JSON object to another. Like XSLT for XML but for JSON files.

## Usage
Just use one of static `JsonTransformer` methods
```c#
		/// <summary>
		/// Transform specified JSON-object with specified transformation.
		/// </summary>
		/// <param name="source">Source JSON-object.</param>
		/// <param name="transformation">Transformation meta object.</param>
		/// <returns>Transformed JSON-object.</returns>
		public static JObject Transform(JObject source, JObject transformation)

		/// <summary>
		/// Transform specified string with JSON with specified transformation.
		/// </summary>
		/// <param name="source">Source JSON string.</param>
		/// <param name="transformDescription">String with transformation meta.</param>
		/// <returns>Transformed JSON-string.</returns>
		public static JObject Transform(string source, string transformDescription)
```

Transformed will merge source and transformation object, except special properties in transformation starts with *"transform-"* prefix
and interpreting as transformation commands. After merge transformer applies all additional transformations to result object.

Returning `JObject` has overriden `ToString` method to easy translate it to *string*.

## Transformation commands.

All command descriptions in transformation metaobject are JSON properties which name starts with *"transform-"* prefix
and helps to determine which command to use. Property name contains three blocks separated by hyphen.

Value of property is using as optional argument of several commands.
If command doesn't require any arguments - value may contain anything and will be ignored.

For example 
`"transform-copy-propertyName": "path.to.source.property"`

Where `transform` is special prefix,
`copy` - key of command,
`propertyName` - name of target property of called command.
*ATTENTION*: You should never use the hyphens in property names. I hope you weren't going to do it before.

Value contains a [JSON path](https://stackoverflow.com/tags/jsonpath) to property that will be copied.

### copy
*Argument*: Path to property we want to copy. You can set null to copy root object.
*Description*: Copies to the property with name specified in 3rd segment of command name value from element in source object, found by specified path.
**Example**:
*Source*
```json
{
	"source": {
		"inner": {
			"value": true
		}
	},
	"target": null
}
```
*Transformation*
```json
{ "transform-copy-target": "source.inner" }
```
*Result*
```json
{
  "source": {
    "inner": {
      "value": true
    }
  },
  "target": {
    "value": true
  }
}
```

### foreach
*Apply to*: Arrays
*Argument*: Inner transformation object for each item of array.
*Description*: Immplements transformation described in argument of command for each item of array.
**Example**: Removes property "removeMe" from each element of objects array
*Source*
```json
{
    "array": [
        {
            "id": 1,
            "removeMe": 42
        },
        {
            "id": 2,
            "removeMe": 42
        },
        {
            "id": 3,
            "removeMe": 42
        }
    ]
}
```
*Transformation*
```json
{"transform-foreach-array": {"transform-remove-removeMe": null}}
```
*Result*
```json
{
    "array": [
        {
            "id": 1
        },
        {
            "id": 2
        },
        {
            "id": 3
        }
    ]
}
```

**ATTENTION**: You should use relative paths about command argument object, not a root object.
For source
```json
{
    "outerSource": "value",
    "array": []
}
```
transformation
```json
{
    "outerSource": "value",
    "transform-foreach-array": {
        "innerSource": "value",
        "transform-copy-target": "outerSource"
    }
}
```
will throw exception, correct transformation should have "transform-copy-target": "innerSource"

### remove
*Argument*: Ignoring
*Description*: Removes element from source object.
**Example**:
*Source*
```json
{
  "removeMe": 42,
  "dontRemoveMe": true
}
```
*Transformation*
```json
{"transform-remove-removeMe": null}
```
or
```json
{"transform-remove-removeMe": "whatever value. this wiil be ignored"}
```
*Result*
```json
{"dontRemoveMe": true}
```
### setnull
*Example*:
```json
"transform-setnull-value": "any value that will be ignored"
```


### union
*Argument*: Array
*Description*: Union source array with specified in argument.
**Example**:
*Source*
```json
{"array": [1, 2]}
```
*Transformation*
```json
{"transform-union-array": [2, 3]}
```
*Result*
```json
{"array": [1, 2, 3]}
```

## Complete example of usage
```c#
			var source = @"{
        ""source"": {
          ""inner"": {
            ""value"": true
          },
        },
        ""target"": null
      }";

			var transformation = @"{ ""transform-copy-target"": ""source.inner"" }";

			var result = JsonTransformer.Transform(source, transformation).ToString();

			Console.Out.WriteLine(result);
```

console output will be
```json
{
  "source": {
    "inner": {
      "value": true
    }
  },
  "target": {
    "value": true
  }
}
```

## Custom transformations

You can use your own transformations in JsonTransform. Just implement `ITransformation` interface and register your transform with
static `JsonTransformer.RegisterTransformation` method. First parameter is a code of your transformation, which you can call from
transformation JSON file. For forward compability custom transformations mark with prefix `transform-custom-`, so don't worry about conflicts with built-in transformations.

[See usage example](https://github.com/mrcopperbeard/JsonTransform/blob/master/tests/JsonTransform.Tests/CustomTransform/CustomTransformationsTests.cs)
