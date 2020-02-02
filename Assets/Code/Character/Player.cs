using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int Keys {
        get { return PlayerPrefs.GetInt("Key"); }
        set { PlayerPrefs.SetInt("Key", value); }
    }

    private void Start() {
        Keys = 0;
        PlayerPrefs.SetInt("Key", Keys);
    }
}
