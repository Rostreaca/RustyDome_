using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHitEffect : MonoBehaviour
{
    
    BoxCollider2D boxcol;
    public bool aleadyhitting;
    public GameObject[] EffectImage;
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
        EnemyController enemy = gameObject.GetComponentInParent<EnemyController>();
       if(enemy._create_effect == true && aleadyhitting == false)
        {
            CreateEffect();
            aleadyhitting = true; enemy._create_effect = false;
        }

        if (enemy._create_effect == false )
        {
            aleadyhitting = false;
        }
    }

    void CreateEffect()
    {
        int random_range = Random.Range(0, EffectImage.Length);
        randompos_x = Random.Range(-(boxcol.bounds.size.x/2), boxcol.bounds.size.x/2);
        randompos_y = Random.Range(-(boxcol.bounds.size.y/2), boxcol.bounds.size.y/2);

        random_position = new Vector2(randompos_x, randompos_y);

        spawn_position = new Vector2(transform.position.x+randompos_x,transform.position.y+randompos_y);
        Instantiate(EffectImage[random_range], spawn_position, Quaternion.identity);
    }
}
