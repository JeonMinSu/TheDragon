using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : CompositeTask {

    public override bool Run()
    {
        foreach (TreeNode child in ChildNodes)
        {
            if (!child.Run())
            {
                return false;
            }
        }
        return true;
    }


}
