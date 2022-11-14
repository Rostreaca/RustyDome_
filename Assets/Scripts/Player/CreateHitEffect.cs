using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHitEffect : MonoBehaviour
{
    BoxCollider2D boxcol;
    public bool aleadyhitting;
    public GameObject EffectImage;
    public Vector2 random_position, spawn_position;
    public float randompos_x,randompos_y;
    // Start is called before the first frame update
    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
       if(PlayerController.instance.isHit == true && aleadyhitting == false)
        {
            CreateEffect();
            aleadyhitting = true;
        }

        if (PlayerController.instance.isHit == false )
        {
            aleadyhitting = false;
        }
    }

    void CreateEffect()
    {
        randompos_x = Random.Range(-(boxcol.bounds.size.x/2), boxcol.bounds.size.x/2);
        randompos_y = Random.Range(-(boxcol.bounds.size.y/2), boxcol.bounds.size.y/2);

        random_position = new Vector2(randompos_x, randompos_y);

        spawn_position = new Vector2(transform.position.x+randompos_x,transform.position.y+randompos_y);
        Instantiate(EffectImage, spawn_position, Quaternion.identity);
    }
}
