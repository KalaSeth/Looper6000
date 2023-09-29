// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// CameraController
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController instance;

	public Transform targetPoint;

	public float moveSpeed = 8f;

	public float rotateSpeed = 3f;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		//MountIndex = PlayerPrefs.GetInt("PP", 0);
	}

	private void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, rotateSpeed * Time.deltaTime);
	}
}
