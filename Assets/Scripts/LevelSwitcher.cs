// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// LevelSwitcher
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
	public static LevelSwitcher instance;

	private void Awake()
	{
		instance = this;
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		ResumeGame();
	}

	public void Playgame()
	{
		SceneManager.LoadScene(1);
		ResumeGame();
	}

	public void Menugame()
	{
		SceneManager.LoadScene(0);
		ResumeGame();
	}

	public void PauseGame()
	{
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{	
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
