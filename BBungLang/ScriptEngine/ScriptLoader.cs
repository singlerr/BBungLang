using System.Collections.ObjectModel;
using System.Text;
using Handler;
using Handler.Pipelines;
using ScriptEngine.Lexer;
using ScriptEngine.Utils;

namespace ScriptEngine;

public class ScriptLoader
{
    public Task<PipelineHandler> LoadScripts(string parentPath)
    {
        return Task.Run(() =>
        {
            var paths = Directory.GetFiles(parentPath, "*.sk", SearchOption.AllDirectories);
            return LoadScripts(paths);
        });
    }

    public PipelineHandler LoadScripts(List<string> lines)
    {
        var pipelines = LoadScript(lines);
        var pipelineHandler = new PipelineHandler();

        foreach (var pipeline in pipelines) pipelineHandler.PutPipeline(pipeline.Name, pipeline);

        return pipelineHandler;
    }

    public PipelineHandler LoadScripts(params string[] paths)
    {
        var pipelineHandler = new PipelineHandler();

        foreach (var path in paths)
        {
            var pipelines = LoadScript(path);
            foreach (var pipeline in pipelines) pipelineHandler.PutPipeline(pipeline.Name, pipeline);
        }

        return pipelineHandler;
    }

    private Collection<Pipeline> LoadScript(List<string> lines)
    {
        var pipelines = new Collection<Pipeline>();
        var bracketCount = new AtomicPositiveInteger();

        var startIdx = 0;
        var endIdx = 0;

        string currentPipelineName = null;

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var buffer = new StringBuilder();
            for (var j = 0; j < line.Length; j++)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var currentChar = line[j];
                if (char.IsWhiteSpace(currentChar))
                    continue;
                if (currentChar == '{')
                {
                    if (bracketCount.Number == 0)
                    {
                        currentPipelineName = buffer.ToString();
                        buffer.Clear();
                        startIdx = i + 1;
                    }

                    bracketCount.Increment();
                }
                else if (currentChar == '}')
                {
                    if (bracketCount.DecrementAndGet() == 0)
                    {
                        endIdx = i - 1;
                        var lexer = new ScriptLexer(lines.GetRange(startIdx, endIdx - startIdx + 1).ToArray());
                        var parser = new ScriptParser.ScriptParser(lexer.Lex());

                        var pipeline = new Pipeline(currentPipelineName, parser.Parse());

                        pipelines.Add(pipeline);
                    }
                }

                if (bracketCount.Number == 0) buffer.Append(currentChar);
            }
        }

        return pipelines;
    }

    private Collection<Pipeline> LoadScript(string path)
    {
        var pipelines = new Collection<Pipeline>();

        var lines = File.ReadAllLines(path).ToList();
        var bracketCount = new AtomicPositiveInteger();

        var startIdx = 0;
        var endIdx = 0;

        string currentPipelineName = null;

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var buffer = new StringBuilder();
            for (var j = 0; j < line.Length; j++)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var currentChar = line[j];
                if (char.IsWhiteSpace(currentChar))
                    continue;
                if (currentChar == '{')
                {
                    if (bracketCount.Number == 0)
                    {
                        currentPipelineName = buffer.ToString();
                        buffer.Clear();
                        startIdx = i + 1;
                    }

                    bracketCount.Increment();
                }
                else if (currentChar == '}')
                {
                    if (bracketCount.DecrementAndGet() == 0)
                    {
                        endIdx = i - 1;
                        var lexer = new ScriptLexer(lines.GetRange(startIdx, endIdx - startIdx + 1).ToArray());
                        var parser = new ScriptParser.ScriptParser(lexer.Lex());

                        var pipeline = new Pipeline(currentPipelineName, parser.Parse());

                        pipelines.Add(pipeline);
                    }
                }

                if (bracketCount.Number == 0) buffer.Append(currentChar);
            }
        }

        return pipelines;
    }
}