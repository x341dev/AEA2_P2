using UnityEditor.Toolbars;
using UnityEngine;
// Init() ja està implementat (el fem servir per donar-li tota la informació al prefab del Asteroid)
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Asteroid : MonoBehaviour
{
    // Una mica més gran que la distancia d'aparició
    [SerializeField] private float despawnDistance = 15f;

    private AsteroidData data;
    private int currentHealth;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //TODO Agafem el component RigidBody2D i SpriteRenderer.
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Cridem aquest mètode cada vegada que un Asteroid surt de la pool.
    public void Init(AsteroidData newData, Vector2 direction)
    {
        data = newData;

        // TODO: passa els valorsdel ScriptableObject a Variables locals
        // currentHealth = data.maxHealth;
        // spriteRenderer.color = data.color;
        // transform.localScale = Vector3.one * data.scale;
        // float speed = Random.Range(data.minSpeed, data.maxSpeed);

        currentHealth = data.maxHealth;
        spriteRenderer.color = data.color;
        transform.localScale = Vector3.one * data.scale;
        float speed = Random.Range(data.minSpeed, data.maxSpeed);

        // rb.linearVelocity = direction.normalized * speed;

        rb.linearVelocity = direction.normalized * speed;
    }

    private void Update()
    {
        // TODO: Retornar l'ateroide al pool quan ja està massa lluny
        // Distancia al centre: transform.position.magnitude
        // Si és més gran que despawnDistance, cridem AsteroidPool.Instance.ReturnAsteroid(this)

        if (transform.position.magnitude < despawnDistance) AsteroidPool.Instance.ReturnAsteroid(this);
    }

    public void TakeDamage(int amount)
    {
        // TODO: Restem amount al currentHealth

        currentHealth -= amount;

        // TODO: si la currentHealth es menor a 0 
        // Sumem els punt al Player
        // Cridem al GameManger i el seu mètode per sumar scoreValue
        // Retornem l'asteroid a la seva pool

        if (currentHealth < 0)
        {
            GameManager.Instance.AddScore(data.points);
            AsteroidPool.Instance.ReturnAsteroid(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Mirem si col·lisiona amb el Player if 'other' has the tag "Player" (fem servir other.CompareTag):
        // Restem vida al Player - GameManager.Instance.LoseLife(data.damage)         
        // Retornem l'asteroid a la seva pool
        // Tip: if si l'asteroid ja està innactiu no fagis res
        //      (if (!gameObject.activeSelf) return;)

        if (other.CompareTag("Player")) {
            if (!gameObject.activeSelf) return;
            GameManager.Instance.LoseLife(data.damage);
        }
    }
}
