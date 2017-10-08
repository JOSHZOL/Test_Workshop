﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hit platform");
        //collision.gameObject.transform.localScale = new Vector3(0.43f, 0.43f, 1.0f);
        collision.gameObject.GetComponent<PlayerController>().bJumpReady = true;
    }
}