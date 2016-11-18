using UnityEngine;
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
    private Vector2 forceWhenStartRag;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        wasRagdolling = false;
        addChildParts();

        parts = new List<ChildParts>(GetComponentsInChildren<ChildParts>());
    }

    void Update()
    {
        if (isRagdolling && !wasRagdolling)
        {
            startRagdoll();
        }
        else if(isRagdolling && wasRagdolling)
        {
            duringRagdoll();
        }
        else if (!isRagdolling && wasRagdolling)
        {
            afterRagdoll();
        }
        else
        {
            notRagdoll();
        }
    }

    void addChildParts()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Player") && child.GetComponent<ChildParts>() == null && 
                child.transform.parent != null && child.GetComponent<FollowTarget>() != null)
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
            setChildChecks(ot, false, true, true);
            ot.SetEnabled(true);
        }
        wasRagdolling = true;
        anim.enabled = false;
    }

    void duringRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            if (!ot.followTarget.isColObj)
            {
                ot.followTarget.snapToTarget();
            }
        }
    }

    void afterRagdoll()
    {
        foreach (ChildParts ot in parts)
        {
            ot.SetEnabled(false);
        }
        wasRagdolling = false;
        anim.enabled = false;
        anim.applyRootMotion = false;
        StartCoroutine(WaitForRoot());
    }

    void notRagdoll()
    {
        foreach(ChildParts ot in parts)
        {
            if(ot.followTarget.isColObj)
            {
               
                ot.followTarget.snapToTarget();
                //ot.transform.rotation = Quaternion.Euler(0, angleBetween + ot.yRotOffset, 0);
            }
        }
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
//doesnt lerp after ragdoll []
//Hinge not behaving correctly []
//proper hinge angular limitations []
//use the planet's normal to find the up( transform.position - closestplanet.transform.position) []
//need to have Y rotation be the same as the original
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


