using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableWall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public int hp;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Break()
    {
        hp -= 1;

        if (hp >= 4)
        {
            spriteRenderer.sprite = sprites[0];
        }

        else if (hp == 3)
        {
            spriteRenderer.sprite = sprites[1];
        }

        else if (hp == 2)
        {
            spriteRenderer.sprite = sprites[2];
        }

        else if (hp == 1)
        {
            spriteRenderer.sprite = sprites[3];
        }

        else if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
