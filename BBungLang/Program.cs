// See https://aka.ms/new-console-template for more information

using Handler.FlowContext;
using ScriptEngine;
using ScriptEngine.Lexer;
using ScriptEngine.ScriptParser;

if (args.Length <= 0)
{
    throw new Exception("컴파일 할 파일 이름을 입력하십시오.");
}

var lines = File.ReadAllLines(args[0]);

var scriptLoader = new ScriptLoader();
var pipelineHandler = scriptLoader.LoadScripts(lines.ToList());

foreach (var registeredPipeline in pipelineHandler.GetRegisteredPipelines())
{
    var iter = registeredPipeline.InternalFlow.GetEnumerator();
    while (iter.MoveNext())
    {
        var seg = iter.Current;
        seg.Execute(new Context());
    }
}
