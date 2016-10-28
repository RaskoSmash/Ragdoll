using UnityEngine;
using System.Collections;

public class ChildParts : MonoBehaviour
{
    public Transform originalT;

    public Rigidbody2D rbody;

    new public Collider2D collider;

    public HingeJoint2D hinge;

    public bool checkForHinge;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        hinge = GetComponent<HingeJoint2D>();
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        originalT = transform;
        if(checkForRigidbody)
        {
            
        }
    }
}
