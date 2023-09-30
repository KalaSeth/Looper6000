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
		Destroy(gameObject, DesTime);
	}

	private void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Instantiate(Flash, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
