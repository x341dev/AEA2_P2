using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int initialSize = 20;

    private Stack<Bullet> pool = new Stack<Bullet>();

    private void Awake()
    {
		// TODO:
		//   Singleton.
		//   Fer les comprovacions necessàries perquè només hi hagi una instància d'aquest singleton 
		// TODO: Inicialitza aquí la Pool amb base que es farà servir.
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		for (int i = 0; i < initialSize; i++) pool.Push(CreateBullet());
    }

    private Bullet CreateBullet()
    {
        // TODO:
        //   Instància els prefabs recorda que els has d'instanciar desactivats.
        //   Bullet bullet = Instantiate(bulletPrefab, transform);
        Bullet newBullet = Instantiate(bulletPrefab, transform);
        newBullet.gameObject.SetActive(false);
        return newBullet;
    }

	// TODO:
	//     si l'stack té elements (pool.Count>0), treu una amb Pop().
	//     si està buit crea una nova amb CreatBullet()
	//     col·loca-la i rota-la bullet.transform.SetPositionAndRotation(position, rotation)        
	//     Activa-la
	//     Inizialitza amb Init(). (important col·locar-la i rotar-la 1r i després iniciar)
	//     retorna-la.
	public Bullet GetBullet(Vector3 position, Quaternion rotation) => (pool.Count > 0) ? pool.Pop() : CreateBullet();

    // Mètode cridat per la bullet quan termina el temps o impacta amb un Asteroid.
    public void ReturnBullet(Bullet bullet)
    {
        // TODO: Desactiva la bullet i retorna-la a la seva pool
        bullet.gameObject.SetActive(false);
        pool.Push(bullet);
    }
}
