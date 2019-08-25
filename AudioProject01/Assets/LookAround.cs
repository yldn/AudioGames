using UnityEngine;

public class LookAround : MonoBehaviour {

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
    float wantedRotationAngle;
    float wantedHeight;
    float currentRotationAngle;
    float currentHeight;
    Quaternion currentRotation;
    Quaternion viewRotation = Quaternion.identity;

    CursorLockMode prevCursorState;
    float prevEulerY;
    Vector3 origViewDir;

    public Camera cam;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        float angleYViewOffset = 0;
        float angleXViewOffset = 0;
        //// manipulate view
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    prevCursorState = Cursor.lockState;
        //    Cursor.lockState = CursorLockMode.None;
        //    prevEulerY = transform.eulerAngles.y;

        //    origViewDir = transform.forward;
        //}
        //else if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    Cursor.lockState = prevCursorState;
        //    viewRotation = Quaternion.identity;
        //}

        //Vector3 lookAroundOffset = Vector3.zero;

        Vector3 viewDir = transform.forward;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Vector2 screenRatio = Mathf.Clamp01((Input.mousePosition / new Vector2(cam.pixelWidth, cam.pixelHeight)) - new Vector2(0.5f,0.5f))*2.0f;
            Vector2 screenRatio = new Vector2(Mathf.Clamp01(Input.mousePosition.x / cam.pixelWidth) * 2 - 1, 0);
            Debug.Log(screenRatio);
            angleYViewOffset = screenRatio.x * horizontalLook;
            angleXViewOffset = screenRatio.y * verticalLook;

            //float alreadyPerformedLookAngleY = (transform.eulerAngles.y - prevEulerY);
            //Debug.Log(alreadyPerformedLookAngleY);
            viewRotation = Quaternion.Euler(angleXViewOffset, angleYViewOffset, 0);

            //viewDir = viewRotation * origViewDir;

            //Debug.DrawLine(transform.position, transform.position + viewDir);

            ////angleYViewOffset = Mathf.Max(0, alreadyPerformedLookAngleY - angleYViewOffset);
            ////transform.Rotate(transform.up, angleYViewOffset);
            //viewRotation = Quaternion.FromToRotation(transform.forward, viewDir.normalized);
            ////viewRotation = Quaternion.Slerp(transform.rotation, viewRotation, rotationDamping * Time.deltaTime);
            ///

            transform.rotation = viewRotation;

        }
    }
}
