using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public Item item;

    private Image icon;
    [SerializeField]
    private Vector3 offset;

    void SIngleton_Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Awake()
    {
        SIngleton_Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
        Drop();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition + offset;
    }

    public void TakeItem(Item item)
    {
        this.item = item;

        icon.sprite = item.icon;
        icon.color = Color.white;
    }

    public void Drop()
    {
        item = null;

        icon.color = new Color(0, 0, 0, 0);
    }

}