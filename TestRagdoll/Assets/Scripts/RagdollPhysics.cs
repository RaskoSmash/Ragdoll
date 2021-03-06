﻿using UnityEngine;
using System.Collections.Generic;

public class RagdollPhysics : MonoBehaviour
{
    private List<ChildParts> parts;
    public bool isRagdolling;
    public bool wasRagdolling;
    private Animator anim;
    new private Collider2D collider;

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
            foreach (ChildParts ot in parts)
            {

            }

            collider.enabled = false;
            wasRagdolling = true;
            anim.enabled = false;
        }
        else if (!isRagdolling && wasRagdolling)
        {
            foreach (ChildParts ot in parts)
            {
                ot.transform.position = ot.originalT.position;
                ot.rbody.isKinematic = true;
                ot.collider.enabled = false;
                ot.enabled = false;
            }
            //wasRagdolling = false;
            //anim.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isRagdolling)
        {
            Physics2D.IgnoreCollision(collider, col.collider);
        }
        else if (col.gameObject.CompareTag("Player") && isRagdolling)
        {
            Physics2D.IgnoreCollision(collider, col.collider, false);
        }
    }

    void addChildParts()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Player") && child.GetComponent<ChildParts>() == null && child.transform.parent != null)
            {
                child.gameObject.AddComponent<ChildParts>();
            }
        }
    }

    void setChildChecks(bool h, bool rb, bool c) //rb: rigidbody, c: collider, h: hinges
    {
        foreach(ChildParts cp in parts)
        {
            cp.checkForHinge = h;
            cp.checkForRigidbody = rb;
            cp.checkForCollider = c;
        }
    }
}

//one universal box collider during !isRagdolling

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

        private List<ChildParts> parts;
    public bool isRagdolling;
    public bool wasRagdolling;
    private Animator anim;
    new public Collider2D collider;

    void Start()
    {
        parts = new List<ChildParts>(GetComponentsInChildren<ChildParts>());
        anim = GetComponent<Animator>();
        wasRagdolling = false;
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isRagdolling && !wasRagdolling)
        {
            foreach (ChildParts ot in parts)
            {
                ot.enabled = true;
                ot.rbody.isKinematic = false;
                ot.collider.enabled = true;
            }
            Collider2D playerBox = GetComponentInParent<Collider2D>();
            playerBox.enabled = false;
            wasRagdolling = true;
            anim.enabled = false;
        }
        else if (!isRagdolling && wasRagdolling)
        {
            foreach (ChildParts ot in parts)
            {
                ot.transform.position = ot.originalT.position;
                ot.rbody.isKinematic = true;
                ot.collider.enabled = false;
                ot.enabled = false;
            }
            wasRagdolling = false;
            anim.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && !isRagdolling)
        {
            Physics2D.IgnoreCollision(collider, col.collider);
        }
        else if(col.gameObject.CompareTag("Player") && isRagdolling)
        {
            Physics2D.IgnoreCollision(collider, col.collider, false);
        }
    }

     */


