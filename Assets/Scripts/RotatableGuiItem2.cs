using UnityEngine;

public class RotatableGuiItem2 : MonoBehaviour
{
	public static Texture2D originalTexture;
	public Vector2 offset;
	public Vector2 tiling;
	public float rot = 90f;
	
//	void Update ()
//	{
//		Matrix4x4 matrix = Matrix4x4.TRS(offset, Quaternion.Euler(0, 0, rot), tiling);
//		this.GetComponent<Renderer>().material.SetMatrix("_Matrix", matrix);
//	}
	
	public static void SetOriginalTexture(Texture2D tex)
	{
		originalTexture = tex;
	}
	
	public static Texture2D rotateTexture()
	{
		//Get ready to rotate the image
		Texture2D modifiedTexture = originalTexture;
		//Matrix4x4 matrix = Matrix4x4.TRS(offset, Quaternion.Euler(0, 0, rot), tiling);
		Color[ , ] newColorArrayWidth = new Color[originalTexture.height, originalTexture.width];
		
		//Deconstruct the image
		for(int i = 0; i < originalTexture.height - 140; i++)
		{
			for(int j = 0; j < originalTexture.width - 141; j++)
			{
				newColorArrayWidth[originalTexture.width - 1 - j, i] = originalTexture.GetPixel(j, i);
			}
		}
		
		//Reconstruct the rotated image
		for(int i = 0; i < originalTexture.height - 140; i++)
		{
			for(int j = 0; j < originalTexture.width - 141; j++)
			{
				modifiedTexture.SetPixel(j, i, newColorArrayWidth[j, i]);
			}
		}
		
		//Send the modified version, not the original
		return modifiedTexture;
	}
}