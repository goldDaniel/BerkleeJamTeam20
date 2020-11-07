using UnityEngine;

public class CameraController : MonoBehaviour
{

    //the transform the camera is going to move towards
    public Transform target;


    // Update is called once per frame
    void FixedUpdate()
    {
        //we use this because we do not want to alter the camera Z position
        Vector3 finalPos = Vector3.Lerp(transform.position, target.position, 3 * Time.fixedDeltaTime);
        finalPos.z = transform.position.z;
        transform.position = finalPos;
    }
}
