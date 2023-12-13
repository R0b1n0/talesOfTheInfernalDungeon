using System.Collections.Generic;
using UnityEngine.XR;

public class EWSelector : EWNode
{
    protected List<EWNode> nodes = new List<EWNode>();

    public EWSelector(List<EWNode> nodes)
    {
        this.nodes = nodes;
        foreach (var n in nodes)
            n.parent = this;
    }

    public override EWNodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case EWNodeState.RUNNING:
                    _nodeState = EWNodeState.RUNNING;
                    return _nodeState;
                case EWNodeState.SUCCESS:
                    _nodeState = EWNodeState.SUCCESS;
                    return _nodeState;
                case EWNodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        _nodeState = EWNodeState.FAILURE;
        return _nodeState;
    }
}