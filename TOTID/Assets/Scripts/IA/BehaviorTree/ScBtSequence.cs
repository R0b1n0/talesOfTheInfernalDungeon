using System.Collections.Generic;
using UnityEngine.XR;


public class EWSequence : EWNode
{
    protected List<EWNode> nodes = new List<EWNode>();

    public EWSequence(List<EWNode> nodes)
    {
        this.nodes = nodes;
        foreach (var n in nodes)
            n.parent = this;
    }

    public override EWNodeState Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case EWNodeState.RUNNING:
                    anyChildIsRunning = true;
                    break;
                case EWNodeState.SUCCESS:
                    break;
                case EWNodeState.FAILURE:
                    _nodeState = EWNodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = anyChildIsRunning ? EWNodeState.RUNNING : EWNodeState.SUCCESS;

        return _nodeState;
    }
}