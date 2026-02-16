using BepInEx;
using BepInEx.Bootstrap;
using UnityEngine;
using System.Collections.Generic;

namespace ModLister
{
    [BepInPlugin("com.s3cret.whatsmymods", "whatsmymods", "1.3.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool showMenu = true;
        private Vector2 scrollPos = Vector2.zero;

        // Change this to match the EXACT name of your mod in the BepInPlugin attribute
        private string myModName = "whatsmymods";

        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.Tab)) showMenu = !showMenu;
        }

        void OnGUI()
        {
            if (!showMenu) return;

            // 1. Set the Box and General UI to White
            GUI.color = Color.white;
            GUI.contentColor = Color.white;

            GUI.Box(new Rect(10, 10, 300, 320), $"Active Mods ({Chainloader.PluginInfos.Count})");

            // 2. Main Mod List Area
            GUILayout.BeginArea(new Rect(25, 45, 270, 220));
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            foreach (var plugin in Chainloader.PluginInfos)
            {
                string currentName = plugin.Value.Metadata.Name;
                string currentVersion = plugin.Value.Metadata.Version.ToString();

                // Check if this mod is OUR mod
                if (currentName == myModName)
                {
                    // Turn text Golden Yellow just for this line
                    GUI.contentColor = new Color(1.0f, 0.84f, 0.0f);
                    GUILayout.Label($"> {currentName} v{currentVersion} (YOU)");

                    // Immediately switch back to white for the next mods in the list
                    GUI.contentColor = Color.white;
                }
                else
                {
                    GUILayout.Label($"> {currentName} v{currentVersion}");
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            // 3. Credits Section (Remains White)
            GUI.Box(new Rect(10, 335, 300, 60), "Credits");
            GUI.Label(new Rect(20, 355, 280, 40), "Mod created by: S3cree\nPress TAB to toggle menu");
        }
    }
}