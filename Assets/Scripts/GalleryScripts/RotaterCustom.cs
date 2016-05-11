using UnityEngine;
using System.Collections;

public class RotaterCustom : MonoBehaviour
{
	//Customizable variables
	[Tooltip("Roate the object's x-axis.")]
	public float rotateX = 0f;
	[Tooltip("Roate the object's y-axis.")]
	public float rotateY = 0f;
	[Tooltip("Roate the object's z-axis.")]
	public float rotateZ = 0f;
	[Tooltip("The speed of the rotation of the object.")]
	public float speed = 1f;
	[Tooltip("Use world space for the rotation, else if not selected will use local space to rotate.")]
	public bool worldSpace = false;

	//Update is called once per frame
	void Update()
	{
		//Rotate the object
		transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime * speed, worldSpace ? Space.World : Space.Self);
	}
}