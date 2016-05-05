using _1920Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace _1920ParserTest
{
    
    
    /// <summary>
    ///This is a test class for SchemaTest and is intended
    ///to contain all SchemaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SchemaTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Parser Constructor
        ///</summary>
        [TestMethod()]
        public void SchemaConstructorTest()
        {
            string schema = string.Empty; // TODO: Initialize to an appropriate value
            Schema target = new Schema();
        }


        /// <summary>
        ///Just a  GroupNode
        ///</summary>
        [TestMethod()]
        public void parseTest1()
        {
            //string schema = @"01 Daten                          Kommentar";

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex : 1, comment: "");
            GroupNode daten =   new GroupNode(redefines : false, level : 1, varName : "Daten", repeatCount : 1, repeatIndex: 1, comment : "Kommentar");
            root.AddChild(daten);
            var actual = new Schema().Parse();
            Assert.AreEqual(root, actual);
        }




        /// <summary>
        ///Just a ValueNode
        ///</summary>
        [TestMethod()]
        public void parseTest2()
        {
            //string schema = @"05 Nachname    C  4      4 Byte langer Nachname";

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            ValueNode nachname = new ValueNode(redefines: false, level: 5, varName: "Nachname", type : "C", length : 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");

            root.AddChild(nachname);
            Assert.AreEqual(root, new Schema().Parse());
        }






        /// <summary>
        ///GroupNode with a child
        ///</summary>
        [TestMethod()]
        public void parseTest3()
        {
//            string schema = @"
//01 Daten                         Kommentar
//    05 Nachname    C  4      4 Byte langer Nachname";

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            GroupNode daten = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");
            ValueNode nachname = new ValueNode(redefines: false, level: 5, varName: "Nachname", type : "C", length : 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");


            daten.AddChild(nachname);
            root.AddChild(daten);
            Assert.AreEqual(root, new Schema().Parse());
        }


        /// <summary>
        ///Repeating GroupNode
        ///</summary>
        [TestMethod()]
        public void parseTest4()
        {
//            string schema = @"
//01 Daten                    2    Kommentar
//    05 Vorname     C  5      5 Bytes vom Typ char mit Namen ”Vorname”
//    05 Nachname    C  4      4 Byte langer Nachname";

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            GroupNode daten1 = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 2, repeatIndex: 1, comment: "Kommentar");
            ValueNode daten1_vorname = new ValueNode(redefines: false, level: 5, varName: "vorname", type: "C", length: 5, repeatCount: 1, repeatIndex: 1, comment: "5 Bytes vom Typ char mit Namen ”Vorname”");
            ValueNode daten1_nachname = new ValueNode(redefines: false, level: 5, varName: "Nachname", type : "C", length : 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");
            GroupNode daten2 = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 2, repeatIndex: 2, comment: "Kommentar");
            ValueNode daten2_vorname = new ValueNode(redefines: false, level: 5, varName: "vorname", type: "C", length: 5, repeatCount: 1, repeatIndex: 1, comment: "5 Bytes vom Typ char mit Namen ”Vorname”");
            ValueNode daten2_nachname = new ValueNode(redefines: false, level: 5, varName: "Nachname", type : "C", length : 4, repeatCount: 1, repeatIndex: 1, comment: "4 Byte langer Nachname");

            daten1.AddChild(daten1_vorname);
            daten1.AddChild(daten1_nachname);
            root.AddChild(daten1);
            daten2.AddChild(daten2_vorname);
            daten2.AddChild(daten2_nachname);
            root.AddChild(daten2);
            GroupNode actual = new Schema().Parse();
            Assert.AreEqual(root, actual);
        }


        /// <summary>
        ///A test for parse
        ///</summary>
        [TestMethod()]
        public void parseTest5()
        {
//            string schema = @"
//01 Daten                         Kommentar";

            GroupNode root = new GroupNode(redefines: false, level: 0, varName: "root", repeatCount: 1, repeatIndex: 1, comment: "");
            GroupNode daten = new GroupNode(redefines: false, level: 1, varName: "Daten", repeatCount: 1, repeatIndex: 1, comment: "Kommentar");

            root.AddChild(daten);
            Assert.AreEqual(root, new Schema().Parse());
        }

        /// <summary>
        ///A test for parse
        ///</summary>
        [TestMethod()]
        public void parseTest6()
        {
//            string schema = @"
//01 Daten                         Kommentar
//    03 Personendaten             Kommentar
//        05 Vorname     C  5      5 Bytes vom Typ char mit Namen ”Vorname”
//        05 Nachname    C  4      4 Byte langer Nachname
//    R03 Gesamter-Name  C  9      Redefiniert den Bereich zuvor, hat die gleichen Werte
//    03 Bestellungen          2   Ein ”Array”, Länge 2
//        05 Artikelnr   N  4      4 Byte lange Artikelnummer";

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

            Assert.AreEqual(root, new Schema().Parse());
        }
    }
}
