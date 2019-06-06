using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp3;

namespace AgregateTests
{
    /// <summary>
    /// There are two test suites containing three tests each.
    /// Each of the tests follow the pattern of Arrange, Act and Assert, which is
    ///          setting the input data and expected value, 
    ///          executing the function to be tested and
    ///          asserting that the value obtained is the same as expected.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// In this test suite, the function processDataLine() in Aggregate class is tested with various input strings. 
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            System.Console.WriteLine( "UnitTest 1 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            String inputString = "Canada\tWaterloo\tMale\t1240";
            Boolean bExpected = true;

            // Act
            Boolean bActual = a.processDataLine( inputString );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }

        [TestMethod]
        public void TestMethod2()
        {
            System.Console.WriteLine( "UnitTest 2 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            string inputString = "CanadaWaterloo\tMale\t1234";
            Boolean bExpected = false;

            // Act
            Boolean bActual = a.processDataLine( inputString );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }

        [TestMethod]
        public void TestMethod3()
        {
            System.Console.WriteLine( "UnitTest 3 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            string inputString = "CanadaWaterloo\tMale\t123.45";
            Boolean bExpected = false;

            // Act
            Boolean bActual = a.processDataLine( inputString );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }
    }

    /// <summary>
    /// In this test suite, the function processDataFile() in Aggregate class is tested with various data file paths as input.
    /// For the tests to run successfully, the input files must be located in the locations indicated.
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod4()
        {
            System.Console.WriteLine( "UnitTest 4 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            Boolean bExpected = true;

            // Act
            Boolean bActual = a.processDataFile( "C:\\Users\\Public\\inputTest.txt" );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }

        [TestMethod]
        public void TestMethod5()
        {
            System.Console.WriteLine( "UnitTest 5 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            Boolean bExpected = false;

            // Act
            Boolean bActual = a.processDataFile( "C:\\Users\\Public\\BadTestInput1.txt" );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }

        [TestMethod]
        public void TestMethod6()
        {
            System.Console.WriteLine( "UnitTest 6 begins" );

            // Arrange
            Aggregate a = new Aggregate();
            Boolean bExpected = false;

            // Act
            Boolean bActual = a.processDataFile( "C:\\Users\\Public\\BadTestInput2.txt" );

            // Assert
            Assert.AreEqual( bExpected, bActual );
            System.Console.WriteLine( "UnitTest Successfully Executed" );
        }
    }

}