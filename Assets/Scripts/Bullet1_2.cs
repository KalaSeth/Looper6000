// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Bullet1_2
using UnityEngine;

public class Bullet1_2 : MonoBehaviour
{
	public float speed;

	public float DesTime;

	public GameObject Flash;

	private void Start()
	{
		Object.Destroy(base.gameObject, DesTime);
	}

	private void Update()
	{
		base.transform.position += base.transform.forward * speed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Object.Instantiate(Flash, base.transform.position, base.transform.rotation);
	}
}
