using System;
using System.Collections.Generic;
using BlueClassCompared;
using Xunit;

namespace XUnitTestProject1
{
    public class ClassToCompare
    {
        public string oneString { get; set; }
        public int ontInt { get; set; }
        public string otherString { get; set; }
        public List<string> oneStringList { get; set; }
    }
    public class ComplexTypeCompared
    {
        [Fact]
        public void DefaultComplexType_can_be_compare_with_both_methods_wiht_same_result()
        {
            var oneComplexClass = new ClassToCompare();
            var twoComplexClass = new ClassToCompare();
            Assert.Equal(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass), ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass));
            Assert.True(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass));
        }

        [Fact]
        public void DefaultComplexType_with_some_properties_initialiced_can_be_compare_with_both_methods_wiht_same_result()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 5, otherString = "test" };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, otherString = "test" };
            Assert.Equal(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass), ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass));
            Assert.True(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass));
        }


        [Fact]
        public void DefaultComplexType_with_diferent_properties_initialiced_can_be_compare_with_both_methods_wiht_same_result()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 5, oneString = "test" };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, otherString = "test" };
            Assert.Equal(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass), ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass));
            Assert.False(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass));
        }


        [Fact]
        public void DefaultComplexType_with_diferent_properties_initialiced_can_be_compare_with_both_methods_wiht_diferent_result_if_in_compareproperties_exclude_propertie_that_are_diferent()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 5, oneString = "test" };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, otherString = "test" };
            Assert.NotEqual(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass), ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass, new List<string>() { "oneString", "otherString" }));
            Assert.False(ClassCompared.AbsoluteCompareResult(oneComplexClass, twoComplexClass));
        }

        [Fact]
        public void In_CompareProperties_we_can_exclude_from_compare_all_properties()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 9, oneString = "test", otherString = "diff", oneStringList = new List<string>() { "one" } };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, oneString = "", otherString = "test" };
            Assert.True(ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass, new List<string>() { "ontInt", "oneString", "otherString", "oneStringList" }));

        }

        [Fact]
        public void In_CompareProperties_one_list_initialized_vs_not_initialized_will_get_false()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 5, oneString = "", oneStringList = new List<string>() };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, oneString = "", };
            Assert.False(ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass));

        }


        [Fact]
        public void In_CompareProperties_one_list_initialized_vs_not_initialized_will_get_false_but_we_can_Exclude_it_and_get_true()
        {
            var oneComplexClass = new ClassToCompare() { ontInt = 5, oneString = "", oneStringList = new List<string>() };
            var twoComplexClass = new ClassToCompare() { ontInt = 5, oneString = "", };
            Assert.True(ClassCompared.ComparePropertiesResult(oneComplexClass, twoComplexClass, new List<string>() { "oneStringList" }));

        }
    }
}
