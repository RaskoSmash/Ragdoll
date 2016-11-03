using UnityEngine;
using System.Collections;

public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody2D rbody;
    new public BoxCollider2D collider;
    public HingeJoint2D hinge;

    public bool checkForHinge;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        hinge = GetComponent<HingeJoint2D>();
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
                gameObject.AddComponent<BoxCollider2D>();
        }
        if (checkForHinge)
        {
            hinge = GetComponent<HingeJoint2D>();
            if (hinge == null)
                gameObject.AddComponent<HingeJoint2D>();
        }
        checkForHinge = false;
        checkForCollider = false;
        checkForRigidbody = false;
    }

    void onDisable()
    {
        Destroy(rbody);
        Destroy(hinge);
        Destroy(collider);
    }
}
