using UnityEngine;

public enum LevelEnd {

    Win,
    Lose

}

public class EndLevel : MonoBehaviour {

    [SerializeField] private LevelEnd LevelEnd;
    private AnimationController Player;

    private void Start() {
        Player = FindObjectOfType<AnimationController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Player.DisableLocomotion = true;
            Player.GetComponent<Rigidbody>().isKinematic = true;
            if (LevelEnd == LevelEnd.Lose) {
                Player.Lose();
            } else if (LevelEnd == LevelEnd.Win) {
                Player.Win();
            }
        }
    }

}
