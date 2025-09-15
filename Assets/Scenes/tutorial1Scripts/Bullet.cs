using UnityEngine;

public class Bullet : PoolableObject
{

    public Rigidbody bulletRigidBody;
    public float bulletLifetime = 5.0f;
    private string disableMethod = "Disable";
    private float damage = 5.0f;
    public Vector3 bulletOffset = new Vector3(0,1,0);

    private void Awake()
    {
        bulletRigidBody=GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Invoke(disableMethod, bulletLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable;
        if (other.TryGetComponent<IDamagable>(out damagable))
        {
            damagable.TakeDamage(damage);
        }
        Disable();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        bulletRigidBody.linearVelocity=Vector3.zero;
    }


}
