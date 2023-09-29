// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// SelfRotation
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
	public float Speed;

	public void Update()
	{
		base.transform.Rotate(Vector3.up, Speed * Time.deltaTime);
	}
}
