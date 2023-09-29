// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// DestroyerGuy
using UnityEngine;

public class DestroyerGuy : MonoBehaviour
{
	public float Dtime;

	private void Start()
	{
		Object.Destroy(gameObject, Dtime);
	}
}
