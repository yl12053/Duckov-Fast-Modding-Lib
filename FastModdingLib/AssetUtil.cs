using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace FastModdingLib
{
    public static class AssetUtil
    {
        public static Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle>();
        public static AssetBundle? LoadBundle(string modPath, string bundleName)
        {
            string resourceLoc = $"{modPath}:{bundleName}";
            if (loadedBundles.ContainsKey(resourceLoc)) {
                Debug.Log($"AssetBundle {bundleName} is already loaded.");
                Debug.Log($"Load {bundleName} from Dictionary {loadedBundles[resourceLoc]}.");
                return loadedBundles[resourceLoc];
            }
            string modDirectory = Path.GetDirectoryName(modPath);
            StringBuilder assetLoc = new StringBuilder($"assets/bundle/");
            assetLoc.Append(bundleName);
            string fileLoc = Path.Combine(modDirectory, assetLoc.ToString());

            var assetBundle
                = AssetBundle.LoadFromFile(fileLoc);
            if (assetBundle == null)
            {
                Debug.Log($"Failed to load AssetBundle {bundleName}!");
                return null;
            }
            Debug.Log($"Load {bundleName} from File {assetBundle}.");
            loadedBundles.Add(resourceLoc, assetBundle);
            return assetBundle;
        }

    }
}
