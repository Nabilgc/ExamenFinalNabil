using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace app.Models
{
    public class Galeb
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
