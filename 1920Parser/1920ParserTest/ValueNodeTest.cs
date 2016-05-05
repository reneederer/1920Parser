using _1920Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _1920ParserTest
{
    
    
    /// <summary>
    ///This is a test class for ValueNodeTest and is intended
    ///to contain all ValueNodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ValueNodeTest
    {
        /// <summary>
        ///ValueNodes with equal attribute values are equal
        ///</summary>
        [TestMethod()]
        public void ValueNodesWithSamAeattributeValuesAreEqual()
        {
            bool redefines = false;
            int level = 0;
            string varName = string.Empty;
            string type = "C";
            int length = 4;
            int repeatCount = 1;
            int repeatIndex = 1;
            string comment = string.Empty;
            ValueNode node1 = new ValueNode(redefines, level, varName, type, length, repeatCount, repeatIndex, comment);
            ValueNode node2 = new ValueNode(redefines, level, varName, type, length, repeatCount, repeatIndex, comment);
            Assert.IsTrue(node1.Equals(node2));
        }


        /// <summary>
        ///ValueNodes with 1 or more different attribute values are not equal
        ///</summary>
        [TestMethod()]
        public void ValueNodesWithDifferentAttributeValuesAreNotEqual()
        {
            bool redefines = false;
            int level = 0;
            string varName = string.Empty;
            string type = "C";
            int length = 4;
            int repeatCount = 1;
            int repeatIndex = 1;
            string comment = string.Empty;
            ValueNode node1 = new ValueNode(redefines, level, varName, type, length, repeatCount, repeatIndex, comment);
            ValueNode node2 = new ValueNode(redefines, level + 1, varName, type, length, repeatCount, repeatIndex, comment);
            Assert.IsFalse(node1.Equals(node2));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ValueNode node = new ValueNode(false, 3, "varName", "C", 6, 1, 1, "Kommentar");
            node.AssignValue("hithereiamsometestdata");
            var expected = "3 varName=hither\r\n";
            var actual = node.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest1()
        {
            GroupNode root = new GroupNode(false, 0, "root", 1, 1, "comment");
            ValueNode child1 = new ValueNode(false, 3, "child1", "C", 6, 1, 1, "Kommentar");
            ValueNode child2 = new ValueNode(false, 3, "child2", "C", 2, 1, 1, "Kommentar");
            ValueNode child3 = new ValueNode(true, 3, "child3", "C", 3, 1, 1, "Kommentar");
            ValueNode child4 = new ValueNode(false, 3, "child4", "C", 6, 1, 1, "Kommentar");
            root.AddChild(child1);
            root.AddChild(child2);
            root.AddChild(child3);
            root.AddChild(child4);
            root.AssignValue("hithereiamsometestdata");
            var expected = "0 root\r\n    3 child1=hither\r\n    3 child2=ei\r\n   R3 child3=eia\r\n    3 child4=amsome\r\n";
            var actual = root.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
