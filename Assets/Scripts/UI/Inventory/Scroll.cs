using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scroll : MonoBehaviour
{
    public Transform content;
    public int scrollLimit;
    public int scrollTrigger;
    public float scrollDist;
    public float scrollSpeed;

    private bool isScrolling;

    private void Start()
    {
        content = transform.GetChild(0);
        scrollLimit = content.childCount - 1;
        scrollTrigger = 0;
    }

    private void Update()
    {
        if (!isScrolling && EventSystem.current.IsPointerOverGameObject())
        {
            //ÈÙ ´Ù¿î
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && scrollTrigger < scrollLimit)
            {
                //À§·Î°¡¾ß´ï
                StartCoroutine(SmoothScroll(new Vector3(content.position.x, content.position.y + scrollDist)));
                scrollTrigger++;
                isScrolling = true;
            }

            //ÈÙ ¾÷
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && scrollTrigger > 0)
            {
                //¾Æ·¡·Î°¡¾ß´ï
                StartCoroutine(SmoothScroll(new Vector3(content.position.x, content.position.y - scrollDist)));
                scrollTrigger--;
                isScrolling = true;
            }
        }
    }

    IEnumerator SmoothScroll(Vector3 target)
    {
        while (Mathf.Abs(target.y - content.position.y) > 0.1f)
        {
            content.position = Vector3.Lerp(content.position, target, 0.05f);
            yield return null;
        }

        isScrolling = false;
        yield return null;
    }
}
