using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : CompositeTask {
    
    public override bool Run()
    {
        foreach (TreeNode child in ChildNodes)
        {
            if (child.Run())
            {
                return true;
            }
        }
        return false; 
    }
}
