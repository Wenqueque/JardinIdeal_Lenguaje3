using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Vector3 additionalRotation;

    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        //transform.LookAt(cameraTransform);
        transform.rotation *= Quaternion.Euler(additionalRotation);

        Vector3 cameraPosition = Camera.main.transform.position;
        transform.LookAt(new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z));

    }

}