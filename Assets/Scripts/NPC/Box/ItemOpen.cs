using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpen : MonoBehaviour
{
    Rigidbody2D rigid;
    public float x,y;
    public float v = 7.0f;
    public float t,g; //속력,시간,중력가속도

    public float theta;

    void Awake()
    {
        if(BoxText.instance !=null)
        {
            theta = BoxText.instance.seta;
        }
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        t = Time.deltaTime;
        g = 9.8f;
        Parabola();

    }

    void Parabola()
    {
        x = v* Mathf.Cos(theta * Mathf.Deg2Rad) *t;
        y = v* Mathf.Sin(theta * Mathf.Deg2Rad) *t-(0.5f*g*t*t);
        transform.right = new Vector2(x,y);
        rigid.velocity = transform.right;
    }
}
