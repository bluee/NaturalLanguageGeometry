using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NaturalLanguageGeometry.API.Models
{
    public class ParseRequest
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}