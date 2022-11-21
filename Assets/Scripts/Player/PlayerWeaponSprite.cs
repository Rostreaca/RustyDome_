using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSprite : MonoBehaviour
{
    public Animator playerAnimator, weaponAnimator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D meleeAttackTrigger;

    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack1"))
        {
            weaponAnimator.Play("MeleeAttack1");
        }
    }

    public void Rotate()
    {
        //´ë·« 0.015f = 1px
        float weaponSpriteOffset = 0.185f;
        float meleeAttackTriggerOffset = 0.45f;

        if(Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.gameObject.transform.localPosition = new Vector3(-weaponSpriteOffset, 0, 0);
            meleeAttackTrigger.offset = new Vector2(-meleeAttackTriggerOffset, -0.45f);
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.gameObject.transform.localPosition = new Vector3(weaponSpriteOffset, 0, 0);
            meleeAttackTrigger.offset = new Vector2(meleeAttackTriggerOffset, -0.45f);
        }
    }
}
