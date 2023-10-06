// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// TargetMaker
using UnityEngine;

public class TargetMaker : MonoBehaviour
{
	public static TargetMaker instance;

	[SerializeField]
	private GameObject DestroyEffects;

	public bool IsDestroyed;

	public void Start()
	{
		IsDestroyed = false;
		instance = this;
		DestroyEffects.SetActive(false);
	}

	public void Update()
	{
		if (IsDestroyed)
		{
			DeadEffects();
			DeleteObjaftertime();
		}
	}

	private void DeleteObjaftertime()
	{
		Object.Destroy(gameObject, 10f);
	}

	private void DeadEffects()
	{
		DestroyEffects.SetActive(true);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			IsDestroyed = true;
		}
		if (collision.gameObject.tag == "Missle")
		{
			IsDestroyed = true;
		}
	}
}
