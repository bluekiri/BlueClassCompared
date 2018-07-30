using System;
using System.Collections.Generic;
using System.Text;
using BlueClassCompared;
using Xunit;

namespace XUnitTestProject1
{
    public class BasicTypeCompared
    {
        [Fact]
        public void We_Can_compare_two_string_abs()
        {
            var firstString = "aeiou";
            var secondString = "aeiou";
            Assert.True(ClassCompared.AbsoluteCompareResult(firstString, secondString));
        }

        [Fact]
        public void If_we_compare_empty_vs_null_in_absolutecompareresult_we_have_false()
        {
            var firstString = string.Empty;
            Assert.False(ClassCompared.AbsoluteCompareResult(firstString, null));
        }

        [Fact]
        public void If_we_compare_emtpty_vs_null_in_compareproperties_we_have_true()
        {
            var firstString = string.Empty;
            Assert.True(ClassCompared.ComparePropertiesResult(firstString, null));
        }

        [Fact]
        public void If_we_compare_two_diferent_int_we_get_always_false()
        {
            int? firstInt = 5;
            int? secondInt = 0;
            Assert.False(ClassCompared.ComparePropertiesResult(firstInt, secondInt));
            Assert.False(ClassCompared.AbsoluteCompareResult(firstInt, secondInt));
            Assert.False(ClassCompared.ComparePropertiesResult(null, secondInt));
            Assert.False(ClassCompared.AbsoluteCompareResult(null, secondInt));
        }


        [Fact]
        public void If_we_compare_two_diferent_list_of_string_with_AbsoluteCompare_only_get_true_if_all_items_are_equal()
        {
            var firstListString = new List<string>() { "a", "ei", "ou" };
            var secondListString = new List<string>() { "a", "ei", "ou" };
            var firstListStringA = new List<string>() { "a", "ei", "ou", string.Empty };
            var secondListStringA = new List<string>() { "a", "ei", "ou", null };

            Assert.True(ClassCompared.AbsoluteCompareResult(firstListString, secondListString));
            Assert.False(ClassCompared.AbsoluteCompareResult(firstListStringA, secondListStringA));
        }


        [Fact]
        public void If_we_compare_two_diferent_list_of_string_with_CompareProperties_only_get_true_if_all_items_are_equal_or_one_is_null_and_other_is_empty()
        {
            var firstListString = new List<string>() { "a", "ei", "ou" };
            var secondListString = new List<string>() { "a", "ei", "ou" };
            var firstListStringA = new List<string>() { "a", "ei", "ou", string.Empty };
            var secondListStringA = new List<string>() { "a", "ei", "ou", null };

            Assert.True(ClassCompared.ComparePropertiesResult(firstListString, secondListString));
            Assert.True(ClassCompared.ComparePropertiesResult(firstListStringA, secondListStringA));
        }
    }
}
