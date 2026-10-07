using UnityEngine;
// TODO: crea el ScriptableObject [CreateAssetsMenu...] recorda que no ha d'heretar de MonoBehaviour
[CreateAssetMenu(fileName = "NewAsteroidData", menuName = "Asteroids/Asteroid Data")]
public class AsteroidData : ScriptableObject
{
	//TODO: introdueix les dades necesaties
	public float scale;
	public float minSpeed;
	public float maxSpeed;
	public int maxHealth;
	public int damage;
	public Color color;
	public int points;
}
