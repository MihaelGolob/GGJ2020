using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textManager : MonoBehaviour {
    [SerializeField] private Text money;
    private AnimationController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        money.text = player.Coins.ToString();
    }
}
