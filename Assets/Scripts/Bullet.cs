using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;

    private float lifeTimer;
    private Rigidbody2D rb;

    private void Awake()
    {
        // TODO: guarda el component Rigidboy (rb)
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        // TODO: fem reset al lifeTimer.
        lifeTimer = lifeTime;

        // TODO: fem que la bullet es mogui cap al seu Forward.
        // "Forward" per un 2D sprite pointing up is transform.up // rb.linearVelocity = ...
        rb.linearVelocity = transform.up * speed;
    }

    private void Update()
    {
        // TODO: Restem Time.deltaTime a lifeTimer.
        //       si arriba a 0 retorna la bullet amb BulletPool.Instance.ReturnBullet(this).
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0) ReturnSelfToPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO:
        //   si la bullet ja està inactiva no facis res (if (!gameObject.activeSelf) return;)
        //   això evitarà que es retorni la mateixa bullet si aquesta impacta 2 Asteroides en el mateix frame.
        //   comprova si 'other' és un Ateroid i quedat amb el component (GetComponent<Asteroid>()).
        //   si no és null TakeDamage(damage) i retorna la bullet a la seva pool (asteroid != null).
        if (!gameObject.activeSelf) return;

        if (other.CompareTag("Asteroid")) {
            Asteroid asteroid = other.GetComponent<Asteroid>();
            asteroid.TakeDamage(damage);
            ReturnSelfToPool();
        }

    }

    private void ReturnSelfToPool() => BulletPool.Instance.ReturnBullet(this);
}
