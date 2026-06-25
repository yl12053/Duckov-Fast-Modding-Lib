using System.IO;
using HarmonyLib;
using FMOD.Studio;
using Duckov;
using FastModdingLib.Registry;

namespace FastModdingLib.Audio
{
    [HarmonyPatch(typeof(AudioObject), "Post")]
    public class AudioObjectMixin
    {
        public static IRegistry<string, string> audios;
        
        public static bool Prefix(AudioObject __instance, ref EventInstance? __result, string eventName, bool doRelease = true)
        {
            string? path = audios.Get(eventName);
            if (path != null)
            {
                __result = __instance.PostCustomSFX(path, doRelease);
                return false;
            }

            return true;
        }
    }
}