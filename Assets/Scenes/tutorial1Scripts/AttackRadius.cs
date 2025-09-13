using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class AttackRadius : MonoBehaviour
{
    private List<IDamagable> listDamagable = new List<IDamagable>();
    private float attackDelay = 0.8f;
    private Coroutine attackCoroutine;
    public delegate void AttackAnimation(IDamagable damagable);
    public AttackAnimation setAttackAnimation;
    public float damage = 10.0f;
    public NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        Debug.Log(damagable);
        if (damagable!=null)
        {
            listDamagable.Add(damagable);
            if (attackCoroutine==null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable!=null)
        {
            listDamagable.Remove(damagable);
            if (listDamagable.Count==0)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine= null;
            }
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds time=new WaitForSeconds(attackDelay);
        float maxDistance = float.MaxValue;
        IDamagable closestDamagable = null;
        while (listDamagable.Count>0 )
        {
            for (int i = 0; i < listDamagable.Count; i++)
            {
                float distance = Vector3.Distance(listDamagable[i].GetTransform().position, transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestDamagable = listDamagable[i];
                }
            }

            if (closestDamagable != null)
            {
                setAttackAnimation.Invoke(closestDamagable);
                yield return time;
                closestDamagable.TakeDamage(damage);
            }
            yield return null;
            maxDistance = float.MaxValue;
            closestDamagable=null;
            listDamagable.RemoveAll(RemoveDisabledDamagable);
        }


    }

    private bool RemoveDisabledDamagable(IDamagable damagable)
    {
        return !damagable.GetTransform().gameObject.activeSelf;
    }


}
