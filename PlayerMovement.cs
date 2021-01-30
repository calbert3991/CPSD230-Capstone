using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    public GroundCheck groundCheck; 

    [SerializeField]
    private float walkSpeed = 12f, sprintFactor = 1.4f;

    [SerializeField]
    private float gravity = -9.81f, jumpHeight = 3f;

    [SerializeField]
    private float characterHeight = 1.8f, crouchFactor = 0.6f, timeToCrouch = 1f;

    Vector3 velocity;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update() {
        if (groundCheck.isGrounded) {
            if (velocity.y < 0) {
                velocity.y = -3f;
            }

            if (Input.GetButtonDown ("Jump")) {
                velocity.y = Mathf.Sqrt (jumpHeight * -2f * gravity);
            }

            float crouchOffset = (0.5f * characterHeight) * (1 - crouchFactor);
            if (Input.GetKey (KeyCode.LeftControl) ) {
                controller.height = Mathf.Lerp(characterHeight, characterHeight * crouchFactor, timeToCrouch);
                transform.position = new Vector3 (transform.position.x, transform.position.y - crouchOffset, transform.position.z);
            } else {
                controller.height = Mathf.Lerp (characterHeight * crouchFactor, characterHeight, timeToCrouch);
                transform.position = new Vector3(transform.position.x, transform.position.y + crouchOffset, transform.position.z);
            }
        }

        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey (KeyCode.LeftShift) && groundCheck.isGrounded) {
            move *= sprintFactor;
        }
        controller.Move (move * walkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move (velocity * Time.deltaTime);
    }
}
