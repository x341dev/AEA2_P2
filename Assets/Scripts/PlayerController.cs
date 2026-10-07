using UnityEngine;
using UnityEngine.InputSystem;

// TODO:
//   Us donarà error perquè no teniu creat el ActionMap SpaceShip aneu amb cura de com l'anomeneu.
//     un cop creat fixeu-vos en quins mètodes té implementat la interfície (Aim, Fire)
//     haureu de crear les accions preparar el binding Aim Vector2 MousPosition //  Fire buttom SpaceKeyboard
//   Recordeu marcar el tick "Generate C# Class" i aplicar.
//     la interfície obliga a tenir els mètodes: OnFire() and OnAim().
public class PlayerController : MonoBehaviour, InputSystem_Actions.IShipActions
{
    [Header("Shooting")]
    [SerializeField] private Transform firePoint; // EmptyObject a la punta de la nau

	private InputSystem_Actions inputActions;
    private Vector2 aimScreenPosition;            // Última posicio del Mouse
	private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

		// TODO: Crea el new inputActions
		// TODO: Fes que els Callbacks avisin aquest script
		inputActions = new InputSystem_Actions();
		inputActions.Ship.SetCallbacks(this);
    }

	// TODO: Activa (Enable) l'inputActions
	private void OnEnable() => inputActions.Enable();

	// TODO: Desactiva (Disable) l'inputActions
	private void OnDisable() => inputActions.Disable();

	public void OnFire(InputAction.CallbackContext context)
	{
		// TODO: si no és "performed" no facis res (if (!context.performed) return;)

		// TODO: si és GameOver no facis res GameManager.Instance.IsGameOver (return).

		// TODO: Dispara.
		//       BulletPool.Instance.GetBullet(firePoint.position, transform.rotation);
		if (!context.performed || GameManager.Instance.IsGameOver) return;

		Bullet bullet = BulletPool.Instance.GetBullet(firePoint.position, transform.rotation);
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		if (GameManager.Instance.IsGameOver) return;

		// TODO: aimScreenPosition = context;
		aimScreenPosition = context.ReadValue<Vector2>();
		Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(aimScreenPosition);
		Vector2 direction = mouseWorldPosition - transform.position;
		transform.up = direction;
	}
}
