using System.IO;
using HarmonyLib;
using Steamworks;

namespace Reactor.Patches
{
    internal static class SteamPatch
    {
        public static void Initialise()
        {
            const string file = "steam_appid.txt";

            if (!File.Exists(file))
            {
                File.WriteAllText(file, "945360");
            }
        }
    }
}
