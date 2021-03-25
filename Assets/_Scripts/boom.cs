using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Interactive Ball")
        {
            _rb.AddForce(Vector3.up * 1000 * Time.deltaTime, ForceMode.Impulse);
        }
    }

}
