using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    public IEnumerator destroy(int time) {
        yield return new WaitForSeconds(time);
        Debug.Log("Selfdestructing");
        Destroy(gameObject);
    }
}
