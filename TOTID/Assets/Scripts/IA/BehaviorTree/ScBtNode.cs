using System.Collections.Generic;

[System.Serializable]
public abstract class EWNode
{
    public EWNode parent = null;

    protected EWNodeState _nodeState;
    public EWNodeState nodeState { get { return _nodeState; } }

    public abstract EWNodeState Evaluate();


    private Dictionary<string, object> _data = new Dictionary<string, object>();

    public object GetData(string key)
    {
        if (parent != null)
            return parent.GetData(key);

        if (_data.ContainsKey(key))
            return _data.GetValueOrDefault(key);

        return null;
    }

    public void SetData(string key, object value)
    {
        if (parent != null)
            parent.SetData(key, value);
        else
        {
            if (!_data.ContainsKey(key))
                _data.Add(key, value);
            else
                _data[key] = value;
        }
    }
}

public enum EWNodeState
{
    RUNNING, SUCCESS, FAILURE,
}
