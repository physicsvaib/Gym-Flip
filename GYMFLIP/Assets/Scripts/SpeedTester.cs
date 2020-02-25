using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTester : MonoBehaviour
{
    Rigidbody rigid;
    HingeJoint joint;
    public Vector3 spr;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        joint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Testing With Right Click
        if (Input.GetMouseButtonDown(1))
        {
            joint.breakForce = 0;
            Debug.Log(joint.angle);
            StartCoroutine("Delayer");
            //GetComponent<SphereCollider>().radius = 1;
            
        }
        
    }

    // Introducing Delay
    IEnumerator Delayer()
    {
        yield return new WaitForSeconds(41f);

    }


    // On Trigger to Detect if something collided
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hinge" && joint == null)
        {
            Debug.Log("hi");
            StartCoroutine("Delayer");

            joint = this.gameObject.AddComponent<HingeJoint>();
            joint.autoConfigureConnectedAnchor = true;
            joint.connectedAnchor = other.gameObject.transform.position;
            joint.axis = new Vector3(0, 0, 1);
            joint.anchor = new Vector3(0, spr.x, 0);
            joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
            var motor = joint.motor;
            motor.force = spr.z;
            motor.targetVelocity = spr.y;
            motor.freeSpin = false;
            joint.motor = motor;
            joint.useMotor = true;


        }
    }
}
