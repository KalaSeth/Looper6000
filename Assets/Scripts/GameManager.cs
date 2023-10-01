// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// GameManager
using System;
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
	public Text ChallengeText;
	public Animator ChallengeAnim;
	public AudioSource OutOfTimeAudio;

	public Text LoopText;
	public Text BestLoopText;
	public int Loops;
	public int BestLoops;
	public int Kills;

	public Text Quest;
	[NonSerialized]
	public string QuestText;

	public float CounterCh;

	public GameObject DeathHUD;

	public Slider Health;
	public float Coins;
	public Text CoinText;

	public GameObject PauseMenu;
	public GameObject HelpHUD;

	public void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		OutCam[0].enabled = true;
		Camswitcher(0);
		IsDead = false;
		IsPaused = false;

        CounterCh = 11f;
        ChallengeStart = false;
		Health.maxValue = 11;
		Health.value = CounterCh; //Player3DControl.instance.BodyHealth;
		PauseMenu.SetActive(false);

		BestLoops = PlayerPrefs.GetInt("BestLoop", 0);
	}

	private void Update()
	{
		Health.value = CounterCh;
		CoinText.text = "Coins " + Coins;
		LoopText.fontSize = (int)Mathf.Lerp(LoopText.fontSize, 45, 0.05f);
		BestLoopText.text = BestLoops.ToString();
		if (ChallengeStart == false) { LoopText.text ="0x"; ChallengeTimerText.text = " "; }
		else if (ChallengeStart == true) {
			LoopText.text = Loops.ToString() + "x";
			ChallengeTimerText.text = ((int)CounterCh).ToString() + " sec"; }

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
				IsPaused = true; Player3DControl.instance.ShipSound.Stop();
                LevelSwitcher.instance.PauseGame();
				PauseMenu.SetActive(true);
			}
			else if (IsPaused)
			{
				ResumeBut();
            }
		}
		if (Player3DControl.instance.BodyHealth <= 0f)
		{
			IsDead = true;
		}
		if (IsDead)
		{
			DeathHUD.SetActive(true);
			Cursor.visible = true;
		}
		if (!ChallengeStart)
		{
			return;
		}
		if (CounterCh <= 11f && IsDead != true)
		{
			CounterCh -= Time.deltaTime;
			
			if (CounterCh <= 0f)
			{
				ChallengeStart = false;
				ChallengeText.text = "Challenge Failed";
				ChallengeAnim.SetTrigger("Go");
                OutOfTimeAudio.Play();
				Loops = 0;
			}
		}
		ChallengeBegin();

		if (Loops >= BestLoops)
		{
			BestLoops = Loops;
			PlayerPrefs.SetInt("BestLoop", BestLoops);
		}


	}

	public void ResumeBut()
	{
        Player3DControl.instance.ShipSound.Play();
        HelpHUD.SetActive(false);
        IsPaused = false;
        LevelSwitcher.instance.ResumeGame();
        PauseMenu.SetActive(false);
    }

	public void ChallengeBegin()
	{
		ChallengeHUD.SetActive(true);
	}

	public void RandomQuestGen()
	{
		QuestText = "Pass throug 3 Portal before juice runs out!";
		QuestText = "Collect 4 Coins before juice runs out!";
		QuestText = "Destroy 2 Red Ball before Portal Juice runs out.";
		QuestText = "Destroy 10 Red Ball before Portal Juice runs out.";
		QuestText = "Destroy 10 Red Ball before Portal Juice runs out.";
		QuestText = "Destroy 10 Red Ball before Portal Juice runs out.";
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
