using System.Collections;
using System.Data;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    [SerializeField] private float health = 1000;
    public Animator playerAnimator;
    private string attackTrigger = "attack";
    public AttackRadius attackRadius;
    private Coroutine lookCoroutine;
    private void Awake()
    {
        attackRadius.setAttackAnimation += SetAttackAnimation;
    }


    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(float damage)
    {
        health-=damage;
        if (health<=0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAttackAnimation(IDamagable damagable)
    {
        playerAnimator.SetTrigger(attackTrigger);
        if (lookCoroutine!=null)
        {
            lookCoroutine = null;
        }
        lookCoroutine = StartCoroutine(LookAtObject(damagable.GetTransform()));
    }

    private IEnumerator LookAtObject(Transform obj)
    {
        Vector3 direction = obj.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float current = 0.0f;
        while (current < 1.0f)
        {
            current += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, current);
            yield return null;
        }
        transform.rotation = lookRotation;
    }

}
