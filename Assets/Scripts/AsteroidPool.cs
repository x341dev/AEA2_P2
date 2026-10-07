using System.Collections.Generic;
using UnityEngine;

// TODO: la mateixa estructura que el bullet pool.
public class AsteroidPool : MonoBehaviour
{
    public static AsteroidPool Instance;

    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private int initialSize = 15;

    private Stack<Asteroid> pool = new Stack<Asteroid>();

    private void Awake()
    {
		// TODO:
		//   Singleton.
		//   Fer les comprovacions necessàries perquè només hi hagi una instància d'aquest singleton 
		// TODO:Inicialitza aquí la Pool amb base que es farà servir. 
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		for (int i = 0; i < initialSize; i++) pool.Push(CreateAsteroid());
	}

    private Asteroid CreateAsteroid()
    {
        // TODO:
        //   Instància els prefabs recorda que els has d'instanciar desactivats.
        //   Fixeu-vos que el mètode ha de retornar un Asteroid
        Asteroid newAsteroid = Instantiate(asteroidPrefab, transform);
        newAsteroid.gameObject.SetActive(false);
        return newAsteroid;
    }

	// TODO:
	//   si l'stack no esta buit (pool.Count > 0), treu un Asteroid Pop().
	//   si no en queda cap instancia un CreateAsteroid().
	//   col·loca en la posició correcta, activa'l i retorna'l
	public Asteroid GetAsteroid(Vector3 position) => pool.Count > 0 ? pool.Pop() : CreateAsteroid();

    public void ReturnAsteroid(Asteroid asteroid)
    {
        // TODO:
        //   Desactiva l'asteroid i torna'l al stack Push().
        asteroid.gameObject.SetActive(false);
        pool.Push(asteroid);
    }
}
