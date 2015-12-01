using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Console
{
    public class CommandLineParser
    {
        private readonly IFluentCommandLineParser _parser;

        public string Location { get; set; }

        public string Language { get; set; }

        public CommandLineParser(IFluentCommandLineParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            _parser = parser;
        }

        public void Parse(string[] commandLineArgs)
        {
            SetupString('l', l => Location = l);
            SetupString('t', t => Language = t);

            _parser.Parse(commandLineArgs);
        }

        private void SetupString(char flag, Action<string> assignment)
        {
            _parser.Setup<string>(flag).Callback(assignment);
        }
    }
}
