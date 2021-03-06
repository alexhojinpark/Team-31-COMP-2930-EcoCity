﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{

    public float speed;
    public float zoomSpeed;
    public int minZoom = 13;
    public int maxZoom = 60;

    private float orthoTarget;
    private Animator animator;
    private float animPlayPercent = 0.0f;
    private Camera mainCamera;

    private bool movingRight;
    private bool movingLeft;
    private bool movingUp;
    private bool movingDown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        orthoTarget = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();
        SetZoom();
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, orthoTarget, Time.deltaTime * zoomSpeed);
    }

    /// <summary>
    ///     Checks for Camera Movement Keypresses. Only for M/KB.
    /// </summary>
    private void HandleControls()
    {
        //Strafe Controls
        if (Input.GetKey(KeyCode.W) || movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || movingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || movingDown)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void MoveDown(bool state)
    {
        movingDown = state;
    }

    public void MoveForward(bool state)
    {
        movingUp = state;
    }

    public void MoveRight(bool state)
    {
        movingRight = state;
    }

    public void MoveLeft(bool state)
    {
        movingLeft = state;
    }

    private void SetZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            orthoTarget = Mathf.Clamp(mainCamera.orthographicSize - zoomSpeed, minZoom, maxZoom);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            orthoTarget = Mathf.Clamp(mainCamera.orthographicSize + zoomSpeed, minZoom, maxZoom);
        }
    }

    public void ZoomIn()
    {
        orthoTarget = Mathf.Clamp(mainCamera.orthographicSize - zoomSpeed, minZoom, maxZoom);
    }

    public void ZoomOut()
    {
        orthoTarget = Mathf.Clamp(mainCamera.orthographicSize + zoomSpeed, minZoom, maxZoom);
    }

}
