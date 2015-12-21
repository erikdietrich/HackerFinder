using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Console;

namespace HackerFinder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandLineParser(new FluentCommandLineParser());
            parser.Parse(args);

            var inquisitor = new GithubInquisitor("erikdietrich", Environment.GetEnvironmentVariable("GithubPass", EnvironmentVariableTarget.User));
            var searcher = new ProfileSearcher(inquisitor);

            var candidates = searcher.GetProfilesForLocationByTechnology(parser.Location, parser.Language);

            foreach(var candidate in candidates)
                WriteLine($"Candidate is named {candidate.FirstName} {candidate.LastName} and email address is {candidate.EmailAddress}");

            ReadLine();
        }
    }
}
