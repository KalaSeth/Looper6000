// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Coin
using UnityEngine;

public class Coin : MonoBehaviour
{
	public Vector3 RotateAmmount;

	private Animator CoinAnim;
	public AudioSource CollectSound;

	private void Start()
	{
		CoinAnim = GetComponent<Animator>();
	}

	private void Update()
	{
		transform.Rotate(RotateAmmount * Time.deltaTime);
	}

	private void DestroyCoin()
	{
		CollectSound.Play();
		RotateAmmount = new Vector3(0f, 1000f, 0f);
		CoinAnim.SetTrigger("Dead");
		gameObject.GetComponent<BoxCollider>().enabled = false;
		GameManager.instance.Coins += 1f;
		GameManager.instance.Qcoins++;
        GameManager.instance.Coinup.SetTrigger("Go");
        Destroy(gameObject, 1f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			DestroyCoin();
		}
		if (collision.gameObject.tag == "Missle")
		{
			DestroyCoin();
		}
		if (collision.gameObject.tag == "Player")
		{
			GameManager.instance.IsDead = true;
			//DestroyCoin();
		}
	}
}
