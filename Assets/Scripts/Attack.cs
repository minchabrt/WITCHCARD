using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 2f;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}