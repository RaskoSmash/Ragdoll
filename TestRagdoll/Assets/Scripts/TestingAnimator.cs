using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingAnimator : MonoBehaviour
{
    private RagdollPhysics rag;

    // Use this for initialization
    void Start()
    {
        rag = GetComponent<RagdollPhysics>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") || Input.GetKey("e"))
        {
            rag.isRagdolling = true;
        }
        else
            rag.isRagdolling = false;
    }
}

/*
    private Rigidbody2D rbody;
    private Vector3 ogCOM;
    public bool doTest;
   // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        ogCOM = rbody.centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        if(doTest)
        {
            rbody.centerOfMass = ogCOM;
        }
    }

*/
