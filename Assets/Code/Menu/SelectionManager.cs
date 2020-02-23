using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "selectable";

    private SoundManager _sound;

    private void Start() {
        _sound = FindObjectOfType<SceneConfiguration>().SoundManager;
        _sound.PlayClip("mainMenu", 1, true);
    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;
            
            if (selection.CompareTag(selectableTag)) {

                if (Input.GetMouseButtonDown(0)) {
                    var button = selection.GetComponent<MenuButton>();
                    StartCoroutine(button.click());
                }
                
            }
        }
    }
}
