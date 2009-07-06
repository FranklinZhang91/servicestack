using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using ServiceStack.Common.Extensions;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceModel.Tests.DataContracts;

namespace ServiceStack.ServiceModel.Tests
{
	[TestFixture]
	public class StringConverterUtilsTests
	{
		public class StringEnumerable : IEnumerable<string>
		{
			public List<string> Items = new[] { "a", "b", "c" }.ToList();

			public IEnumerator<string> GetEnumerator()
			{
				return Items.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public static StringEnumerable Parse(string value)
			{
				return new StringEnumerable {
					Items = value.To<List<string>>()
				};
			}

		}

		[Test]
		public void Create_super_list_type_of_int_from_string()
		{
			var textValue = "1,2,3";
			var convertedValue = textValue.Split(',').ToList().ConvertAll(x => Convert.ToInt32(x));
			var result = StringConverterUtils.Parse<ArrayOfIntId>(textValue);
			Assert.That(result, Is.EquivalentTo(convertedValue));
		}

		[Test]
		public void Create_guid_from_string()
		{
			var textValue = "40DFA5A2-8054-4b3e-B7F5-06E61FF387EF";
			var convertedValue = new Guid(textValue);
			var result = StringConverterUtils.Parse<Guid>(textValue);
			Assert.That(result, Is.EqualTo(convertedValue));
		}

		[Test]
		public void Create_int_from_string()
		{
			var textValue = "99";
			var convertedValue = int.Parse(textValue);
			var result = StringConverterUtils.Parse<int>(textValue);
			Assert.That(result, Is.EqualTo(convertedValue));
		}

		[Test]
		public void Create_bool_from_string()
		{
			var textValue = "True";
			var convertedValue = bool.Parse(textValue);
			var result = StringConverterUtils.Parse<bool>(textValue);
			Assert.That(result, Is.EqualTo(convertedValue));
		}

		[Test]
		public void Create_string_array_from_string()
		{
			var convertedValue = new[] { "Hello", "World" };
			var textValue = string.Join(",", convertedValue);
			var result = StringConverterUtils.Parse<string[]>(textValue);
			Assert.That(result, Is.EqualTo(convertedValue));
		}

		[Test]
		public void Create_from_StringEnumerable()
		{
			var value = StringEnumerable.Parse("d,e,f");
			var convertedValue = StringConverterUtils.ToString(value);
			var result = StringConverterUtils.Parse<StringEnumerable>(convertedValue);
			Assert.That(result, Is.EquivalentTo(value.Items));
		}

	}
}