﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    bool pressed;
    float fTimePassed;
    float fDelay;
    float shakeAmt;
    //float startY;

    public Camera mainCamera;
    public Slider power;
    public Rigidbody2D rb;
    public GameObject sugarDrop;
    public AudioSource Jump;

    public bool dead;
    public int force;
    public int sugarForce;
    public bool bJumpReady = false;
    public string jumpButton;
    public bool bCanMove = true;
    
    // Use this for initialization
    void Start()
    {
        force = 100;
        pressed = false;
        bJumpReady = true;
        dead = false;
        shakeAmt = 0;
        fTimePassed = 0.0f;
        fDelay = 3.0f;
        //startY = mainCamera.transform.position.y;
    }

    public void vStayStill()
    {
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetBool("Land"))
        {
            fTimePassed += Time.deltaTime;
        }

        fDelay += Time.deltaTime;

        if (fTimePassed > 0.5f)
        {
            GetComponent<Animator>().SetBool("Land", false);
            fTimePassed = 0.0f;
        }

        if (!bCanMove && bJumpReady && fDelay > 1.0f)
        {
            rb.AddForce(Vector3.up * force);
            bJumpReady = false;
            fDelay = 0.0f;
        }

        if (bJumpReady && bCanMove && rb != null)
        {
            if (Input.GetKeyDown(jumpButton))
            {
                pressed = true;

                GetComponent<Animator>().SetBool("Charge", true);
            }

            if (pressed && force <= 260)
            {
                force += 3;
            }

            if (Input.GetKeyUp(jumpButton))
            {
                Jump.Play();
                rb.AddForce(Vector3.right * force + Vector3.up * force);
                force = 100;
                pressed = false;
                bJumpReady = false;

                GetComponent<Animator>().SetBool("Charge", false);
            }

            power.value = force;
        }
        else if (!bJumpReady && bCanMove && rb != null && fDelay > 1.5f)
        {
            if (Input.GetKeyUp(jumpButton))
            {
                Instantiate(sugarDrop, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                sugarDrop.GetComponent<Rigidbody2D>().AddForce(Vector3.up * sugarForce);
                fDelay = 0.0f;
            }
        }
        
        if (dead)
        {
            shakeAmt = 4 * .0045f;
            InvokeRepeating("CameraShake", 0, .001f);
            Invoke("StopShaking", 0.2f);
        }
        
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;// can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, startY, mainCamera.transform.position.z);
    }
}


