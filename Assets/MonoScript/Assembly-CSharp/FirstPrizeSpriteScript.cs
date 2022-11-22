using UnityEngine;

public class FirstPrizeSpriteScript : MonoBehaviour
{
	public float debug;

	public int angle;

	public float angleF;

	private SpriteRenderer sprite;

	public Transform cam;

	public Transform body;

	public Sprite[] sprites = new Sprite[16];

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		angleF = Mathf.Atan2(cam.position.z - body.position.z, cam.position.x - body.position.x) * 57.29578f;
		if (angleF < 0f)
		{
			angleF += 360f;
		}
		debug = body.eulerAngles.y;
		angleF += body.eulerAngles.y;
		angle = Mathf.RoundToInt(angleF / 22.5f);
		while (angle < 0 || angle >= 16)
		{
			angle += (int)(-16f * Mathf.Sign(angle));
		}
		sprite.sprite = sprites[angle];
	}
}
