using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ServiceStack.Common.Extensions;
using ServiceStack.Common.Utils;

namespace ServiceStack.Common.Tests.Perf
{
	[Ignore]
	[TestFixture]
	public class ToStringPerf
		: PerfTestBase
	{
		public ToStringPerf()
		{
			this.MultipleIterations = new List<int> { 1000000 };
		}

		[Test]
		public void Compare_string()
		{
			CompareMultipleRuns(
				"'test'.ToSafeString()", () => "test".ToSafeString(),
				"SCU.ToString('test')", () => StringConverterUtils.ToString("test")
			);
		}

		[Test]
		public void Compare_escaped_string()
		{
			CompareMultipleRuns(
				"'t,e:st'.ToSafeString()", () => "t,e:st".ToSafeString(),
				"SCU.ToString('t,e:st')", () => StringConverterUtils.ToString("t,e:st")
			);
		}

		[Test]
		public void Compare_ints()
		{
			CompareMultipleRuns(
				"1.ToString()", () => 1.ToString(),
				"SCU.ToString(1)", () => StringConverterUtils.ToString(1)
			);
		}

		[Test]
		public void Compare_longs()
		{
			CompareMultipleRuns(
				"1L.ToString()", () => 1L.ToString(),
				"SCU.ToString(1L)", () => StringConverterUtils.ToString(1L)
			);
		}

		[Test]
		public void Compare_Guids()
		{
			var guid = new Guid("AC800C9C-B8BE-4829-868A-B43CFF7B2AFD");
			CompareMultipleRuns(
				"guid.ToString()", () => guid.ToString(),
				"SCU.ToString(guid)", () => StringConverterUtils.ToString(guid)
			);
		}

		[Test]
		public void Compare_DateTime()
		{
			var now = DateTime.Now;
			CompareMultipleRuns(
				"now.ToString()", () => now.ToString(),
				"SCU.ToString(now)", () => StringConverterUtils.ToString(now)
			);
		}

		[Test]
		public void Compare_IntList()
		{
			var intList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			CompareMultipleRuns(
				"intList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					intList.ForEach(x => { if (sb.Length > 0) sb.Append(","); sb.Append(x.ToString()); });
					sb.ToString();
				},
				"SCU.ToString(intList)", () => StringConverterUtils.ToString(intList)
			);
		}

		[Test]
		public void Compare_LongList()
		{
			var longList = new List<long> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			CompareMultipleRuns(
				"longList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					longList.ForEach(x => { if (sb.Length > 0) sb.Append(","); sb.Append(x.ToString()); });
					sb.ToString();
				},
				"SCU.ToString(longList)", () => StringConverterUtils.ToString(longList)
			);
		}

		[Test]
		public void Compare_StringArray()
		{
			var stringArray = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
			CompareMultipleRuns(
				"sb.Append(s.ToSafeString());", () => {
					var sb = new StringBuilder();
					foreach (var s in stringArray)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(s.ToSafeString());
					}
					sb.ToString();
				},
				"SCU.ToString(stringArray)", () => StringConverterUtils.ToString(stringArray)
			);
		}

		[Test]
		public void Compare_StringList()
		{
			var stringList = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
			CompareMultipleRuns(
				"sb.Append(s.ToSafeString());", () => {
					var sb = new StringBuilder();
					stringList.ForEach(x => { if (sb.Length > 0) sb.Append(","); sb.Append(x.ToString()); });
					sb.ToString();
				},
				"SCU.ToString(stringList)", () => StringConverterUtils.ToString(stringList)
			);
		}

		[Test]
		public void Compare_DoubleList()
		{
			var doubleList = new List<double> { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 0.1 };
			CompareMultipleRuns(
				"doubleList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					doubleList.ForEach(x => { if (sb.Length > 0) sb.Append(","); sb.Append(x.ToString()); });
					sb.ToString();
				},
				"SCU.ToString(doubleList)", () => StringConverterUtils.ToString(doubleList)
			);
		}

		[Test]
		public void Compare_GuidList()
		{
			var guidList = new List<Guid>
           	{
           		new Guid("8F403A5E-CDFC-4C6F-B0EB-C055C1C8BA60"),
				new Guid("5673BAC7-BAC5-4B3F-9B69-4180E6227508"),
				new Guid("B0CA730F-14C9-4D00-AC7F-07E7DE8D566E"),
				new Guid("4E26AF94-6B13-4F89-B192-36C6ABE73DAE"),
				new Guid("08491B16-2270-4DF9-8AEE-A8861A791C50"),
           	};
			CompareMultipleRuns(
				"guidList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					guidList.ForEach(x => { if (sb.Length > 0) sb.Append(","); sb.Append(x.ToString()); });
					sb.ToString();
				},
				"SCU.ToString(guidList)", () => StringConverterUtils.ToString(guidList)
			);
		}

		[Test]
		public void Compare_StringHashSet()
		{
			var stringHashSet = new HashSet<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
			CompareMultipleRuns(
				"sb.Append(s.ToSafeString());", () => {
					var sb = new StringBuilder();
					foreach (var s in stringHashSet)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(s.ToSafeString());
					}
					sb.ToString();
				},
				"SCU.ToString(stringHashSet)", () => StringConverterUtils.ToString(stringHashSet)
			);
		}

		[Test]
		public void Compare_IntHashSet()
		{
			var intHashSet = new HashSet<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			CompareMultipleRuns(
				"intList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					foreach (var s in intHashSet)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(s.ToString());
					}
					sb.ToString();
				},
				"SCU.ToString(intHashSet)", () => StringConverterUtils.ToString(intHashSet)
			);
		}

		[Test]
		public void Compare_DoubleHashSet()
		{
			var doubleHashSet = new HashSet<double> { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 0.1 };
			CompareMultipleRuns(
				"doubleList.ForEach(x => sb.Append(x.ToString()))", () => {
					var sb = new StringBuilder();
					foreach (var s in doubleHashSet)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(s.ToString());
					}
					sb.ToString();
				},
				"SCU.ToString(doubleHashSet)", () => StringConverterUtils.ToString(doubleHashSet)
			);
		}

		[Test]
		public void Compare_StringStringMap()
		{
			var map = new Dictionary<string, string> {
          		{"A", "1"},{"B", "2"},{"C", "3"},{"D", "4"},{"E", "5"},
          		{"F", "6"},{"G", "7"},{"H", "8"},{"I", "9"},{"j", "10"},
          	};
			CompareMultipleRuns(
				"sb.Append(kv.Key.ToSafeString()).Append(ParseStringMethods.KeyValueSeperator).", () => {
					var sb = new StringBuilder();
					foreach (var kv in map)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(kv.Key.ToSafeString())
							.Append(TextExtensions.KeyValueSeperator)
							.Append(kv.Value.ToSafeString());
					}
					sb.ToString();
				},
				"SCU.ToString(map)", () => StringConverterUtils.ToString(map)
			);
		}

		[Test]
		public void Compare_StringIntMap()
		{
			var map = new Dictionary<string, int> {
          		{"A", 1},{"B", 2},{"C", 3},{"D", 4},{"E", 5},
          		{"F", 6},{"G", 7},{"H", 8},{"I", 9},{"j", 10},
          	};
			CompareMultipleRuns(
				".Append(ParseStringMethods.KeyValueSeperator).Append(kv.Value.ToString())", () => {
					var sb = new StringBuilder();
					foreach (var kv in map)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(kv.Key.ToSafeString())
							.Append(TextExtensions.KeyValueSeperator)
							.Append(kv.Value.ToString());
					}
					sb.ToString();
				},
				"SCU.ToString(map)", () => StringConverterUtils.ToString(map)
			);
		}

		[Test]
		public void Compare_StringInt_SortedDictionary()
		{
			var map = new SortedDictionary<string, int>{
          		{"A", 1},{"B", 2},{"C", 3},{"D", 4},{"E", 5},
          		{"F", 6},{"G", 7},{"H", 8},{"I", 9},{"j", 10},
          	};
			CompareMultipleRuns(
				".Append(ParseStringMethods.KeyValueSeperator).Append(kv.Value.ToString())", () => {
					var sb = new StringBuilder();
					foreach (var kv in map)
					{
						if (sb.Length > 0) sb.Append(",");
						sb.Append(kv.Key.ToSafeString())
							.Append(TextExtensions.KeyValueSeperator)
							.Append(kv.Value.ToString());
					}
					sb.ToString();
				},
				"SCU.ToString(map)", () => StringConverterUtils.ToString(map)
			);
		}

		[Test]
		public void Compare_ByteArray()
		{
			var byteArrayValue = new byte[] { 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, 0, 65, 97, 255, };

			CompareMultipleRuns(
				"Encoding.Default.GetString(byteArrayValue)", () => Encoding.Default.GetString(byteArrayValue),
				"SCU.ToString(byteArrayValue)", () => StringConverterUtils.ToString(byteArrayValue)
			);
		}

	}
}