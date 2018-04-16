using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NODESTATE
{
    SUCCESS = 0,
    FAULURE,
    RUNNING
}

public abstract class TreeNode : MonoBehaviour {

    private NODESTATE _nodeState;
    public NODESTATE NodeState { set { _nodeState = value; } get { return _nodeState; } }

    private List<TreeNode> _childNodes;
    public List<TreeNode> ChildNodes { get { return _childNodes; } }

    public virtual void ChildAdd(TreeNode Node)
    {
        ChildNodes.Add(Node);
    }

    public abstract bool Run();

}
