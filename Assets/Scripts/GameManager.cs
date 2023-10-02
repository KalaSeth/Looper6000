// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// GameManager

using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public Camera[] OutCam;
	public Transform[] CamSer; 
	private int CamImdex;

	public int PlayerLevel;
	public Text Leveltext;
	public Animator Levelup;
	public int XP;
	public Slider XpSlider;
	public Animator XPup;

	public Text DeadText;
	public bool IsDead;
	public bool IsPaused;
	public bool ChallengeStart;

	public GameObject ChallengeHUD;
	public Text ChallengeTimerText;
	public Text ChallengeText;
	public Animator ChallengeAnim;
	public Animator ChallengeBaseAnim;
	public AudioSource OutOfTimeAudio;

	public Text LoopText;
	public Text BestLoopText;
	public int Loops;
	public int BestLoops;
	public int Kills;
	int MainKill;
	public Text KillText;
	public Animator KillUP;
	public int Qkill;

	public Text Quest;
	public Text TaskLeft;
	[System.NonSerialized]
	public string QuestText;
	public bool QuestComplete;
	public float TaskAmmount;
	int TaskIndex; // 0 = loops; 1 = coins; 2 = balls; 3 = enemy;
	public int Qloop = 0;
	public int Qcoins = 0;


	public float CounterCh;

	public GameObject DeathHUD;

	public Slider Health;
	public float Coins;
	public Text CoinText;
	public Animator Coinup;

	public GameObject PauseMenu;
	public GameObject HelpHUD;

	public int Tutorial1;
	public int Tutorial2;
	public int Tutorial3;

	public void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		PlayerLevel = PlayerPrefs.GetInt("Level",1);
		TaskAmmount = PlayerPrefs.GetInt("TaskAm",4);
		MainKill = PlayerPrefs.GetInt("Kills", 0);

		Kills = 0;
        CamSer[0] = Player3DControl.instance.Cockpit[Player3DControl.instance.PlayerIndex];
        CamSer[1] = Player3DControl.instance.TOP[Player3DControl.instance.PlayerIndex];
        CamSer[2] = Player3DControl.instance.BackCam[Player3DControl.instance.PlayerIndex];

        TaskIndex = PlayerPrefs.GetInt("TaskIn", 0);
		Tutorial1 = PlayerPrefs.GetInt("Tut1", 0);
		Tutorial2 = PlayerPrefs.GetInt("Tut2", 0);
		Tutorial3 = PlayerPrefs.GetInt("Tut3", 0);

		XpSlider.maxValue = 100 * PlayerLevel + (PlayerLevel / 2);
        XP = PlayerPrefs.GetInt("xp", 0);
        XpSlider.value =  XP;
		

		OutCam[0].enabled = true;
		Camswitcher(0);
		IsDead = false;
		IsPaused = false;
		QuestComplete = false;

        CounterCh = 11f;
        ChallengeStart = false;
		Health.maxValue = 11;
		Health.value = CounterCh; //Player3DControl.instance.BodyHealth;
		PauseMenu.SetActive(false);
		
		QuestChecker();

		BestLoops = PlayerPrefs.GetInt("BestLoop", 0);
    }

	public void Update()
	{
        PlayerLevel = PlayerPrefs.GetInt("Level", 1);
        TaskAmmount = PlayerPrefs.GetInt("TaskAm", 4);
        TaskIndex = PlayerPrefs.GetInt("TaskIn", 0);
        MainKill = PlayerPrefs.GetInt("Kills", 0);
		Coins = PlayerPrefs.GetInt("Coins", 0);

        if (Kills >= MainKill)
		{
			MainKill = Kills;
			PlayerPrefs.SetInt("Kills", MainKill);
		}

        Leveltext.text = PlayerLevel.ToString();
        Health.value = CounterCh;
		CoinText.text = "x" + Coins.ToString();
		KillText.text = "x" + Kills.ToString();
        Quest.text = QuestText;
        XpSlider.maxValue = 100 * PlayerLevel;
        XpSlider.value = XP;
		if (XpSlider.value == XpSlider.maxValue) {
			Levelup.SetTrigger("Go");
            PlayerLevel++; PlayerPrefs.SetInt("Level", PlayerLevel);
			XP = 0; PlayerPrefs.SetInt("xp", XP);
		}



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
				ChallengeText.color = new Color32(255, 66, 55, 255);
                ChallengeStart = false;
				ChallengeText.text = "Challenge Failed";
				ChallengeAnim.SetTrigger("Go");
				ChallengeBaseAnim.SetTrigger("Go");
                OutOfTimeAudio.Play();
				Loops = 0;
				Qloop = 0;
				Qcoins = 0;
				Qkill = 0;
			}
		}

		ChallengeBegin();
		QuestChecker();
		
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

	public void QuestChecker()
	{
		if (QuestComplete == true)
		{
			ChallengeText.color = new Color32(112, 236, 143, 255);
            ChallengeText.text = "Challenge Completed";
            ChallengeBaseAnim.SetTrigger("Go");
            ChallengeAnim.SetTrigger("Go");
            XP += 30 * Random.Range( PlayerLevel, PlayerLevel * 2);
			XPup.GetComponent<Text>().text = XP.ToString() + "xp+";
			XPup.SetTrigger("Go");
            PlayerPrefs.SetInt("xp", XP);
			Debug.Log(XP);

			RandomQuestGen();
		}
		if (TaskIndex == 0) { QuestText = "Pass throug " + TaskAmmount.ToString() + " Portal before juice runs out!";
		   if (Qloop >= TaskAmmount)
			{
				Qloop = 0;
				QuestComplete = true;
			}
			TaskLeft.text = (TaskAmmount - Qloop).ToString() + "/" + TaskAmmount.ToString() + " left";
		}
		else if (TaskIndex == 1) { QuestText = "Destroy " + TaskAmmount.ToString() + " Coins before juice runs out!";
            if (Qcoins >= TaskAmmount)
            {
				Qcoins = 0;
                QuestComplete = true;
            }
            TaskLeft.text = (TaskAmmount - Qcoins).ToString() + "/" + TaskAmmount.ToString() + " left";
        }
		else if (TaskIndex == 2) { QuestText = "Destroy " + TaskAmmount.ToString() + " Red Base before juice runs out!";
            if (Qkill >= TaskAmmount)
            {
                Qkill = 0;
                QuestComplete = true;
            }
            TaskLeft.text = (TaskAmmount - Qkill).ToString() + "/" + TaskAmmount.ToString() + " left";
        }
		else if (TaskIndex == 3) { QuestText = "Kill " + TaskAmmount.ToString() + " Enemies before juice runs out!";

		}

	}


	public void RandomQuestGen()
	{
		int Xgroup = Random.Range(0, 2);
		int XAmmount = Random.Range(4,11);

		if (Xgroup == 0) // Pass Loops	
		{
			TaskIndex = Xgroup;
            TaskAmmount = XAmmount;
			PlayerPrefs.SetInt("TaskAm", XAmmount);
			PlayerPrefs.SetInt("TaskIn", Xgroup);

        }
        else if( Xgroup == 1) // Collect Coins while passing loops
		{
            XAmmount = Random.Range(1, 10);
            TaskIndex = Xgroup;
            TaskAmmount = XAmmount;
            PlayerPrefs.SetInt("TaskAm", XAmmount);
            PlayerPrefs.SetInt("TaskIn", Xgroup);
        }
        else if( Xgroup == 2) // Destroy Balls while passing throug loops
		{
            XAmmount = Random.Range(1, 10);
            TaskIndex = Xgroup;
            TaskAmmount = XAmmount;
            PlayerPrefs.SetInt("TaskAm", XAmmount);
            PlayerPrefs.SetInt("TaskIn", Xgroup);
        }
        else if (Xgroup == 3) // Destroy Enemies While passing loops
		{
            TaskIndex = Xgroup;
            TaskAmmount = XAmmount;
            PlayerPrefs.SetInt("TaskAm", XAmmount);
            PlayerPrefs.SetInt("TaskIn", Xgroup);
        }
        QuestComplete = false;
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
