using System.Collections.ObjectModel;
using Handler.Segments;

namespace Handler.Pipelines;

public class Pipeline
{
    public Collection<Segment> InternalFlow;

    public Pipeline(string name, Collection<Segment> internalFlow)
    {
        Name = name;
        InternalFlow = internalFlow;
    }

    public string Name { get; }

    public void OnEventFired()
    {
    }

    public void OnStart()
    {
    }

    public void OnEnd()
    {
    }
}