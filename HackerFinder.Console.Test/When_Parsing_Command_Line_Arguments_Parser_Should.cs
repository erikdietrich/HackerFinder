using Fclp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerFinder.Console.Test
{
    [TestClass]
    public class When_Parsing_Command_Line_Arguments_Parser_Should
    {
        private CommandLineParser Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new CommandLineParser(new FluentCommandLineParser());   
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Initialize_Location_To_Null()
        {
            Assert.IsNull(Target.Location);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Set_Location_To_Value_Following_l_Flag()
        {
            const string location = "Wheeling, IL";

            var args = new string[] { "-l", location };

            Target.Parse(args);

            Assert.AreEqual<string>(location, Target.Location);
        }

    }
    public class CommandLineParser
    {
        private readonly IFluentCommandLineParser _parser;
        public string Location { get; set; }

        public CommandLineParser(IFluentCommandLineParser parser)
        {
            _parser = parser;
        }

        public void Parse(string[] commandLineArgs)
        {
            _parser.Setup<string>('l').Callback(l => Location = l);

            _parser.Parse(commandLineArgs);
        }
    }
}
