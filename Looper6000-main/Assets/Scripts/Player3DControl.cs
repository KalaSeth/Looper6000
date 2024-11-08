// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Player3DControl
using UnityEngine;
using UnityEngine.UI;

public class Player3DControl : MonoBehaviour
{
	public static Player3DControl instance;

	public GameObject[] PlayerModel;

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

	public AudioSource ShipSound;
	float CurrentPitch = 0.74f; // NoShift
	float NewPitch = 0.93f;     // Shift
	float NoPitch = 0.41f;      // idle
	public AudioSource DeadSound;
	public AudioSource ShootSound;
	public AudioSource ShootSound2;
	public AudioSource Portalsf;
	public AudioSource bg;

	public Transform[] TPS;
	public Transform[] Cockpit;
	public Transform[] TOP;
	public Transform[] BackCam;
	public int PlayerIndex;

	private void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		PlayerIndex = PlayerPrefs.GetInt("Chip",0);
		PlayerModel[PlayerIndex].SetActive(true);

        screenCenter.x = (float)Screen.width * 0.5f;
		screenCenter.y = (float)Screen.height * 0.5f;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;

		ShipSound.pitch = NoPitch;

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
			ShipSound.volume = 0.5f;
			bg.volume = 0.375f;
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				NewSpeed = forwardSpeed * 3f;
			}
			else if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				NewSpeed = forwardSpeed;
			}
			PlayerMover();


			if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
			{
				ShipSound.pitch = Mathf.Lerp(ShipSound.pitch, NoPitch, 0.1f);
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				ShipSound.pitch = Mathf.Lerp(ShipSound.pitch, NewPitch, 0.1f);
			}
			else { ShipSound.pitch = Mathf.Lerp(ShipSound.pitch, CurrentPitch, 0.1f); }
		}
		else if (GameManager.instance.IsDead == true) {
			gameObject.GetComponent<BoxCollider>().enabled = false;
				ShipSound.volume = 0f; bg.volume = 0.125f;
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
			FireEffect1.SetActive(true);
		}
		if (BodyHealth <= 30f)
		{
			FireEffect2.SetActive(true);
		}
		if (BodyHealth <= 10f)
		{
			FireEffect3.SetActive(true);
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
		ShootSound.Play();
		Instantiate(Bullet, GunA.transform.position, GunA.transform.rotation);
		Instantiate(Bullet, GunB.transform.position, GunB.transform.rotation);
		CanShootB = false;
		CounterB = GunBCooldown;
	}

	public void FireMissile()
	{
		ShootSound2.Play();
		Instantiate(Missile, Gun1.transform.position, Gun1.transform.rotation);
		Instantiate(Missile, Gun2.transform.position, Gun2.transform.rotation);
		CanShootM = false;
		CounterM = GunMCooldown;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Portal")
		{
			Portalsf.Play();
			if (GameManager.instance.ChallengeStart == false)
			{
				GameManager.instance.ChallengeText.color = new Color32(167, 221, 229, 255);
                GameManager.instance.ChallengeText.text = "Challenge Started";
                GameManager.instance.ChallengeAnim.SetTrigger("Go");
                GameManager.instance.ChallengeBaseAnim.SetTrigger("Go");
            }
			other.GetComponentInParent<Portalthing>().AccessPortal();
			GameManager.instance.ChallengeStart = true;
			GameManager.instance.CounterCh = 11f;
			GameManager.instance.Loops++;
			GameManager.instance.Qloop++;
			GameManager.instance.LoopText.fontSize = 85;


        }
		if (other.gameObject.tag == "Objects")
		{
			if(GameManager.instance.IsDead == false)
            {
                DeadSound.Play();
                DeadSound.Play();
                GameManager.instance.DeadText.text = "-You Crashed-";
                GameManager.instance.IsDead = true;
				PlayerModel[PlayerIndex].GetComponent<MeshDestroy>().enabled = true;
            }
			
		}
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bound")
		{
			GameManager.instance.DeadText.text = "-Out of Bounds-";
			GameManager.instance.IsDead = true;
		}
    }
}
