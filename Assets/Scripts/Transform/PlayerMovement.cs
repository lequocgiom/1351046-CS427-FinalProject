﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f, bankingValue = 90f;
    public Transform visualChild;
    private Camera cam;
    private Rigidbody myRb;
    private float distance;
    private Vector3 velocity, lastPosition, rotation, touchPos, screenToWorld;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        myRb = GetComponent<Rigidbody>();

        distance = (cam.transform.position - transform.position).y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        Move();

        lastPosition = transform.position;
    }

    void Move()
    {
#if UNITY_EDITOR
        touchPos = Input.mousePosition;

#elif UNITY_ANDROID
        touchPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)touchPos;
 
#elif UNITY_STANDALONE_WIN
        touchPos = Input.mousePosition;
#endif
        touchPos.z = distance;

        screenToWorld = cam.ScreenToWorldPoint(touchPos);

        Vector3 movement = Vector3.Lerp(myRb.position, screenToWorld, speed * Time.fixedDeltaTime);

        myRb.MovePosition(movement);

        rotation.z = -velocity.x * bankingValue;
        myRb.MoveRotation(Quaternion.Euler(rotation));
    }
}
