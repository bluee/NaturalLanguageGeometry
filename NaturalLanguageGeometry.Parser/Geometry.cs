using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takenet.Textc;
using Takenet.Textc.Csdl;
using Takenet.Textc.Processors;
using Takenet.Textc.Splitters;

namespace NaturalLanguageGeometry.Parser
{
    public class Geometry
    {
        static Task<GeometryResponse> method1(string shape, string measurement, int amount)
        {
            return Task.FromResult(new GeometryResponse(){ Shape = shape, Measurement = measurement, SizeX = amount,  SizeY = amount});
        }
        static Task<GeometryResponse> method2(string shape, string measurement, int amount, string measurement2, int amount2)
        {
            return Task.FromResult(new GeometryResponse() { Shape = shape, Measurement = measurement, SizeX = amount, SizeY = amount2});
        }

        public static ITextProcessor CreateTextProcessor()
        {
            Func<string, string, int, Task<GeometryResponse>> firstFunc = method1;
            Func<string, string, int, string, int, Task<GeometryResponse>> secondFunc = method2;

            //Draw a(n) < shape > with a(n) < measurement > of<amount>(and a(n) < measurement > of
            //    <amount>)

            var firstSyntax =
                CsdlParser.Parse(
                    "operation+:Word(draw) :Word(a,an) shape:LDWord(circle,rectangle,octagon,triangle,heptagon,square,isosceles triangle) :Word?(with) :Word?(a,an) measurement:Word(radius,width,side,height) :Word?(of) amount:Integer");

            var extraArgsSyntax =
                CsdlParser.Parse(
                    "operation+:Word(draw) :Word(a,an) shape:LDWord(circle,rectangle,octagon,triangle,heptagon,square,isosceles triangle) :Word?(with) :Word?(a,an) measurement:Word(radius,width,side,height) :Word?(of) amount:Integer :Word?(and) :Word?(a,an) measurement2:Word(width,height) :Word(of) amount2:Integer");

            // Define a output processor that prints the command results to the console
            var outputProcessor = new DelegateOutputProcessor<GeometryResponse>((o, context) =>
                {
                    //Console.WriteLine($"Result: {o}")
                    context.SetVariable("result", o);
                }

            );

            var firstCommandProcessor = new DelegateCommandProcessor(firstFunc, true, outputProcessor, firstSyntax);
            var secondCommandProcessor = new DelegateCommandProcessor(secondFunc, true, outputProcessor, extraArgsSyntax);

            var textProcessor = new TextProcessor(new PunctuationTextSplitter());
            textProcessor.CommandProcessors.Add(firstCommandProcessor);
            textProcessor.CommandProcessors.Add(secondCommandProcessor);
            //textProcessor.TextPreprocessors.Add(new TrimTextPreprocessor());

            return textProcessor;
        }
    }
}
