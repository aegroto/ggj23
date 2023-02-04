using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCursorLock : MonoBehaviour {
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
}
