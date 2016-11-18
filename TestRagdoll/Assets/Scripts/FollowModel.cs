using UnityEngine;
using System.Collections;

public class FollowModel : MonoBehaviour {

    public Transform target;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 1f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, target.rotation.z);
    }
}
