using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance; //static camera for use in other scripts

    Transform player; //Player position

    public Vector2 offset; //Camera offset by player position
    public float cameraXPosMin, cameraXPosMax;
    public float cameraYPosMin, cameraYPosMax; //Camera position clamp
    public float moveSpeed;

    public BoxCollider2D bound;
    private Vector3 minBound, maxBound;

    private float halfWidth, halfHeight;

    private Camera theCamera;

    void SingletonInit()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        player = GameObject.FindGameObjectWithTag("Player").transform; //Find player in scene
    }

    public void FixedUpdate()
    {
        Vector3 newPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); //Local vector get player position
        transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime); //Set camera position smooth

        float clampedX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z); //make clamp
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}