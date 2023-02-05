using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField]
    private GameObject pg;
    // Start is called before the first frame update
    void Start()
    {
        pg = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempRotation = transform.rotation.eulerAngles;
        transform.LookAt(pg.transform.position);
        transform.rotation = Quaternion.Euler(tempRotation.x, transform.rotation.eulerAngles.y, tempRotation.z);
    }
}
