using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Dto
{
    public class UniqueInput
    {
        public string Id { get; set; }

        public string ModuleName { get; set; }

        public List<KeyValuePair<string, string>> KeyValues { get; set; } = new List<KeyValuePair<string, string>>();
    }
}
