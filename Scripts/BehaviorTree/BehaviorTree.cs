using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "BehaviorTree/Tree")]
public class BehaviorTree : ScriptableObject {
    
    [SerializeField]
    private TreeNode _root;
    public TreeNode Root { get { return _root; } }

    private Selection test;

    public void OnEnable()
    {
        if (_root.ChildNodes != null) { 
            if (_root.ChildNodes.Count == 0) { 
                _root.hideFlags = HideFlags.None;
                SerializeNodes(_root);
            }
        }
    } 

    public void SerializeNodes(TreeNode node)
    {
        TreeNode[] ChildNodes = node.GetComponentsInChildren<TreeNode>();

        for (int i = 1; i < ChildNodes.Length; i++)
        {
            int n = i;
            i += ChildNodes[i].GetComponentsInChildren<TreeNode>().Length - 1;
            node.ChildAdd(ChildNodes[n]);
            SerializeNodes(ChildNodes[n]);
        }
    }

    public static BehaviorTree Create()
    {
        BehaviorTree asset = CreateInstance<BehaviorTree>();
        
        AssetDatabase.CreateAsset(asset, "Assets/BehaviroTree.asset");
        AssetDatabase.SaveAssets();

        return asset;
    }
}