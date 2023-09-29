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
	}

	private void LateUpdate()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, targetPoint.rotation, rotateSpeed * Time.deltaTime);
	}
}
