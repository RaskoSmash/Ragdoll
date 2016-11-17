﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// map<Transform, TransformData> originalTrans;
// originalTrans[trans].Apply(trans);



public class RagdollPhysics : MonoBehaviour
{
    private List<ChildParts> parts;
    public bool isRagdolling;
    public bool wasRagdolling;
    private Animator anim;
    new private Collider2D collider;
    private Vector2 forceWhenStartRag;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        wasRagdolling = false;
        collider = GetComponent<Collider2D>();
        addChildParts();

        parts = new List<ChildParts>(GetComponentsInChildren<ChildParts>());
    }

    void Update()
    {
        if (isRagdolling && !wasRagdolling)
        {
            startRagdoll();
        }
        else if (!isRagdolling && wasRagdolling)
        {
            afterRagdoll();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //    if (col.gameObject.CompareTag("Player") && !isRagdolling)
        //    {
        //        Physics2D.IgnoreCollision(collider, col.collider);
        //    }
        //    else if (col.gameObject.CompareTag("Player") && isRagdolling)
        //    {
        //        Physics2D.IgnoreCollision(collider, col.collider, false);
        //    }
        //I need a way to still collide with self but not the master parent
        if (col.gameObject.CompareTag("Player")) 
        {
            Physics2D.IgnoreCollision(collider, col.collider);
        }
    }

    void addChildParts()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Player") && child.GetComponent<ChildParts>() == null && 
                child.transform.parent != null)
            {
                child.gameObject.AddComponent<ChildParts>();
            }
        }
    }

    void setChildChecks(ChildParts cp, bool d, bool rb, bool c) //rb: rigidbody, c: collider, d: distancejoint
    {
        cp.checkForDist = d;
        cp.checkForRigidbody = rb;
        cp.checkForCollider = c;
    }

    void startRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            setChildChecks(ot, true, true, true);
            ot.SetEnabled(true);
        }
        wasRagdolling = true;
        anim.enabled = false;
    }

    void afterRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            ot.SetEnabled(false);
            ot.transform.position = ot.originalT.position;
        }
        wasRagdolling = false;
        anim.enabled = false;
        anim.applyRootMotion = false;
        StartCoroutine(WaitForRoot());
    }

    //IEnumerator WaitForRoot() {
    //    Debug.Log("wait");
    //    yield return new WaitForEndOfFrame();
    //}

    IEnumerator WaitForRoot()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }

        yield return new WaitForFixedUpdate();
        anim.enabled = true;
        anim.applyRootMotion = true;
        anim.StartPlayback();
        anim.speed = 1;

        anim.SetTrigger("Reset");
    }

    
}
//BUGS{
//collide with parented boxcollider while in rag [fixed]
//revert needs a desired rot,scale,pos to blend to after ragdoll []

//proper hinge angular limitations []
//use the planet's normal to find the up( transform.position - closestplanet.transform.position) []
//old model works "YAY"

//
//
//}

/*
 bool isActive
	kinematic RB
	center mass, mass
	transforms of limbs
	vec2 forceAppliedWhenRagdollActivates;

	public static doRagdoll(Player playRef);

	addRigidbodies
	addColliders
	addHinges
	hingeJoint 2D

*/


