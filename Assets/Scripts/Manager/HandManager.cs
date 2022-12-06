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

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();

        Drop();
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

    public void UpdateHand()
    {
        icon.transform.localPosition = Input.mousePosition + offset;
    }

}