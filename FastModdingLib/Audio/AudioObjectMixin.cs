using Duckov;
using FMOD.Studio;
using HarmonyLib;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace FastModdingLib.Audio
{
    [HarmonyPatch(typeof(AudioObject), nameof(AudioObject.Post))]
    public class AudioObjectMixin
    {
        [HarmonyPrefix]
        public static bool Prefix(AudioObject __instance, ref EventInstance? __result, string eventName, bool doRelease)
        {
            Debug.Log($"AudioObjectMixin: Post called with eventName: {eventName}");
            if (eventName.Contains(':'))
            {
                string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = eventName.Substring(eventName.IndexOf(":")+1);
                Debug.Log($"AudioObjectMixin:{modDirectory}:{path}");
                string assetLoc = $"assets/sounds/{path}.ogg";
                string fileLoc = Path.Combine(modDirectory, assetLoc.ToString());
                if (File.Exists(fileLoc) == true)
                {
                    __result = __instance.PostCustomSFX(fileLoc, false);
                    return false;
                }
                Debug.Log("File Not Found :"+ fileLoc);
            }
            return true;
           
        }
    }
}