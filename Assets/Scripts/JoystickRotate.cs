using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickRotate : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform objectToRotate;
    public float rotationSpeed = 30f;

    private Vector2 joystickInput;

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            // Get joystick input using new Input System
            joystickInput = Gamepad.current?.leftStick.ReadValue() ?? Vector2.zero;
            
            // Rotate the object based on joystick input
            RotateObject();
        }
    }

    void RotateObject()
    {
        // Calculate rotation angle based on joystick input
        float rotationAngle = joystickInput.x * rotationSpeed * Time.deltaTime;

        // Rotate the object around its up vector
        objectToRotate.Rotate(Vector3.up, rotationAngle);
    }
}
