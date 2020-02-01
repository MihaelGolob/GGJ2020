using UnityEngine;

public class StartForce : MonoBehaviour {

    public Vector3 Force;

    void Start() {
        GetComponent<Rigidbody>().AddForce(Force, ForceMode.Impulse);
    }

}
