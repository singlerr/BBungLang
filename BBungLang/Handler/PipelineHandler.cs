using Handler.Pipelines;

namespace Handler;

public class PipelineHandler
{
    private readonly Dictionary<string, Pipeline> _registeredPipelines;

    public PipelineHandler()
    {
        _registeredPipelines = new Dictionary<string, Pipeline>();
    }

    public bool PipelineExists(string name)
    {
        return _registeredPipelines.ContainsKey(name);
    }

    public Pipeline GetPipeline(string name)
    {
        return PipelineExists(name) ? _registeredPipelines[name] : null;
    }

    public void PutPipeline(string name, Pipeline pipeline)
    {
        _registeredPipelines[name] = pipeline;
    }

    public Dictionary<string, Pipeline>.ValueCollection GetRegisteredPipelines()
    {
        return _registeredPipelines.Values;
    }
}