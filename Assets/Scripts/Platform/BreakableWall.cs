using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableWall : MonoBehaviour
{
    private Animator animator;

    public int hp;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        hp -= 1;

        if (hp > 0)
        {
            animator.SetTrigger("Damage");
        }

        else if (hp <= 0)
        {
            animator.SetTrigger("Break");
        }
    }

    public void OnBreakEnd()
    {
        Destroy(gameObject);
    }
}
