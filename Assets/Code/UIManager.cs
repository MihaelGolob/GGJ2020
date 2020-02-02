using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private Text _coinCounter;

    private void Start() {
        _coinCounter = GetComponentInChildren<Text>();
    }

    private void Update() {
        _coinCounter.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
