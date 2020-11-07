using UnityEngine;


public class CameraController : MonoBehaviour
{

    public Transform targetPosition;
    public float targetOrthoSize;
    
    void Update()
    {
        float finalSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthoSize, 1.5f * Time.deltaTime);
        Camera.main.orthographicSize = finalSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //we use this because we do not want to alter the camera Z position
        Vector3 finalPos = Vector3.Lerp(transform.position, targetPosition.position, 3 * Time.fixedDeltaTime);
        finalPos.z = transform.position.z;
        transform.position = finalPos;
    }
}
