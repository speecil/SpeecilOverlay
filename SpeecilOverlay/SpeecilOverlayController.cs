using System;
using System.Threading;
using UnityEngine;

namespace SpeecilOverlay
{
    public class SpeecilOverlayController : MonoBehaviour
    {
        public static SpeecilOverlayController Instance { get; private set; }

        void OnGUI()
        {

            // Define the style for the boxes
            GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
            myBoxStyle.padding = new RectOffset(10, 10, 10, 10);

            // Define the style for the main label
            GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
            myLabelStyle.normal.textColor = Color.white;
            myLabelStyle.font = Plugin.loadedFont;
            myLabelStyle.fontStyle = FontStyle.Bold;
            myLabelStyle.richText = true;

            // Define a smaller style for some labels
            GUIStyle SmallerStyle = new GUIStyle(GUI.skin.label);
            SmallerStyle.normal.textColor = Color.white;
            SmallerStyle.font = Plugin.loadedFont;
            SmallerStyle.fontSize = 60;
            SmallerStyle.fontStyle = FontStyle.Bold;

            // Define the style for the image
            GUIStyle myImageStyle = new GUIStyle(GUI.skin.box);
            myImageStyle.border = new RectOffset(15, 15, 15, 15);
            myImageStyle.normal.background = null;

            // Define the positions of the three boxes
            Rect currentRawScoreTextPos = new Rect(Screen.width - 450, Screen.height - 175, 200, 50);
            Rect currentAccTextPos = new Rect(Screen.width - 450, currentRawScoreTextPos.yMin - 200, 200, 50);
            Rect currentComboTextPos = new Rect(100, Screen.height - 200, 200, 50);

            // Define the texts for the labels
            string currentComboText;
            string currentAccText = $"<size=100>{Plugin.currentAccuracy}</size><size=130>%</size>";
            string currentRawScoreText = $"<size=90>{Plugin.currentRawScore}</size>";
            currentComboText = $"<size=100>x </size><size=120>{Plugin.currentCombo}</size>";
            
            // Set the text to be red if the user has missed
            if (Plugin.didMiss)
            {
                currentComboText = $"<color=red><size=100>x </size><size=120>{Plugin.currentCombo}</size></color>";
            }

            // Create GUIContent objects for each label
            GUIContent currentAccContent = new GUIContent(currentAccText);
            GUIContent currentRawScoreContent = new GUIContent(currentRawScoreText);
            GUIContent currentComboContent = new GUIContent(currentComboText);

            // Calculate the size and position of each label
            Vector2 currentAccSize = myLabelStyle.CalcSize(currentAccContent);
            Rect currentAccLabelPos = new Rect(currentAccTextPos.x, currentAccTextPos.y, currentAccSize.x, currentAccSize.y);
            Vector2 currentRawScoreSize = SmallerStyle.CalcSize(currentRawScoreContent);
            Rect currentRawScoreLabelPos = new Rect(currentRawScoreTextPos.x, currentRawScoreTextPos.y, currentRawScoreSize.x, currentRawScoreSize.y);
            Vector2 currentComboSize = myLabelStyle.CalcSize(currentComboContent);
            Rect currentComboLabelPos = new Rect(currentComboTextPos.x, currentComboTextPos.y, currentComboSize.x, currentComboSize.y);
            
            // Draw the boxes
            GUI.Box(new Rect(currentAccLabelPos.x, currentAccLabelPos.y, currentAccSize.x + myBoxStyle.padding.horizontal, currentAccSize.y + myBoxStyle.padding.vertical), GUIContent.none, myBoxStyle);
            GUI.Box(new Rect(currentRawScoreLabelPos.x, currentRawScoreLabelPos.y, currentRawScoreSize.x + myBoxStyle.padding.horizontal, currentRawScoreSize.y + myBoxStyle.padding.vertical), GUIContent.none, myBoxStyle);
            GUI.Box(new Rect(currentComboLabelPos.x, currentComboLabelPos.y, currentComboSize.x + myBoxStyle.padding.horizontal, currentComboSize.y + myBoxStyle.padding.vertical), GUIContent.none, myBoxStyle);

            // Draw the labels
            GUI.Label(new Rect(currentAccLabelPos.x + myBoxStyle.padding.left, currentAccLabelPos.y + (currentAccLabelPos.height - currentAccSize.y) / 2, currentAccSize.x, currentAccSize.y), currentAccText, myLabelStyle);
            GUI.Label(new Rect(currentRawScoreLabelPos.x + myBoxStyle.padding.left, currentRawScoreLabelPos.y + (currentRawScoreLabelPos.height - currentRawScoreSize.y) / 2, currentRawScoreSize.x, currentRawScoreSize.y), currentRawScoreText, SmallerStyle);
            GUI.Label(new Rect(currentComboLabelPos.x + myBoxStyle.padding.left, currentComboLabelPos.y + (currentComboLabelPos.height - currentComboSize.y) / 2, currentComboSize.x, currentComboSize.y), currentComboText, myLabelStyle);
        }
    }
}
