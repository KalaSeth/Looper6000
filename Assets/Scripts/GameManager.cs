// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// GameManager
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public Camera[] OutCam;

	private int CamImdex;

	public bool IsDead;

	public bool IsPaused;

	public bool ChallengeStart;

	public GameObject ChallengeHUD;

	public Text ChallengeTimerText;

	public float CounterCh;

	public GameObject DeathHUD;

	public Slider Health;

	public float Coins;

	public Text CoinText;

	public GameObject PauseMenu;

	public void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		OutCam[0].enabled = true;
		IsDead = false;
		IsPaused = false;
		ChallengeStart = false;
		Health.maxValue = 100f;
		Health.value = Player3DControl.instance.BodyHealth;
		PauseMenu.SetActive(value: false);
		CounterCh = 10f;
	}

	private void Update()
	{
		Health.value = Player3DControl.instance.BodyHealth;
		CoinText.text = "Coins " + Coins;
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (CamImdex < OutCam.Length - 1)
			{
				CamImdex++;
			}
			else if (CamImdex == OutCam.Length - 1)
			{
				CamImdex = 0;
			}
			Camswitcher(CamImdex);
		}
		if (Input.GetKeyDown(KeyCode.Escape) && !IsDead)
		{
			if (!IsPaused)
			{
				IsPaused = true;
				LevelSwitcher.instance.PauseGame();
				PauseMenu.SetActive(value: true);
			}
			else if (IsPaused)
			{
				IsPaused = false;
				LevelSwitcher.instance.ResumeGame();
				PauseMenu.SetActive(value: false);
			}
		}
		if (Player3DControl.instance.BodyHealth <= 0f)
		{
			IsDead = true;
		}
		if (IsDead)
		{
			DeathHUD.SetActive(value: true);
			Cursor.visible = true;
		}
		if (!ChallengeStart)
		{
			return;
		}
		if (CounterCh <= 10f)
		{
			CounterCh -= Time.deltaTime;
			ChallengeTimerText.text = "Timer " + (int)CounterCh;
			if (CounterCh <= 0f)
			{
				ChallengeStart = false;
			}
		}
		ChallengeBegin();
	}

	public void ChallengeBegin()
	{
		ChallengeHUD.SetActive(value: true);
	}

	private void Camswitcher(int Index)
	{
		OutCam[Index].enabled = true;
		for (int i = 0; i < OutCam.Length; i++)
		{
			if (i != Index)
			{
				OutCam[i].enabled = false;
			}
		}
	}
}
