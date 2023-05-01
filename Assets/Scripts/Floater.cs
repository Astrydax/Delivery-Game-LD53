using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    private void Start()
    {
        //if(rb == null) rb = GetComponent<Rigidbody>();
        //if(rb.drag == 0) rb.drag = 0.01f;
    }


    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01( (waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

}
