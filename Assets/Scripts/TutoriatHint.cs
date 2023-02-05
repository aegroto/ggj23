using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoriatHint : MonoBehaviour {
    [SerializeField]
    private string text;

    [SerializeField]
    private TextMeshProUGUI label;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            label.text = text;
        }
    }
}
