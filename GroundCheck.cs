using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float groundDistance = 0.4f;

    [SerializeField]
    public bool isGrounded { get; private set; }


    void Update() {
        isGrounded = Physics.CheckSphere (groundCheck.position, groundDistance, groundMask);
    }



}
