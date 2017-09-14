using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Takenet.Textc;
using Takenet.Textc.Csdl;
using Takenet.Textc.Processors;

namespace NaturalLanguageGeometry.Parser
{
    public static class NLPGP
    {
        public static GeometryResponse Parse(string textStr)
        {
            
            ITextProcessor textProcessor;

            textProcessor = Geometry.CreateTextProcessor();

            var context = new RequestContext();
            
            //string inputText = "Draw a circle with a radius of 100";
            //inputText = "Draw a rectangle with a width of 250 and a height of 400";


            try
            {
                textProcessor.ProcessAsync(textStr, context, CancellationToken.None);
            }
            catch (MatchNotFoundException)
            {
                Console.WriteLine("There's no match for the specified input");
            }

            return (GeometryResponse)context.GetVariable("result");
        }

    }
}
