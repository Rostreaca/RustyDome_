using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerHitEffect : MonoBehaviour
{
    public Transform player_sprite;
    public int randomrange;
    BoxCollider2D boxcol;
    public bool aleadyhitting;
    public GameObject[] EffectImage;
    public Vector2 spawn_position;
    // Start is called before the first frame update
    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isHit == true && aleadyhitting == false)
        {
            CreateEffect();
            aleadyhitting = true;
        }

        if (PlayerController.instance.isHit == false)
        {
            aleadyhitting = false;
        }
    }
    void CreateEffect()
    {
        randomrange = Random.Range(0, 2);
        spawn_position = new Vector2(transform.position.x, transform.position.y);
        Instantiate(EffectImage[randomrange], spawn_position, Quaternion.identity,player_sprite);
    }
}
