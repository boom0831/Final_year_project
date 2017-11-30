﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolCue : MonoBehaviour {

    playerManager playerMan;
    GMScript gm;
    GameObject cueBall;
    GameObject pivot;
    GameObject cue;
    public Rigidbody rb;
    private Vector3 cueOffset;
    private Vector3 cRotate = new Vector3(0f, 15f, 0f);
    private Vector3 cueRotOffset = new Vector3(0f, 90f, 0f);
    private Vector3 cuePosOffset = new Vector3(0f, 0.5f, -5f);
    private Vector3 ballRotation;
    float speed = 150f;
    bool reset = true;
    bool canHit = true;
    Quaternion quaternion;

    // Use this for initialization
    void Start () {
        playerMan = playerManager.playerMan;
        gm = GMScript.gameMan;
    }
	
	// Update is called once per frame
	void Update () {
        cueBall = GameObject.Find("cueBall(Clone)");
        pivot = GameObject.Find("cuePivot");
        cue = GameObject.Find("poolCue");

        if (cueBall != null && pivot != null)
        {
            rb = cueBall.GetComponent<Rigidbody>();

            pivot.transform.position = new Vector3(cueBall.transform.position.x, cueBall.transform.position.y, cueBall.transform.position.z);
            transform.LookAt(cueBall.transform.position + cueRotOffset);

            if (reset == true)
            {
                if (Mathf.Abs(rb.velocity.x) < 0.01f && Mathf.Abs(rb.velocity.y) < 0.2f && Mathf.Abs(rb.velocity.z) < 0.01f)
                {
                    cue.GetComponent<MeshRenderer>().enabled = true;
                    transform.position = pivot.transform.position + cuePosOffset;
                    reset = false;
                    canHit = true;

                    if(gm.GetPlayerHasPot() == false)
                    {
                        gm.CallEndTurnEvent();
                    }
                }
                else
                {
                    cue.GetComponent<MeshRenderer>().enabled = false;
                    reset = true;
                    canHit = false;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(pivot.transform.position, Vector3.up, speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(pivot.transform.position, -Vector3.up, speed * Time.deltaTime);
            }
        }
    }

    public void Fire(float power)
    {
        if (canHit == true)
        {
            cue.GetComponent<MeshRenderer>().enabled = false;
            BallAim(power);

        }
    }

    private void BallAim(float power)
    {
        cueBall.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        StartCoroutine(Hit(power));
    }
    
    private IEnumerator Hit(float power)
    {
        yield return new WaitForSeconds(0.01f);

        rb.AddRelativeForce(new Vector3(0f, 0f, power), ForceMode.Impulse);
        reset = true;
        playerMan.SetPlayerHit(true);
    }
}