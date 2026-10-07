using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid types (drag the 3 AsteroidData assets here)")]
    [SerializeField] private AsteroidData[] asteroidTypes;

    [Header("Spawn settings")]
    [SerializeField] private float spawnInterval = 1.5f;

    // Que tan lluny de la pantalla vols que faci spwan l'asteroid.
    // 0.1 = 10%
    [SerializeField] private float spawnMargin = 0.1f;

    // Els asteroides aniran directes a un punt dins del cercle per evitar que tots vagin directament al Player. 
    [SerializeField] private float targetRadius = 2f;

    private float spawnTimer;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        spawnTimer = spawnInterval;

        if (asteroidTypes == null || asteroidTypes.Length == 0)
        {
            Debug.LogError("AsteroidSpawner: assign at least one AsteroidData in the Inspector.");
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnAsteroid();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnAsteroid()
    {
        // On farà spsw l'ateroide
        Vector2 spawnPosition = GetRandomEdgePosition();

        // Quina direcció agafarà
        Vector2 target = Random.insideUnitCircle * targetRadius;
        Vector2 direction = target - spawnPosition;

        // Assignar la data al Asteroide
        AsteroidData data = GetRandomAsteroidData();

        // Agafa un asteroid de la pool
        Asteroid asteroid = AsteroidPool.Instance.GetAsteroid(spawnPosition);

        if (asteroid == null)
        {
            // Aquest if és aquí de mentre AsteroidPool.GetAsteroid() encara és un TODO
            Debug.LogWarning("AsteroidSpawner: the pool returned null. Is GetAsteroid() implemented?");
            return;
        }

        asteroid.Init(data, direction);
    }

    // tria un dels ScriptableObjects aleatòriament.

    private AsteroidData GetRandomAsteroidData()
    {
        int index = Random.Range(0, asteroidTypes.Length);
        return asteroidTypes[index];
    }

    // Retorna una WorldPosition justament a fora d'un dels 4 vèrtex de la pantalla
    // treballem amb coordenades de viewport per així aconseguir que es mantingui en funcionament sense dependre del aspect ratio
    private Vector2 GetRandomEdgePosition()
    {
        int side = Random.Range(0, 4);
        Vector2 viewportPoint = Vector2.zero;

        switch (side)
        {
            case 0: 
                viewportPoint = new Vector2(-spawnMargin, Random.value);
                break;
            case 1: 
                viewportPoint = new Vector2(1f + spawnMargin, Random.value);
                break;
            case 2: 
                viewportPoint = new Vector2(Random.value, 1f + spawnMargin);
                break;
            case 3: 
                viewportPoint = new Vector2(Random.value, -spawnMargin);
                break;
        }

        Vector3 worldPoint = mainCamera.ViewportToWorldPoint(viewportPoint);

        return worldPoint;
    }
}
