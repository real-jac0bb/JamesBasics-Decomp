using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public GameObject player;

	public PlayerScript ps;

	public Transform baldi;

	public float initVelocity;

	public float velocity;

	public float gravity;

	private int lookBehind;

	public Vector3 offset;

	public float jumpHeight;

	public Vector3 jumpHeightV3;

	private void Start()
	{
		offset = base.transform.position - player.transform.position;
	}

	private void Update()
	{
		if (ps.jumpRope)
		{
			velocity -= gravity * Time.deltaTime;
			jumpHeight += velocity * Time.deltaTime;
			if (jumpHeight <= 0f)
			{
				jumpHeight = 0f;
				if (Input.GetButton("Jump"))
				{
					velocity = initVelocity;
				}
			}
			jumpHeightV3 = new Vector3(0f, jumpHeight, 0f);
		}
		else if (Input.GetButton("LookBack"))
		{
			lookBehind = 180;
		}
		else
		{
			lookBehind = 0;
		}
	}

	private void LateUpdate()
	{
		base.transform.position = player.transform.position + offset;
		if (!ps.gameOver & !ps.jumpRope)
		{
			base.transform.position = player.transform.position + offset;
			base.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, lookBehind, 0f);
		}
		else if (ps.gameOver)
		{
			base.transform.position = baldi.transform.position + baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f);
			base.transform.LookAt(new Vector3(baldi.position.x, baldi.position.y + 5f, baldi.position.z));
		}
		else if (ps.jumpRope)
		{
			base.transform.position = player.transform.position + offset + jumpHeightV3;
			base.transform.rotation = player.transform.rotation;
		}
	}
}
