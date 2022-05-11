using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;
    private PlayerController playerController;

    private bool canCombo;

    public Weapon Weapon
    {
        get => default;
        set
        {
        }
    }

    private void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public void OnMeleeAttackBegin(float timeToCombo)
    {
        StartCoroutine(ICombo(timeToCombo)); //Start combo system 
    }

    public void OnMeleeAttackEnd()
    {
        StopCoroutine(ICombo(0)); //Stop combo 

        //Animator update
        animator.ResetTrigger("AttackCombo");
        animator.SetBool("MeleeAttack", false);

        canCombo = false; //block combo
        playerController.isAttack = false;
    }

    IEnumerator ICombo(float comboTimer)
    {
        canCombo = false;
        yield return new WaitForSeconds(comboTimer);
        canCombo = true;

        while (canCombo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                canCombo = false;
                animator.SetTrigger("AttackCombo");
                StopCoroutine(ICombo(0));
            }
            yield return null;
        }
    }
}
