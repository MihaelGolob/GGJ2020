#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class StaticEditorScript {
    [MenuItem("Custom/Clear Player Preferences")]
    private static void ClearPlayerPreferences() {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Custom/Give me 300 coins")]
    private static void GiveMe300Coins() {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 300);
    }
}
#endif