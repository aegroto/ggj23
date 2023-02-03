using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {
    [SerializeField]
    GameObject target;

    Vector3 offset;

    Vector3 targetPosition;

    void Start(){
        this.offset = transform.position - target.transform.position;
    }

    void Update() {
        this.targetPosition = target.transform.position + offset;
        
        RaycastHit hit;
        bool hasHit = Physics.Raycast(target.transform.position, this.targetPosition, out hit);
        if(hasHit) {
            this.targetPosition = hit.point;
        }

    }

    void FixedUpdate() {
        transform.LookAt(target.transform);
        transform.position = Vector3.Lerp(transform.position, this.targetPosition, 0.5f);
    }
}
