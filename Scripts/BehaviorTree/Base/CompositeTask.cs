using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeTask : TreeNode
{
    public override void ChildAdd(TreeNode node)
    {
        ChildNodes.Add(node);
    }

}
