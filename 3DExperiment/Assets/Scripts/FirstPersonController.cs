using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    private Camera mainCamera;

    //Mouse
    [SerializeField] private bool invertMouse = false;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float lookUpConstraint;
    [SerializeField] private float lookDownConstraint;
    private float verticalRotationLimit;

    //Movement
    public float moveSpeed;
    public float runSpeed;
    private CharacterController charController;
    private Vector3 movement;
    // Start is called before the first frame update
    private void Start()
    {
        charController=GetComponent<CharacterController>();
        mainCamera=Camera.main;
    }
    private void FixedUpdate()
    {
        //Mouse input
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //Rotates the player horizontally based on Mouse x input
        float yRotation = transform.rotation.eulerAngles.y + mouseInput.x;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);

        //Mouse invert prefrence
        float invert = (!invertMouse) ? -1f : 1f;
        verticalRotationLimit += (mouseInput.y * invert);

        //Limit x axis rotation based on input
        verticalRotationLimit = Mathf.Clamp(verticalRotationLimit, lookDownConstraint, lookUpConstraint);

        //Applies rotation to the camera target
        cameraTarget.rotation = Quaternion.Euler(verticalRotationLimit, cameraTarget.eulerAngles.y, cameraTarget.eulerAngles.z);

        //User input for moving 
        Vector3 moveForward = transform.forward*Input.GetAxisRaw("Vertical");
        Vector3 moveRight = transform.right*Input.GetAxisRaw("Horizontal");

        //Set movement speed based on if the player is holding down left shift
        float currentSpeed=(Input.GetKey(KeyCode.LeftShift)) ? runSpeed : moveSpeed;

        //Generate movement
        movement=(moveForward+moveRight).normalized*currentSpeed;
        charController.Move(movement*Time.deltaTime);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        mainCamera.transform.position=cameraTarget.position;
        mainCamera.transform.rotation=cameraTarget.rotation;
    }
}
