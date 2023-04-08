using HarmonyLib;
using UnityEngine;
using DataPuller.Data;
using System.Threading;
using Random = System.Random;
using System;
using TMPro;
using System.Linq;
using SpeecilOverlay;
using System.Runtime.InteropServices;
namespace SpeecilOverlay.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.Start))]
    public class AudioTimeSyncControllerStart
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            Plugin.currentSongTimeEnd = __instance.songEndTime.ToString();
            Plugin.currentCombo = "0";
            Plugin.currentAccuracy = "100";
            SpeecilOverlay.Plugin.Log.Info("audio time sync POST FIX");
            SpeecilOverlay.Plugin.Log.Info(Plugin.currentSongName);

            // Create a new game object
            GameObject myGameObject = new GameObject("SpeecilOverlayController");

            // Attach the LevelGradientsController script to the new game object
            myGameObject.AddComponent<SpeecilOverlayController>();
        }
    }
    [HarmonyPatch(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.Update))]
    public class AudioTimeSyncControllerUpdate
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            Plugin.currentSongTime = __instance.songTime.ToString();
            if(__instance.songTime < 0.1)
            {
                Plugin.currentAccuracy = "100";
            }
        }
    }
    /*
    [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), nameof(StandardLevelScenesTransitionSetupDataSO.Init))]
    public class StandardLevelScenesTransitionSetupDataSOInit
    {
        static async void Postfix(StandardLevelScenesTransitionSetupDataSO __instance)
        {
            Plugin.Log.Info("RUNNING ASYNC POSTFIX");
            Sprite sprite = await __instance.difficultyBeatmap.level.GetCoverImageAsync(CancellationToken.None);
            Plugin.currentSongImage = sprite;
        }
    }*/
    [HarmonyPatch(typeof(RelativeScoreAndImmediateRankCounter), nameof(RelativeScoreAndImmediateRankCounter.HandleScoreDidChange))]
    public class ScoreControllerHandleNoteWasCut
    {
        static void Postfix(RelativeScoreAndImmediateRankCounter __instance)
        {
            Plugin.currentAccuracy = (__instance.relativeScore * 100).ToString("F2");
        }
    }
    [HarmonyPatch(typeof(ScoreController), nameof(ScoreController.LateUpdate))]
    public class ScoreControllerLateUpdate
    {
        static void Postfix(ScoreController __instance)
        {
            Plugin.currentRawScore = __instance.modifiedScore.ToString();
        }
    }
    [HarmonyPatch(typeof(ComboUIController), nameof(ComboUIController.HandleComboDidChange))]
    public class ComboUIControllerHandleComboDidChange
    {
        static void Postfix(ref TextMeshProUGUI ____comboText)
        {
            if(int.Parse(____comboText.text) == 0)
            {
                // TODO: Run thread to hold the text color to be red
                Plugin.Instance.changeMiss();
            }
            Plugin.currentCombo = ____comboText.text;
        }
    }
}