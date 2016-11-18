#region 3D
/*
using UnityEngine;
using System.Collections;
public class TransformData
{
    Vector3 desiredPos;
    Quaternion desiredRot;
    Vector3 desiredScale;

    public TransformData(Transform target)
    {
        // record da dada
    }

    public void Apply(Transform target)
    {
        // change da dada
    }
}

public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody rbody;
    new public BoxCollider collider;
    public HingeJoint dist;

    private bool haveDist;
    public bool checkForDist;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        originalT = transform;
        dist = GetComponent<HingeJoint>();
        rbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        haveDist = transform.parent.GetComponent<RagdollPhysics>() == null;
    }

    public void SetEnabled(bool isActive)
    {
        if (isActive)
        {
            originalT = transform;
            if (checkForRigidbody)
            {
                rbody = GetComponent<Rigidbody>();
                if (rbody == null)
                    rbody = gameObject.AddComponent<Rigidbody>();
            }
            if (checkForCollider)
            {
                collider = GetComponent<BoxCollider>();
                if (collider == null)
                    collider = gameObject.AddComponent<BoxCollider>();
            }
            if (checkForDist && haveDist)
            {
                dist = GetComponent<HingeJoint>();
                if (dist == null)
                {
                    dist = gameObject.AddComponent<HingeJoint>();
                    dist.connectedBody = dist.transform.parent.GetComponent<Rigidbody>();
                    dist.connectedAnchor = transform.localPosition;
                }
            }
            collider.enabled = true;
            rbody.isKinematic = false;
        }
        else
        {
            if (haveDist)
                Destroy(dist);

            collider.enabled = false;
            rbody.isKinematic = true;

            checkForDist = false;
            checkForCollider = false;
            checkForRigidbody = false;
        }
    }

    #region CommentedCode

    //Vector2 calParentAnchor()
    //{
    //    Vector2 retVal = Vector2.zero;
    //    //Vector2 direction = transform.parent.position - transform.position;
    //    //Debug.DrawLine(transform.position, new Vector3(direction.x,direction.y,0),Color.magenta,0.1f);
    //    //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude);

    //    //Debug.Log(transform.parent ==f hit.transform);        
    //    //Debug.Log(hit.transform);


    //    ////Vector3 temp = transform.InverseTransformPoint(new Vector3(hit.point.x, hit.point.y, 0));
    //    ////retVal = new Vector2(temp.x, temp.y);
    //    //retVal = hit.point;


    //    retVal = transform.localPosition;
    //    return retVal;
    //}
    #endregion
}
*/
#endregion

#region 2D

 using UnityEngine;
using System.Collections;
public class TransformData
{
    Vector3 desiredPos;
    Quaternion desiredRot;
    Vector3 desiredScale;

    public TransformData(Transform current)
    {
        // record da dada
        desiredPos = current.localPosition;
        desiredRot = current.localRotation;
        desiredScale = current.localScale;
    }

    //how do i handle lerp if this only gets called once
        //lerp should just work
    public void Apply(Transform current)
    {
        // change da dada
        current.localRotation = Quaternion.Lerp(current.localRotation, desiredRot,.01f);
    }
}

public class ChildParts : MonoBehaviour
{
    public Transform originalT;
    public Rigidbody2D rbody;
    new public BoxCollider2D collider;
    public HingeJoint2D dist;
    private TransformData tdata;

    private bool haveDist;
    public bool checkForDist;
    public bool checkForRigidbody;
    public bool checkForCollider;

    void Start()
    {
        dist = GetComponent<HingeJoint2D>();
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
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
            Debug.Log(col.gameObject.name);
            Physics2D.IgnoreCollision(collider, col.collider);
        }
    }

    public void SetEnabled(bool isActive)
    {
        if (isActive)
        {
            haveDist = transform.parent.GetComponent<RagdollPhysics>() == null;
            tdata = new TransformData(transform);
            if (checkForRigidbody)
            {
                rbody = GetComponent<Rigidbody2D>();
                if (rbody == null)
                    rbody = gameObject.AddComponent<Rigidbody2D>();
            }
            if (checkForDist && haveDist)
            {
                dist = GetComponent<HingeJoint2D>();
                if (dist == null)
                {
                    dist = gameObject.AddComponent<HingeJoint2D>();
                    dist.connectedBody = dist.transform.parent.GetComponent<Rigidbody2D>();
                    dist.connectedAnchor = transform.localPosition;
                }
            }
            //if (haveDist)
            //{
            //    dist.enabled = true;
            //}
            collider.enabled = true;
            rbody.isKinematic = false;
        }
        else
        {
            //if (haveDist)
            //    dist.enabled = false;

            collider.enabled = false;
            rbody.isKinematic = true;

            checkForDist = false;
            checkForCollider = false;
            checkForRigidbody = false;
            tdata.Apply(transform);
        }
    }

    #region CommentedCode

    //Vector2 calParentAnchor()
    //{
    //    Vector2 retVal = Vector2.zero;
    //    //Vector2 direction = transform.parent.position - transform.position;
    //    //Debug.DrawLine(transform.position, new Vector3(direction.x,direction.y,0),Color.magenta,0.1f);
    //    //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude);

    //    //Debug.Log(transform.parent ==f hit.transform);        
    //    //Debug.Log(hit.transform);


    //    ////Vector3 temp = transform.InverseTransformPoint(new Vector3(hit.point.x, hit.point.y, 0));
    //    ////retVal = new Vector2(temp.x, temp.y);
    //    //retVal = hit.point;


    //    retVal = transform.localPosition;
    //    return retVal;
    //}
    #endregion
}

#endregion
