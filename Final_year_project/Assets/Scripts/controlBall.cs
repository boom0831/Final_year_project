﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class controlBall : NetworkBehaviour
{

    public Rigidbody rb;
    public float movespeed = 5.0f;

    float horizMove;
    float vertMove;
    GMScript gm;

    bool isGameOver = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horizMove = 0.0f;
        vertMove = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        horizMove = Input.GetAxis("Horizontal");
        vertMove = Input.GetAxis("Vertical");        
        //transform.Rotate(0, 0, 300 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 forceToApply = new Vector3(horizMove * movespeed, 0.0f, vertMove * movespeed);
        rb.AddForce(forceToApply);
    }
}
