using _1920Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _1920ParserTest
{
    
    
    /// <summary>
    ///This is a test class for GroupNodeTest and is intended
    ///to contain all GroupNodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GroupNodeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///GroupNodes with equal attribute values are equal, if all their children are equal too
        ///</summary>
        [TestMethod()]
        public void GroupNodesWithEqualValuesAndEqualChildrenAreEqualTest()
        {
            bool redefines = false;
            int level = 0;
            string varName = string.Empty;
            int repeatCount = 1;
            int repeatIndex = 1;
            string comment = string.Empty;
            GroupNode parent1 = new GroupNode(redefines, level, varName, repeatCount, repeatIndex, comment);
            GroupNode child1 = new GroupNode(false, 5, "child", 3, 3, "");
            ValueNode grandChild1 = new ValueNode(false, 10, "grandchild", "C", 5, 1, 1, "");
            child1.AddChild(grandChild1);
            parent1.AddChild(child1);
            GroupNode parent2 = new GroupNode(redefines, level, varName, repeatCount, repeatIndex, comment);
            GroupNode child2 = new GroupNode(false, 5, "child", 3, 3, "");
            ValueNode grandChild2 = new ValueNode(false, 10, "grandchild", "C", 5, 1, 1, "");
            child2.AddChild(grandChild2);
            parent2.AddChild(child2);
            Assert.IsTrue(parent1.Equals(parent2));
        }


        /// <summary>
        ///GroupNodes with equal attribute values are equal, if all their children are equal too
        ///</summary>
        [TestMethod()]
        public void GroupNodesWithEqualValuesAndDifferentChildrenAreNotEqualTest()
        {
            bool redefines = false;
            int level = 0;
            string varName = string.Empty;
            int repeatCount = 1;
            int repeatIndex = 1;
            string comment = string.Empty;
            GroupNode parent1 = new GroupNode(redefines, level, varName, repeatCount, repeatIndex, comment);
            GroupNode child1 = new GroupNode(false, 5, "child", 3, 3, "");
            ValueNode grandChild1 = new ValueNode(false, 10, "grandchild", "C", 5, 1, 1, "");
            child1.AddChild(grandChild1);
            parent1.AddChild(child1);
            GroupNode parent2 = new GroupNode(redefines, level, varName, repeatCount, repeatIndex, comment);
            GroupNode child2 = new GroupNode(false, 5, "child", 3, 3, "");
            ValueNode grandChild2 = new ValueNode(false, 1000, "grandchild", "C", 5, 1, 1, "");
            child2.AddChild(grandChild2);
            parent2.AddChild(child2);
            Assert.IsFalse(parent1.Equals(parent2));
        }

        /// <summary>
        ///A test for AssignValue
        ///</summary>
        [TestMethod()]
        public void GrouptAssignValueTest()
        {
            /*string schema = @"
01 Daten                         Kommentar
    03 Personendaten             Kommentar
        05 Vorname     C  5      5 Bytes vom Typ char mit Namen ”Vorname”
        05 Nachname    C  4      4 Byte langer Nachname
    R03 Gesamter-Name  C  9      Redefiniert den Bereich zuvor, hat die gleichen Werte
    03 Bestellungen          2   Ein ”Array”, Länge 2
        05 Artikelnr   N  4      4 Byte lange Artikelnummer";*/

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            GroupNode daten = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");
            GroupNode personenDaten = new GroupNode(redefines: false, level: 3, varName: "Personendaten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");
            ValueNode vorname = new ValueNode(redefines: false, level: 5, varName: "Vorname", type: "C", length: 5, repeatCount: 1, repeatIndex: 1, comment: "5 Bytes vom Typ char mit Namen ”Vorname”");
            ValueNode nachname = new ValueNode(redefines: false, level: 5, varName: "Nachname", type: "C", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");
            ValueNode gesamterName = new ValueNode(redefines: true, level: 3, varName: "Gesamter-Name", type: "C", length: 9, repeatCount: 1, repeatIndex: 1, comment: "Redefiniert den Bereich zuvor, hat die gleichen Werte");
            GroupNode bestellungen1 = new GroupNode(redefines: false, level: 3, varName: "Bestellungen", repeatCount: 2, repeatIndex: 1, comment: "Ein ”Array”, Länge 2");
            ValueNode bestellungen1_artikelnr = new ValueNode(redefines: false, level: 5, varName: "Artikelnr", type: "N", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte lange Artikelnummer");
            GroupNode bestellungen2 = new GroupNode(redefines: false, level: 3, varName: "Bestellungen", repeatCount: 2, repeatIndex: 2, comment: "Ein ”Array”, Länge 2");
            ValueNode bestellungen2_artikelnr = new ValueNode(redefines: false, level: 5, varName: "Artikelnr", type: "N", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte lange Artikelnummer");

            personenDaten.AddChild(vorname);
            personenDaten.AddChild(nachname);
            daten.AddChild(personenDaten);
            root.AddChild(personenDaten);
            root.AddChild(gesamterName);
            bestellungen1.AddChild(bestellungen1_artikelnr);
            root.AddChild(bestellungen1);
            bestellungen2.AddChild(bestellungen2_artikelnr);
            root.AddChild(bestellungen2);


            GroupNode rootA = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            GroupNode datenA = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");
            GroupNode personenDatenA = new GroupNode(redefines: false, level: 3, varName: "Personendaten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");
            ValueNode vornameA = new ValueNode(redefines: false, level: 5, varName: "Vorname", type: "C", length: 5, repeatCount: 1, repeatIndex: 1, comment: "5 Bytes vom Typ char mit Namen ”Vorname”");
            ValueNode nachnameA = new ValueNode(redefines: false, level: 5, varName: "Nachname", type: "C", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");
            ValueNode gesamterNameA = new ValueNode(redefines: true, level: 3, varName: "Gesamter-Name", type: "C", length: 9, repeatCount: 1, repeatIndex: 1, comment: "Redefiniert den Bereich zuvor, hat die gleichen Werte");
            GroupNode bestellungen1A = new GroupNode(redefines: false, level: 3, varName: "Bestellungen", repeatCount: 2, repeatIndex: 1, comment: "Ein ”Array”, Länge 2");
            ValueNode bestellungen1_artikelnrA = new ValueNode(redefines: false, level: 5, varName: "Artikelnr", type: "N", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte lange Artikelnummer");
            GroupNode bestellungen2A = new GroupNode(redefines: false, level: 3, varName: "Bestellungen", repeatCount: 2, repeatIndex: 2, comment: "Ein ”Array”, Länge 2");
            ValueNode bestellungen2_artikelnrA = new ValueNode(redefines: false, level: 5, varName: "Artikelnr", type: "N", length: 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte lange Artikelnummer");

            personenDatenA.AddChild(vornameA);
            personenDatenA.AddChild(nachnameA);
            datenA.AddChild(personenDatenA);
            rootA.AddChild(personenDatenA);
            rootA.AddChild(gesamterNameA);
            bestellungen1A.AddChild(bestellungen1_artikelnrA);
            rootA.AddChild(bestellungen1A);
            bestellungen2A.AddChild(bestellungen2_artikelnrA);
            rootA.AddChild(bestellungen2A);
            vornameA.AssignValue("vn~~~");
            nachnameA.AssignValue("nn~~");
            gesamterNameA.AssignValue("vn~~~nn~~");
            bestellungen1_artikelnrA.AssignValue("1234");
            bestellungen2_artikelnrA.AssignValue("5678");

            root.AssignValue("vn~~~nn~~12345678");
            Assert.AreEqual(root, rootA);
        }
    }
}
