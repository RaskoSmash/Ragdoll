using UnityEngine;
using System.Collections;

public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody2D rbody;
    new public BoxCollider2D collider;
    public DistanceJoint2D dist;

    public bool checkForHinge;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        dist = GetComponent<DistanceJoint2D>();
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        originalT = transform;
        if (checkForRigidbody)
        {
            rbody = GetComponent<Rigidbody2D>();
            if (rbody == null)
                gameObject.AddComponent<Rigidbody2D>();
        }
        if (checkForCollider)
        {
            collider = GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                gameObject.AddComponent<BoxCollider2D>();
                Debug.Log("Added box collider");
            }
        }
        if (checkForHinge)
        {
            dist = GetComponent<DistanceJoint2D>();
            if (dist == null)
                gameObject.AddComponent<DistanceJoint2D>();
        }
        checkForHinge = false;
        checkForCollider = false;
        checkForRigidbody = false;
    }

    void OnDisable()
    {
        Destroy(dist);
        Destroy(rbody);
        Destroy(collider);

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
}
