using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpen : MonoBehaviour
{
    Rigidbody2D rigid;
    private float x,y;
    private float v = 7.0f;
    private float t,g; //속력,시간,중력가속도

    public float theta;

    private void Awake()
    {
        if(BoxText.instance !=null)
        {
            theta = BoxText.instance.seta;
        }
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        t = Time.deltaTime;
        g = 9.8f;
        Parabola();

    }

    private void Parabola()
    {
        x = v* Mathf.Cos(theta * Mathf.Deg2Rad) *t;
        y = v* Mathf.Sin(theta * Mathf.Deg2Rad) *t-(0.5f*g*t*t);
        transform.right = new Vector2(x,y);
        rigid.velocity = transform.right;
    }
}
