using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    //used for other scripts but every obj except root will have this script
    public bool isColObj = false;

    public Transform target;
    public void snapToTarget()
    {
        transform.position = target.position;
        transform.rotation = transform.rotation;
        //transform.localRotation = target.localRotation;
        //transform.position = target.position;
        //transform.rotation = target.rotation;
        //transform.rotation 
        // transform.rotation = Quaternion.Euler(0, 0, target.rotation.z);
    }

    public void setParent(bool runOnce)
    {
        if (runOnce)
        {
            transform.parent = target;
        }
    }
}
