using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{

    public Canvas canvas;
    private float halfWidth, halfHeight;

    private Camera theCamera;


    private void Awake()
    {
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas.worldCamera = gameObject.GetComponent<Camera>();
        if (GameObject.Find("SceneManager"))
        {
            Canvas scenemanager = GameObject.Find("SceneManager").GetComponent<Canvas>();

            scenemanager.worldCamera = gameObject.GetComponent<Camera>();
        }
        theCamera = GetComponent<Camera>();

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;


    }

    public void FixedUpdate()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas.worldCamera = gameObject.GetComponent<Camera>();
        //if (GameObject.Find("SceneManager"))
        //{
        //    Canvas scenemanager = GameObject.Find("SceneManager").GetComponent<Canvas>();

        //    scenemanager.worldCamera = gameObject.GetComponent<Camera>();
        //}

        float clampedX = Mathf.Clamp(transform.position.x, halfWidth,  halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, halfHeight, halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z); //make clamp
    }

}
