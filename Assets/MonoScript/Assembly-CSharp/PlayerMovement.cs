using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController cc;

	public float walkSpeed;

	public float runSpeed;

	public float stamina;

	public float staminaDrop;

	public float staminaRise;

	public float staminaMax;

	private float sensitivity;

	private float mouseSensitivity;

	private bool running;

	private void Awake()
	{
		mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	private void Start()
	{
		stamina = staminaMax;
		Time.timeScale = 1f;
	}

	private void Update()
	{
		running = Input.GetButton("Run");
		MouseMove();
		PlayerMove();
		StaminaUpdate();
	}

	private void MouseMove()
	{
		Quaternion rotation = base.transform.rotation;
		rotation.eulerAngles += new Vector3(0f, Input.GetAxis("Turn") * mouseSensitivity * Time.deltaTime * Time.timeScale, 0f);
		base.transform.rotation = rotation;
	}

	private void PlayerMove()
	{
		float num = walkSpeed;
		if ((stamina > 0f) & running)
		{
			num = runSpeed;
		}
		Vector3 vector = base.transform.right * Input.GetAxis("Strafe");
		Vector3 vector2 = base.transform.forward * Input.GetAxis("Forward");
		sensitivity = Mathf.Clamp((vector + vector2).magnitude, 0f, 1f);
		cc.Move((vector + vector2).normalized * num * sensitivity * Time.deltaTime);
	}

	public void StaminaUpdate()
	{
		if (cc.velocity.magnitude > cc.minMoveDistance)
		{
			if (running)
			{
				stamina = Mathf.Max(stamina - staminaDrop * Time.deltaTime, 0f);
			}
		}
		else if (stamina < staminaMax)
		{
			stamina += staminaRise * Time.deltaTime;
		}
	}
}
