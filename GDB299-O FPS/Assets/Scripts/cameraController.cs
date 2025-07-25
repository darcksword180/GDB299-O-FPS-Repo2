using UnityEngine;

public class cameraController : MonoBehaviour
{
    //camera settings
    [SerializeField] int senitivity;
    [SerializeField] int lockVerticalMin, lockVerticalMax;
    [SerializeField] bool invertY;

    //camera direction variable
    float rotateX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //lock mouse movement to respective axis
        float mouseY = Input.GetAxis("Mouse Y") * senitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * senitivity * Time.deltaTime;

        if (invertY)
        {
            rotateX += mouseY;
        }
        else
        {
            rotateX -= mouseY;
        }

        rotateX = Mathf.Clamp(rotateX, lockVerticalMin, lockVerticalMax);

        transform.localRotation = Quaternion.Euler(rotateX, 0, 0);

        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
