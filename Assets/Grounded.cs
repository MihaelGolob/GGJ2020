using UnityEngine;

public class Grounded : MonoBehaviour {

    public bool IsGrounded { get; private set; } = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 8) {
            IsGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other) {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        IsGrounded = false;
    }

}
