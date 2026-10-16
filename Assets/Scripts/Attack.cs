using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 0.6f;

    [Header("Hit Detection")]
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;

    private float nextAttackTime = 0f;

    void Update()
{
    if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
    {
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;
        // DealDamage() se više NE poziva ovde
    }
}

public void DealDamage()
{
    Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

    foreach (Collider hit in hits)
    {
        Debug.Log("Udarac: " + hit.name);
    }
}

    // Crta sferu u Scene prozoru da vidiš domet
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}