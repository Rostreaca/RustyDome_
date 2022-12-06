using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTrigger : MonoBehaviour
{
    public Combat combat;
    
    private void Update()
    {
        if(gameObject.transform.parent.transform.parent.tag == "Enemy")
        {

            if (gameObject.GetComponentInParent<SpriteRenderer>().flipX == true)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (gameObject.GetComponentInParent<SpriteRenderer>().flipX != true)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        combat.colliderDetected = collider;
        combat.MeleeHitDetected();
    }

}