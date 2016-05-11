using UnityEngine;
using System.Collections;

public class MoveCustom : MonoBehaviour
{
	[Tooltip("Move the object in the x-axis.")]
	public float moveX = 0f;
	[Tooltip("Move the object in the y-axis.")]
	public float moveY = 0f;
	[Tooltip("Move the object in the z-axis.")]
	public float moveZ = 0f;
	[Tooltip("The speed of the object.")]
	public float speed = 1f;

	void Update()
	{
		transform.Translate(new Vector3(moveX, moveY, moveZ) * speed * Time.deltaTime);
	}
}