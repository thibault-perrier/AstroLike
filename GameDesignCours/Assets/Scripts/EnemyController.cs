using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackController : MonoBehaviour
{
    public float pushForce = 400f;
    public float attackCooldown = 2f;
    public bool canAttack = true;

    private WaitForSeconds cooldownAttackWaitForSeconds;

    private void Start()
    {
        cooldownAttackWaitForSeconds = new(attackCooldown);
    }

    public void AttackForward(float dist)
    {
        bool succes = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, dist);
        if (succes)
        {
            GameObject gHit = hit.transform.gameObject;
            TryPushEnemy(gHit);
        }

        StartCoroutine(StartCooldown());
    }

    public void AttackSphere(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var col in colliders)
        {
            GameObject gHit = col.transform.gameObject;
            TryPushEnemy(gHit);
        }

        StartCoroutine(StartCooldown());
    }

    private void TryPushEnemy(GameObject e)
    {
        if (!canAttack)
            return;

        if (e.TryGetComponent<Rigidbody>(out var r))
            r.AddForce((e.transform.position - transform.position).normalized * pushForce);
    }

    private IEnumerator StartCooldown()
    {
        canAttack = false;
        yield return cooldownAttackWaitForSeconds;
        canAttack = true;
    }
}