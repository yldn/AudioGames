using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public bool shouldRotate = true;

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    // How much we can look around in degree
    public float horizontalLook = 35.0f;
    public float verticalLook = 15.0f;
    float wantedRotationAngleY;
    float wantedHeight;
    float currentRotationAngleY;
    float currentHeight;
    Quaternion currentRotation;
    Quaternion viewRotation = Quaternion.identity;
    float angleYViewOffset = 0;
    float angleXViewOffset = 0;

    CursorLockMode prevCursorState;
    Vector3 origViewDir;
    Vector3 viewTargetOffset = Vector3.zero;

    Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (target)
        {
            // manipulate view
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                prevCursorState = Cursor.lockState;
                Cursor.lockState = CursorLockMode.None;

                origViewDir = transform.position - target.position;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Cursor.lockState = prevCursorState;
                viewTargetOffset = Vector3.zero;
            }

            Vector3 lookAroundOffset = Vector3.zero;

            Vector3 viewDir = transform.forward;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 screenRatio = new Vector2(Mathf.Clamp01(Input.mousePosition.x / cam.pixelWidth) * 2 - 1,
                    Mathf.Clamp01(Input.mousePosition.y / cam.pixelHeight) * 2 - 1);

                angleYViewOffset = screenRatio.x * horizontalLook;
                angleXViewOffset = screenRatio.y * verticalLook;

                viewRotation = Quaternion.Euler(angleXViewOffset, angleYViewOffset, 0);

                viewDir = viewRotation * origViewDir.normalized;

                viewTargetOffset = target.rotation * new Vector3(angleYViewOffset, angleXViewOffset, 0);
            }


            // Calculate the current rotation angles
            wantedRotationAngleY = target.eulerAngles.y;
            wantedHeight = target.position.y + height;
            currentRotationAngleY = transform.eulerAngles.y;
            currentHeight = transform.position.y;
            // Damp the rotation around the y-axis
            currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDamping * Time.deltaTime);
            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // Convert the angle into a rotation
            currentRotation = Quaternion.Euler(0, currentRotationAngleY, 0);
            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;

            transform.position -= currentRotation * Vector3.forward * distance;
            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            if (shouldRotate)
                transform.LookAt(target.position + viewTargetOffset);

        }

    }
}
