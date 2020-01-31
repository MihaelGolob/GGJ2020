using UnityEngine;

public class AnimationController : MonoBehaviour {

    private void OnAnimatorMove() {
        var animator = GetComponent<Animator>();
        var rootPosition = animator.rootPosition;
        transform.position = new Vector3(0, rootPosition.y, rootPosition.z);

        var rootRotation = animator.rootRotation;
        transform.rotation = Quaternion.Euler(rootRotation.eulerAngles.x, 0, rootRotation.eulerAngles.z);
    }

}
