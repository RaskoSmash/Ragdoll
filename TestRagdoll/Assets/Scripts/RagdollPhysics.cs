using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// map<Transform, TransformData> originalTrans;
// originalTrans[trans].Apply(trans);

public class TransformData
{
    Vector3 pos;
    Quaternion rot;
    Vector3 scale;

    public TransformData(Transform target)
    {
        // record da dada
    }

    public void Apply(Transform target)
    {
        // change da dada
    }
}

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
            duringRagdoll();
        }
        else if (!isRagdolling && wasRagdolling)
        {
            afterRagdoll();
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

    void setChildChecks(ChildParts cp, bool d, bool rb, bool c) //rb: rigidbody, c: collider, d: distancejoint
    {
        cp.checkForDist = d;
        cp.checkForRigidbody = rb;
        cp.checkForCollider = c;
    }

    void duringRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            setChildChecks(ot, true, true, true);
            ot.SetEnabled(true);
        }
        wasRagdolling = true;
        anim.enabled = false;
        //anim.applyRootMotion = false;
    }

    void afterRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            ot.SetEnabled(false);
            ot.transform.position = ot.originalT.position;
            //ot.rbody.isKinematic = true;
        }
        wasRagdolling = false;
        anim.enabled = true;
        anim.applyRootMotion = false;
        Debug.Log("false");
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

        //Debug.Log("true");
        anim.enabled = true;
        //anim.applyRootMotion = false;

        Debug.Log(anim.hasRootMotion);

        anim.applyRootMotion = true;

        Debug.Log(anim.hasRootMotion);

        //anim.StopPlayback();
        anim.StartPlayback();
        anim.speed = 1;

        anim.SetTrigger("Reset");

    }
}

//automate child's components []
//proper hinge angular limitations []
//proper resetting after the ragdoll []
//use the planet's normal to find the up( transform.position - closestplanet.transform.position)
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


