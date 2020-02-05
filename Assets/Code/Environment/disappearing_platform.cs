using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class disappearing_platform : MonoBehaviour {
    [SerializeField] private float transitionTime;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;
    
    private bool _triggered;
    private Vector3 _startScale;
    
    void Start() {
        _startScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other) {
        if(!_triggered && other.CompareTag("Player"))
            StartCoroutine(disappear());
    }

    IEnumerator disappear() {
        _triggered = true;
        var scale = transform.localScale;
        for (float i = 1f; i > 0f; i -= 0.01f) {
            if(xAxis)
                scale = new Vector3(_startScale.x*i, scale.y, scale.z);
            if(yAxis)
                scale = new Vector3(scale.x, _startScale.y*i, scale.z);
            if(zAxis)
                scale = new Vector3(scale.x, scale.y, _startScale.z*i);

            transform.localScale = scale;
            yield return new WaitForSeconds(transitionTime / 100.0f);
        }
        Destroy(gameObject);
    }
}
