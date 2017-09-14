using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageGeometry.Parser
{
    public class GeometryResponse
    {
        public string Shape { get; set; }
        public string Measurement { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}
