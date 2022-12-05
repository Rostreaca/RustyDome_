using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSprite : MonoBehaviour
{
    public Animator playerAnimator, weaponAnimator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D meleeAttackTrigger, specialAttackTrigger;

    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack1"))
        {
            weaponAnimator.Play("MeleeAttack1");
        }

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            weaponAnimator.Play("SpecialAttack");
        }
    }

    public void Rotate()
    {
        //´ë·« 0.015f = 1px
        float weaponSpriteOffset = 0.185f;
        float TriggerOffset = 0.45f;

        if(Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.gameObject.transform.localPosition = new Vector3(-weaponSpriteOffset, 0, 0);
            meleeAttackTrigger.offset = new Vector2(-TriggerOffset, -0.45f);
            specialAttackTrigger.offset = new Vector2(-TriggerOffset, -0.45f);
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.gameObject.transform.localPosition = new Vector3(weaponSpriteOffset, 0, 0);
            meleeAttackTrigger.offset = new Vector2(TriggerOffset, -0.45f);
            specialAttackTrigger.offset = new Vector2(TriggerOffset, -0.45f);
        }
    }

    public void Hit()
    {
        weaponAnimator.SetTrigger("Hit");
    }
}
