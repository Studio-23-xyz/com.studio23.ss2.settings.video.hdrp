
using Studio23.SS2.Settings.Video.HDRP.Core;
using System.IO;
using UnityEditor;
using UnityEngine;

public class HDRPGraphicsConfigurationEditor : MonoBehaviour
{
    [MenuItem("Studio-23/Settings/Video/HDRPGraphicsConfiguration", false, 10)]
    static void CreateDefaultProvider()
    {
        HDRPGraphicsSettings graphicsProviderConfig = ScriptableObject.CreateInstance<HDRPGraphicsSettings>();

        // Create the resource folder path
        string resourceFolderPath = "Assets/Resources/Settings/Video/PostProcess";

        if (!Directory.Exists(resourceFolderPath))
        {
            Directory.CreateDirectory(resourceFolderPath);
        }

        // Create the ScriptableObject asset in the resource folder
        string assetPath = resourceFolderPath + "/PostProcess.asset";
        AssetDatabase.CreateAsset(graphicsProviderConfig, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("HDRP Post Process created at: " + assetPath);
    }
}
