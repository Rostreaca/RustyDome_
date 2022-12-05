using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scroll : MonoBehaviour
{
    public Transform content;
    public float scrollSpeed;

    public bool isScrolling;
    public float scrollDist;
    public int scrollLimit;
    public int scrollTrigger;

    private void Start()
    {
        content = transform.GetChild(0);
        scrollDist = content.GetComponent<RectTransform>().rect.height / content.childCount;
        scrollLimit = content.childCount - 1;
        scrollTrigger = 0;
    }

    public void OnMouseOver()
    {
        if (!isScrolling && UIManager.instance.IsScreenOn(UIManager.instance.inventoryScreen))
        {
            //ÈÙ ´Ù¿î
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && scrollTrigger < scrollLimit)
            {
                //À§·Î°¡¾ß´ï
                StartCoroutine(SmoothScroll(new Vector3(content.localPosition.x, content.localPosition.y + scrollDist)));
                scrollTrigger++;
                isScrolling = true;
            }

            //ÈÙ ¾÷
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && scrollTrigger > 0)
            {
                //¾Æ·¡·Î°¡¾ß´ï
                StartCoroutine(SmoothScroll(new Vector3(content.localPosition.x, content.localPosition.y - scrollDist)));
                scrollTrigger--;
                isScrolling = true;
            }
        }
    }

    IEnumerator SmoothScroll(Vector3 target)
    {
        while (Mathf.Abs(target.y - content.localPosition.y) > 0.01f)
        {
            content.localPosition = Vector3.Lerp(content.localPosition, target, 0.1f);
            yield return null;
        }
        content.localPosition = target;

        isScrolling = false;
        yield return null;
    }

}