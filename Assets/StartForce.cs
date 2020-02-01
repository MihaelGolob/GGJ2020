using UnityEngine;

public class StartForce : MonoBehaviour {

    [SerializeField] private Vector3 Force;

    void Start() {
        GetComponent<Rigidbody>().AddForce(Force, ForceMode.Impulse);
    }

}
