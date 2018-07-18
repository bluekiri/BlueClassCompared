using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueClassCompared.Tests
{
    [TestClass]
    public class BasicTypeCompared
    {
        [TestMethod]
        public void We_Can_compare_two_string_abs()
        {
            var firstString = "aeiou";
            var secondString = "aeiou";
            Assert.IsTrue(ClassCompared.AbsoluteCompareResult(firstString, secondString));
        }

        [TestMethod]
        public void If_we_compare_empty_vs_null_in_absolutecompareresult_we_have_false()
        {
            var firstString = string.Empty;
            Assert.IsFalse(ClassCompared.AbsoluteCompareResult(firstString, null));
        }

        [TestMethod]
        public void If_we_compare_emtpty_vs_null_in_compareproperties_we_have_true()
        {
            var firstString = string.Empty;
            Assert.IsTrue(ClassCompared.ComparePropertiesResult(firstString, null));
        }

        [TestMethod]
        public void If_we_compare_two_diferent_int_we_get_always_false()
        {
            int? firstInt = 5;
            int? secondInt = 0;
            Assert.IsFalse(ClassCompared.ComparePropertiesResult(firstInt, secondInt));
            Assert.IsFalse(ClassCompared.AbsoluteCompareResult(firstInt, secondInt));
            Assert.IsFalse(ClassCompared.ComparePropertiesResult(null, secondInt));
            Assert.IsFalse(ClassCompared.AbsoluteCompareResult(null, secondInt));
        }


        [TestMethod]
        public void If_we_compare_two_diferent_list_of_string_with_AbsoluteCompare_only_get_true_if_all_items_are_equal()
        {
            var firstListString = new List<string>(){"a", "ei", "ou"};
            var secondListString = new List<string>() { "a", "ei", "ou" };
            var firstListStringA = new List<string>() { "a", "ei", "ou", string.Empty };
            var secondListStringA = new List<string>() { "a", "ei", "ou", null };

            Assert.IsTrue(ClassCompared.AbsoluteCompareResult(firstListString, secondListString));
            Assert.IsFalse(ClassCompared.AbsoluteCompareResult(firstListStringA, secondListStringA));
        }


        [TestMethod]
        public void If_we_compare_two_diferent_list_of_string_with_CompareProperties_only_get_true_if_all_items_are_equal_or_one_is_null_and_other_is_empty()
        {
            var firstListString = new List<string>() { "a", "ei", "ou" };
            var secondListString = new List<string>() { "a", "ei", "ou" };
            var firstListStringA = new List<string>() { "a", "ei", "ou", string.Empty };
            var secondListStringA = new List<string>() { "a", "ei", "ou", null };

            Assert.IsTrue(ClassCompared.ComparePropertiesResult(firstListString, secondListString));
            Assert.IsTrue(ClassCompared.ComparePropertiesResult(firstListStringA, secondListStringA));
        }
    }
}
