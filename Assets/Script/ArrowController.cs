using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody rb;
    void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Ground"))
        {
            if (rb != null)
            {
                rb.isKinematic = true;
                transform.parent = collision.transform;
                Destroy(rb.gameObject,3.0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (rb)
        {
            Gizmos.DrawIcon(transform.position + transform.rotation * rb.centerOfMass, "center");
            //Debug.Log(rb);
        }
    }
}
