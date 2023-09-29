// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Player3DControl
using UnityEngine;
using UnityEngine.UI;

public class Player3DControl : MonoBehaviour
{
	public static Player3DControl instance;

	public GameObject PlayerModel;

	public float forwardSpeed;

	public float strafeSpeed;

	public float hoverSpeed;

	private float activeForwardSpeed;

	private float activeStrafeSpeed;

	private float activeHoverSpeed;

	public float forwardAccleration;

	public float strafeAcceleration;

	public float hoverAccleration;

	private float NewSpeed;

	public float lookRateSpeed;

	private Vector2 lookinput;

	private Vector2 screenCenter;

	private Vector2 mouseDistance;

	private float rollinput;

	public float rollSpeed;

	public float RollAccleration;

	public GameObject Bullet;

	public GameObject Missile;

	public GameObject Gun1;

	public GameObject Gun2;

	public GameObject GunA;

	public GameObject GunB;

	public GameObject FireEffect1;

	public GameObject FireEffect2;

	public GameObject FireEffect3;

	private Rigidbody Mainbody;

	public float BodyHealth;

	public float GunBCooldown;

	private bool CanShootB;

	private float CounterB;

	public float GunMCooldown;

	private float CounterM;

	private bool CanShootM;

	public Slider Gun1Meter;

	public Slider Gun2Meter;

	public Slider GunAMeter;

	public Slider GunBMeter;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		screenCenter.x = (float)Screen.width * 0.5f;
		screenCenter.y = (float)Screen.height * 0.5f;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		BodyHealth = 100f;
		CounterB = GunBCooldown;
		CounterM = GunMCooldown;
		NewSpeed = forwardSpeed;
		Gun1Meter.maxValue = GunBCooldown;
		Gun2Meter.maxValue = GunBCooldown;
		Gun1Meter.value = CounterB;
		Gun2Meter.value = CounterB;
		GunAMeter.maxValue = GunMCooldown;
		GunBMeter.maxValue = GunMCooldown;
		GunAMeter.value = CounterM;
		GunBMeter.value = CounterM;
	}

	private void Update()
	{
		lookinput.x = Input.mousePosition.x;
		lookinput.y = Input.mousePosition.y;
		mouseDistance.x = (lookinput.x - screenCenter.x) / screenCenter.y;
		mouseDistance.y = (lookinput.y - screenCenter.y) / screenCenter.y;
		mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
		if (!GameManager.instance.IsDead && !GameManager.instance.IsPaused)
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				NewSpeed = forwardSpeed * 3f;
			}
			else if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				NewSpeed = forwardSpeed;
			}
			PlayerMover();
		}
		Gun1Meter.value = CounterB;
		Gun2Meter.value = CounterB;
		GunAMeter.value = CounterM;
		GunBMeter.value = CounterM;
		if (CounterB <= GunBCooldown)
		{
			CounterB -= Time.deltaTime;
			if (CounterB <= 0f)
			{
				CanShootB = true;
			}
		}
		if (CounterM <= GunMCooldown)
		{
			CounterM -= Time.deltaTime;
			if (CounterM <= 0f)
			{
				CanShootM = true;
			}
		}
		if (BodyHealth <= 50f)
		{
			FireEffect1.SetActive(value: true);
		}
		if (BodyHealth <= 30f)
		{
			FireEffect2.SetActive(value: true);
		}
		if (BodyHealth <= 10f)
		{
			FireEffect3.SetActive(value: true);
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (CanShootB)
			{
				FireBullet();
			}
			if (CanShootM)
			{
				FireMissile();
			}
		}
	}

	public void PlayerMover()
	{
		rollinput = Mathf.Lerp(rollinput, Input.GetAxisRaw("Roll"), RollAccleration * Time.deltaTime);
		transform.Rotate((0f - mouseDistance.y) * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollinput * rollSpeed * Time.deltaTime, Space.Self);
		activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * NewSpeed, forwardAccleration * Time.deltaTime);
		activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
		activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccleration * Time.deltaTime);
		transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
		transform.position += transform.right * activeStrafeSpeed * Time.deltaTime + transform.up * activeHoverSpeed * Time.deltaTime;
	}

	public void FireBullet()
	{
		Object.Instantiate(Bullet, GunA.transform.position, GunA.transform.rotation);
		Object.Instantiate(Bullet, GunB.transform.position, GunB.transform.rotation);
		CanShootB = false;
		CounterB = GunBCooldown;
	}

	public void FireMissile()
	{
		Object.Instantiate(Missile, Gun1.transform.position, Gun1.transform.rotation);
		Object.Instantiate(Missile, Gun2.transform.position, Gun2.transform.rotation);
		CanShootM = false;
		CounterM = GunMCooldown;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Portal")
		{
			GameManager.instance.ChallengeStart = true;
			GameManager.instance.CounterCh = 10f;
		}
		if (other.gameObject.tag == "Objects")
		{
			GameManager.instance.IsDead = true;
			PlayerModel.GetComponent<MeshDestroy>().enabled = true;
		}
	}
}
