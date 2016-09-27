using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonCharsInLines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCharsInLines.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        List<char> result, expected;
        List<string> example;

        void calculateAndCheck()
        {
            result = Program.FindCommonCharsInLines_AddFirstLineThenIntersectWithOtherLines(example);

            Assert.AreEqual(expected.Count, result.Count);

            foreach (var c in expected)
            {
                Assert.IsTrue(result.Contains(c));
            }
        }

        [TestMethod()]
        public void successScenario1()
        {
            example = new List<string>() {
                "Lorem ipsum",
                "dolor sit amet,",
                "consectetur",
                "adipisci velit"
            };

            expected = new List<char>() { 's', 'e' };

            calculateAndCheck();
        }

        [TestMethod()]
        public void successScenario2()
        {
            example = new List<string>() {
                "Ali",
                "Veli",
                "Elli",
                "Deli"
            };

            expected = new List<char>() { 'l', 'i' };

            calculateAndCheck();
        }

        [TestMethod()]
        public void numbers()
        {
            example = new List<string>() {
                "When the chips are down",
                "We don't like it",
                "123456"
            };

            expected = new List<char>() { };

            calculateAndCheck();
        }

        [TestMethod()]
        public void singleLine()
        {
            example = new List<string>() {
                "When the "
            };

            expected = new List<char>() { 'w', 'h', 'e', 'n', ' ', 't' };

            calculateAndCheck();
        }

        [TestMethod()]
        public void empty()
        {
            example = new List<string>() { };

            expected = new List<char>() { };

            calculateAndCheck();
        }

        [TestMethod()]
        public void lowerCaseUpperCaseMixed()
        {
            example = new List<string>() {
                "Lorem ipsum",
                "dolor Sit amet,",
                "consectEtur",
                "adipisci velit"
            };

            expected = new List<char>() { 's', 'e' };

            calculateAndCheck();
        }
    }
}