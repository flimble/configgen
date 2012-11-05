using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Rhino.Mocks;

namespace SAIG.PS.ConfigGen.UnitTest
{
    [TestFixture]
    public class TokenLocatorTest
    {
        

        [SetUp]
        public void TestInitialize()
        {

        }


        [Test]
        public void Single_Token_Is_Found()
        {
            //arrange
            string sourceTemplate =
                @"<?xml version=""1.0""?>
                         <configuration xmlns=""http://schemas.microsoft.com/.NetConfiguration/v2.0"">
                            <appSettings>
                                <add key=""CommonDBConnectionString"" value=""[%Token1%]"" />
                            </appSettings>
                         </configuration>";

            var underTest = new TokenIdentifier();

            underTest.SearchForTokens(sourceTemplate);
            var result = underTest.TokensFound;

            Assert.AreEqual(1, result.Count);

        }


        [Test]
        public void Multiple_Tokens_Are_Found()
        {
            //arrange
            string sourceTemplate =
                @"<?xml version=""1.0""?>
                         <configuration xmlns=""http://schemas.microsoft.com/.NetConfiguration/v2.0"">
                            <appSettings>
                                <add key=""CommonDBConnectionString"" value=""[%Token1%]"" />
                                <add key=""CommonDBConnectionString"" value=""[%Token2%]"" />
                                <add key=""CommonDBConnectionString"" value=""[%Token3%]"" />
                            </appSettings>
                         </configuration>";

            var underTest = new TokenIdentifier();
            underTest.SearchForTokens(sourceTemplate);

            var result = underTest.TokensFound;

            Assert.AreEqual(3, result.Count);

            Assert.AreEqual(result[0], @"[%Token1%]");
            Assert.AreEqual(result[1], @"[%Token2%]");
            Assert.AreEqual(result[2], @"[%Token3%]");

        }

        [Test]
        public void Duplicate_Tokens_Returned_Once()
        {
            //arrange
            string sourceTemplate =
                @"<?xml version=""1.0""?>
                         <configuration xmlns=""http://schemas.microsoft.com/.NetConfiguration/v2.0"">
                            <appSettings>
                                <add key=""CommonDBConnectionString"" value=""[%Token1%]"" />
                                <add key=""CommonDBConnectionString"" value=""[%Token1%]"" />
                            </appSettings>
                         </configuration>";

            var underTest = new TokenIdentifier();

            underTest.SearchForTokens(sourceTemplate);
            var result = underTest.TokensFound;

            Assert.AreEqual(1, result.Count);

        }

    }

        
}
