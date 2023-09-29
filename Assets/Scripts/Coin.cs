// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Coin
using UnityEngine;

public class Coin : MonoBehaviour
{
	public Vector3 RotateAmmount;

	private Animator CoinAnim;

	private void Start()
	{
		CoinAnim = GetComponent<Animator>();
	}

	private void Update()
	{
		base.transform.Rotate(RotateAmmount * Time.deltaTime);
	}

	private void DestroyCoin()
	{
		RotateAmmount = new Vector3(0f, 1000f, 0f);
		CoinAnim.SetTrigger("Dead");
		base.gameObject.GetComponent<BoxCollider>().enabled = false;
		GameManager.instance.Coins += 1f;
		Object.Destroy(base.gameObject, 0.12f);
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
			DestroyCoin();
		}
	}
}
