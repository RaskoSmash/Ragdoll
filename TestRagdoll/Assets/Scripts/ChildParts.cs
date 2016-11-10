using UnityEngine;
using System.Collections;

public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody2D rbody;
    new public BoxCollider2D collider;
    public HingeJoint2D dist;

    private bool haveDist;
    public bool checkForDist;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        dist = GetComponent<HingeJoint2D>();
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void SetEnabled(bool isActive)
    {
        if(isActive)
        {
            haveDist = transform.parent.GetComponent<RagdollPhysics>() == null;
            originalT = transform;
            if (checkForRigidbody)
            {
                rbody = GetComponent<Rigidbody2D>();
                if (rbody == null)
                    rbody = gameObject.AddComponent<Rigidbody2D>();
            }
            if (checkForCollider)
            {
                collider = GetComponent<BoxCollider2D>();
                if (collider == null)
                {
                    collider = gameObject.AddComponent<BoxCollider2D>();
                }
            }
            if (checkForDist && haveDist)
            {
                dist = GetComponent<HingeJoint2D>();
                if (dist == null)
                    dist = gameObject.AddComponent<HingeJoint2D>();
            }
            if (haveDist)
            {
                dist.connectedBody = dist.transform.parent.GetComponent<Rigidbody2D>();
                dist.enabled = true;
                dist.connectedAnchor = calParentAnchor();
            }
            collider.enabled = true;
            rbody.isKinematic = false;
        }
        else
        {
            if(haveDist)
                dist.enabled = false;

            collider.enabled = false;
            rbody.isKinematic = true;
            
            checkForDist = false;
            checkForCollider = false;
            checkForRigidbody = false;
        }
    }

    Vector2 calParentAnchor()
    {
        Vector2 retVal = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (transform.position - transform.parent.position).normalized);
        //Vector3 temp = Transform.InverseTransformPoint(new Vector3(hit.point.x,hit.point.y,0));
        //retVal = 
        return retVal;
    }

}
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player") && !isRagdolling)
    //    {
    //        Physics2D.IgnoreCollision(collider, col.collider);
    //    }
    //    else if (col.gameObject.CompareTag("Player") && isRagdolling)
    //    {
    //        Physics2D.IgnoreCollision(collider, col.collider, false);
    //    }
    //}

    /*public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody2D rbody;
    new public BoxCollider2D collider;
    public DistanceJoint2D dist;

    public bool checkForDist;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        dist = GetComponent<DistanceJoint2D>();
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void SetEnabled(bool isActive)
    {
        if(isActive)
        {
            originalT = transform;
            if (checkForRigidbody)
            {
                rbody = GetComponent<Rigidbody2D>();
                if (rbody == null)
                    rbody = gameObject.AddComponent<Rigidbody2D>();
            }
            if (checkForCollider)
            {
                collider = GetComponent<BoxCollider2D>();
                if (collider == null)
                {
                    collider = gameObject.AddComponent<BoxCollider2D>();
                }
            }
            if (checkForDist)
            {
                dist = GetComponent<DistanceJoint2D>();
                if (dist == null)
                    dist = gameObject.AddComponent<DistanceJoint2D>();
            }
            if (transform.parent.GetComponent<RagdollPhysics>() != null)
            {
                dist.connectedAnchor = transform.parent.position;
                //dist.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
            }
            else
            {
                dist.connectedBody = dist.transform.parent.GetComponent<Rigidbody2D>();
            }
        }
        else
        {
            Destroy(dist);
            Destroy(rbody);
            Destroy(collider);
            checkForDist = false;
            checkForCollider = false;
            checkForRigidbody = false;
        }
    }*/
