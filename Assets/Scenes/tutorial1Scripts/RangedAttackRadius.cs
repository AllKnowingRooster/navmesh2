using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class RangedAttackRadius : AttackRadius
{
    public Bullet bulletPrefab;
    private Pool bulletPool;
    private IDamagable targetDamagable;
    public LayerMask mask;


    private void Awake()
    {
        bulletPool = Pool.CreatePoolInstance(bulletPrefab,Mathf.CeilToInt((1/attackDelay) * bulletPrefab.bulletLifetime));
    }


    protected override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        while (listDamagable.Count>0)
        {
            for (int i=0;i<listDamagable.Count;i++)
            {
                if (HasLineOfSight(listDamagable[i].GetTransform()))
                {
                    agent.isStopped = true;
                    setAttackAnimation.Invoke(listDamagable[i]);
                    targetDamagable=listDamagable[i];
                    break;
                }
            }
            yield return null;
            if (targetDamagable!=null)
            {
                PoolableObject bullet=bulletPool.GetObject();
                if (bullet!=null)
                {
                    Bullet bulletComponent=bullet.GetComponent<Bullet>();
                    bulletComponent.transform.position=transform.position+bulletComponent.bulletOffset;
                    bulletComponent.transform.rotation=agent.transform.rotation;
                    bulletComponent.bulletRigidBody.AddForce(agent.transform.forward * 5.0f, ForceMode.VelocityChange);
                }
                yield return wait;
            }
            else
            {
                agent.isStopped=false;
            }

           
        }
    }


    private bool HasLineOfSight(Transform target)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.transform.position,(target.position - transform.parent.transform.position).normalized,out hit,3.0f,mask))
        {
            IDamagable damagable= hit.collider.GetComponent<IDamagable>();
            if (damagable!=null)
            {
                return target == damagable.GetTransform();
            }
           
        }
        return false;
    }



}
