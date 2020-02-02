using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int Keys { get; set; }

    private void Start() {
        Keys = 0;
    }
}
