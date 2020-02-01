using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "selectable";

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;
            
            if (selection.CompareTag(selectableTag)) {

                if (Input.GetMouseButtonDown(0)) {
                    var button = selection.GetComponent<Button>();
                    StartCoroutine(button.click());
                }
                
            }
        }
    }
}
