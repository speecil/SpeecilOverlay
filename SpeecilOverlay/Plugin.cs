using IPA;
using UnityEngine;
using HarmonyLib;
using IPALogger = IPA.Logging.Logger;
using System.Reflection;
using DataPuller.Data;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;
using System;

namespace SpeecilOverlay
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static bool modEnabled;

        public static string currentSongName;
        public static string currentSongAuthor;
        public static string currentSongTime;
        public static string currentSongTimeEnd;
        //public static Sprite currentSongImage;
        public static string currentAccuracy;
        public static string currentRawScore;
        public static string currentCombo;
        public static bool didMiss = false;
        public static Font loadedFont;
        public static float missTimer = 0;
        
        public void changeMiss()
        {
            didMiss = true;
            missTimer = 0;
        }

        public static TMP_FontAsset loadedFontTMPAsset;
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static bool enabled { get; private set; } = true;

        public static Harmony harmony;

        [Init]
        public void Init(IPA.Config.Config conf, IPALogger logger)
        {
            Instance = this;
            Log = logger;
            harmony = new Harmony("Speecil.BeatSaber.SpeecilOverlay");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            Log.Info("TEST");
            MapData.Instance.OnUpdate += refreshMapValues;
            loadedFont = Font.CreateDynamicFontFromOSFont("Teko", 80);
            Log.Info(loadedFont.name.ToString());
            Log.Info("AFTER FONT LOG");

        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            /*if (arg1.name == "GameCore")
            {
                Log.Info("GAME CORE");
                if (!GameObject.Find("SpeecilOverlayController"))
                {
                    Log.Info("DIDNT FIND MY GAMEOBJECT");
                    return;
                }
                Log.Info("FOUND MY GAMEOBJECT");
                UnityEngine.Object.Destroy(GameObject.Find("SpeecilOverlayController"));
            }*/
        }



        void refreshMapValues(string jsonData)
        {
            currentSongName = MapData.Instance.SongName;
            currentSongAuthor = MapData.Instance.SongAuthor;
            
        }

        [OnEnable]
        public void OnEnable()
        {

            enabled = true;
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        [OnDisable]
        public void OnDisable()
        {
            enabled = false;

            harmony.UnpatchSelf();
        }
    }
}