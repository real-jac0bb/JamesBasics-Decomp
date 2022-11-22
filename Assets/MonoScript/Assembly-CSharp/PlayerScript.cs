using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public GameControllerScript gc;

	public BaldiScript baldi;

	public DoorScript door;

	public PlaytimeScript playtime;

	public bool gameOver;

	public bool jumpRope;

	public bool sweeping;

	public bool hugging;

	public bool bootsActive;

	public int principalBugFixer;

	public float sweepingFailsave;

	public float fliparoo;

	public float flipaturn;

	private Quaternion playerRotation;

	public Vector3 frozenPosition;

	private bool sensitivityActive;

	private float sensitivity;

	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float slowSpeed;

	public float maxStamina;

	public float staminaRate;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private Vector3 moveDirection;

	private float playerSpeed;

	public float stamina;

	public CharacterController cc;

	public NavMeshAgent gottaSweep;

	public NavMeshAgent firstPrize;

	public Transform firstPrizeTransform;

	public Slider staminaBar;

	public float db;

	public string guiltType;

	public GameObject jumpRopeScreen;

	public float height;

	public Material blackSky;

	public Canvas hud;

	public Canvas mobile1;

	public Canvas mobile2;

	private void Start()
	{
		if (PlayerPrefs.GetInt("AnalogMove") == 1)
		{
			sensitivityActive = true;
		}
		height = base.transform.position.y;
		stamina = maxStamina;
		playerRotation = base.transform.rotation;
		mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
		principalBugFixer = 1;
		flipaturn = 1f;
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, height, base.transform.position.z);
		MouseMove();
		PlayerMove();
		StaminaCheck();
		GuiltCheck();
		if (cc.velocity.magnitude > 0f)
		{
			gc.LockMouse();
		}
		if (jumpRope & ((base.transform.position - frozenPosition).magnitude >= 1f))
		{
			DeactivateJumpRope();
		}
		if (sweepingFailsave > 0f)
		{
			sweepingFailsave -= Time.deltaTime;
			return;
		}
		sweeping = false;
		hugging = false;
	}

	private void MouseMove()
	{
		playerRotation.eulerAngles = new Vector3(playerRotation.eulerAngles.x, playerRotation.eulerAngles.y, fliparoo);
		playerRotation.eulerAngles += Vector3.up * Input.GetAxis("Turn") * mouseSensitivity * Time.timeScale * flipaturn;
		base.transform.rotation = playerRotation;
	}

	private void PlayerMove()
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		vector = base.transform.forward * Input.GetAxis("Forward");
		vector2 = base.transform.right * Input.GetAxis("Strafe");
		if (stamina > 0f)
		{
			if (Input.GetButton("Run"))
			{
				playerSpeed = runSpeed;
				sensitivity = 1f;
				if ((cc.velocity.magnitude > 0.1f) & !hugging & !sweeping)
				{
					ResetGuilt("running", 0.1f);
				}
			}
			else
			{
				playerSpeed = walkSpeed;
				if (sensitivityActive)
				{
					sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
				}
				else
				{
					sensitivity = 1f;
				}
			}
		}
		else
		{
			playerSpeed = walkSpeed;
			if (sensitivityActive)
			{
				sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
			}
			else
			{
				sensitivity = 1f;
			}
		}
		playerSpeed *= Time.deltaTime;
		moveDirection = (vector + vector2).normalized * playerSpeed * sensitivity;
		if (!(!jumpRope & !sweeping & !hugging))
		{
			if (sweeping && !bootsActive)
			{
				moveDirection = gottaSweep.velocity * Time.deltaTime + moveDirection * 0.3f;
			}
			else if (hugging && !bootsActive)
			{
				moveDirection = (firstPrize.velocity * 1.2f * Time.deltaTime + (new Vector3(firstPrizeTransform.position.x, height, firstPrizeTransform.position.z) + new Vector3(Mathf.RoundToInt(firstPrizeTransform.forward.x), 0f, Mathf.RoundToInt(firstPrizeTransform.forward.z)) * 3f - base.transform.position)) * principalBugFixer;
			}
			else if (jumpRope)
			{
				moveDirection = new Vector3(0f, 0f, 0f);
			}
		}
		cc.Move(moveDirection);
	}

	private void StaminaCheck()
	{
		if (cc.velocity.magnitude > 0.1f)
		{
			if (Input.GetButton("Run") & (stamina > 0f))
			{
				stamina -= staminaRate * Time.deltaTime;
			}
			if ((stamina < 0f) & (stamina > -5f))
			{
				stamina = -5f;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.transform.name == "Baldi") & !gc.debugMode)
		{
			gameOver = true;
			RenderSettings.skybox = blackSky;
			StartCoroutine(KeepTheHudOff());
		}
		else if ((other.transform.name == "Playtime") & !jumpRope & (playtime.playCool <= 0f))
		{
			ActivateJumpRope();
		}
	}

	public IEnumerator KeepTheHudOff()
	{
		while (gameOver)
		{
			hud.enabled = false;
			mobile1.enabled = false;
			mobile2.enabled = false;
			jumpRopeScreen.SetActive(false);
			yield return new WaitForEndOfFrame();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			sweeping = true;
			sweepingFailsave = 1f;
		}
		else if ((other.transform.name == "1st Prize") & (firstPrize.velocity.magnitude > 5f))
		{
			hugging = true;
			sweepingFailsave = 1f;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger")
		{
			ResetGuilt("escape", door.lockTime);
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			sweeping = false;
		}
		else if (other.transform.name == "1st Prize")
		{
			hugging = false;
		}
	}

	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt)
		{
			guilt = amount;
			guiltType = type;
		}
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}

	public void ActivateJumpRope()
	{
		jumpRopeScreen.SetActive(true);
		jumpRope = true;
		frozenPosition = base.transform.position;
	}

	public void DeactivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
	}

	public void ActivateBoots()
	{
		bootsActive = true;
		StartCoroutine(BootTimer());
	}

	private IEnumerator BootTimer()
	{
		float time = 15f;
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		bootsActive = false;
	}
}
