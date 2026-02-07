using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TandrausFix
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class TandrausFix : BaseUnityPlugin
    {
        public static TandrausFix Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "TandrausScene")
            {
                return;
            }
            AudioReverbTrigger[] windTriggers = scene.GetRootGameObjects()[55].transform.Find("ReverbTriggers (1)").Find("WindTriggers").GetComponentsInChildren<AudioReverbTrigger>();

            foreach (AudioReverbTrigger art in windTriggers)
            {
                art.audioChanges = [];
            }
            Transform t = scene.GetRootGameObjects()[54].transform.Find("ItemShipAnimContainer").Find("ItemShip").Find("ItemSpawnPositions").Find("DropshipSpawn (3)");
            t.position += new Vector3(0f, 1f, 0f);
        }
    }
}
