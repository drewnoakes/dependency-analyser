using System;
using System.Text;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class DependencyAnalyserTest : DependencyAnalyserBase
    {
//
//        [Test]
//        public void testGenerateDotCommand()
//        {
//            _projectInfoList = new ProjectInfo[2];
//            _projectInfoList[0] = new ProjectInfo("ProjectOne.csproj", "ProjectOne", "A");
//            _projectInfoList[1] = new ProjectInfo("ProjectTwo.csproj", "ProjectTwo", "B");
//
//            _projectInfoList[0].DependantProjectNames.Add("ProjectTwo");
//
//            Assertion.AssertEquals(@"digraph G {
//    A -> B;
//    A [label=""ProjectOne""];
//    B [label=""ProjectTwo""];
//}", GenerateDotCommand());
//        }
//
//        [Test]
//        public void testGenerateDotCommand_ExtraCommands()
//        {
//            _projectInfoList = new ProjectInfo[2];
//            _projectInfoList[0] = new ProjectInfo("ProjectOne.csproj", "ProjectOne", "A");
//            _projectInfoList[1] = new ProjectInfo("ProjectTwo.csproj", "ProjectTwo", "B");
//
//            _projectInfoList[0].DependantProjectNames.Add("ProjectTwo");
//            _projectInfoList[1].DependantProjectNames.Add("ProjectOne");
//
//            string extraCommands = @"size=""7,10"" page=""8.5,11"" center="""" node[width=.25,hight=.375,fontsize=9]";
//            string expected = @"digraph G {
//    " + extraCommands + @"
//    A -> B;
//    B -> A;
//    A [label=""ProjectOne""];
//    B [label=""ProjectTwo""];
//}";
//            Assertion.AssertEquals(expected, GenerateDotCommand(extraCommands));
//        }
        
        [Test]
        public void AddAndGetMessages()
        {
            Messages = new StringBuilder();
            Assertion.AssertEquals(String.Empty, GetMessages());
            AddMessage("Test1");
            Assertion.AssertEquals("Test1", GetMessages());
            AddMessage("Test2");
            Assertion.AssertEquals("Test1\r\nTest2", GetMessages());
        }

        [Test]
        public void AddAndGetMessages_Formatted()
        {
            Messages = new StringBuilder();
            Assertion.AssertEquals(String.Empty, GetMessages());
            AddMessage("{0} {1}", "hinkle", "pinkle");
            Assertion.AssertEquals("hinkle pinkle", GetMessages());
        }
    }
}
