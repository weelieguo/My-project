using System;
using System.Collections.Generic;
using UnityEngine;
using TiltFive.Logging;
using TiltFive;
using UnityEngine.InputSystem;

public class CubeMover : MonoBehaviour
{
    public float speed = 0.1f;
    public float direction;
    public float degree;
    Vector2 res;

    // Update is called once per frame
    void Update()
    {
        // float xdirection = Input.GetAxis("Horizontal");
        // float ydirection = Input.GetAxis("Vertical");

        // Vector3 moveDirection = new Vector3(xdirection,0.0f,ydirection);

        if (TiltFive.Input.GetWandAvailability()){
            res = TiltFive.Input.GetStickTilt();
            direction = res.x;
            degree = res.y;
            float xdirection = res.x;
            float ydirection = res.y;
            Vector3 moveDirection = new Vector3(xdirection,0.0f,ydirection);
            transform.position += moveDirection * speed;
        }
        else{
            transform.position = transform.position;
        }
    }
}
