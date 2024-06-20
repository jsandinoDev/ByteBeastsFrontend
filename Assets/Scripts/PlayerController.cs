﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    [SerializeField] Joystick joystick;

    public bool canMove = true;
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //float horizontal = joystick.Horizontal;
        //float vertical = joystick.Vertical;

        if (canMove)
        {
            theRB.velocity = new Vector2(horizontal, vertical) * moveSpeed;

        } else
        {
            theRB.velocity = Vector2.zero;
        }


        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (horizontal == 1 || horizontal == -1 || vertical == 1 || vertical == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX",horizontal);
                myAnim.SetFloat("lastMoveY", vertical);
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
