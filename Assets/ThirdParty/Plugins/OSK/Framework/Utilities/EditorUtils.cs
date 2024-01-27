using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class EditorUtils 
{
    public static void ReplaceNameFile(Object asset, string newName)
    {
        string assetPath = AssetDatabase.GetAssetPath(asset.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, newName);
        AssetDatabase.SaveAssets();
    }
}

#endif
