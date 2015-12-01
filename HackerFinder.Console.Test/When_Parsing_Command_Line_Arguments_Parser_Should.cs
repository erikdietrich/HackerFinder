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

        private const string Location = "Wheeling,IL";
        private const string Language = "C#";

        private static readonly string[] Args = new string[] { "-t", Language, "-l", Location };

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
            Target.Parse(Args);

            Assert.AreEqual<string>(Location, Target.Location);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Set_Language_To_Value_Following_t_Flag()
        {
            Target.Parse(Args);

            Assert.AreEqual<string>(Language, Target.Language);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Throw_Exception_On_Null_Constructor_Argument()
        {
            ExtendedAssert.Throws<ArgumentNullException>(() => new CommandLineParser(null));
        }

    }
}
