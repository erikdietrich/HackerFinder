using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerFinder.Domain
{
    public class Repository
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string Sha { get; set; }
        public IList<string> Files { get; set; } = new List<string>();

        public string DownloadUrl
        {
            get { return Url + "/archive/master.zip"; }
        }
        public void AddFile(string filePath)
        {
            Files.Add(filePath);
        }
    }
}
