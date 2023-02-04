using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other) {
    //    if(other.tag == "Enemy") {
    //        other.GetComponent<Enemy>().AddDamage(1);

    //    }
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().AddDamage(1);
    }
}
