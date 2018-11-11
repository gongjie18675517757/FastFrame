using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Dto
{
    public class UniqueInput
    {
        public string Id { get; set; }

        public string ModuleName { get; set; }

        public IEnumerable<KeyValuePair<string, string>> KeyValues { get; set; } = new List<KeyValuePair<string, string>>();
    }
}
