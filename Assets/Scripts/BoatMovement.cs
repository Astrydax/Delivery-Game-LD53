using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Rigidbody[] rbs;
    public Rigidbody rb;
    public Quaternion startRot;

    public Transform[] powerSources;
    private Transform powerSource;

    public float steerPower = 500f;
    public float thrust = 5f;
    public float maxSpeed = 10f;
    public float drag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        SetPowerSource(0);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forceDirection = transform.forward;
        int steer = 0;

        if (Input.GetKey(KeyCode.A))
        {
            steer = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = -1;
        }


        rb.AddForceAtPosition(steer * transform.right * steerPower / 100f, powerSource.position);
        //this.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        //this.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0f);


        Vector3 forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        if (Input.GetKey(KeyCode.W))
        {
            ApplyForceToReachVelocity(rb, forward * maxSpeed, thrust);
        }
        if(Input.GetKey(KeyCode.S)) 
        {
            ApplyForceToReachVelocity(rb, forward * -maxSpeed, thrust);
        }
    }

    public void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
    public void SetPowerSource(int id)
    {
        powerSource = powerSources[id];
    }
}
