using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Vector3 relativePosXZ = Vector3.ProjectOnPlane(relativePos, Vector3.up);

        Quaternion lookRotation = Quaternion.LookRotation(relativePosXZ);

        transform.rotation = lookRotation;
    }
}
