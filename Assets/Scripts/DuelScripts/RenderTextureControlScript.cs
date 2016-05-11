using UnityEngine;
using System.Collections;

public class RenderTextureControlScript : MonoBehaviour
{
	//Variables
	public bool renderControl = true;
	public Camera overviewCamera;

	//Use this for initialization
	void Start()
	{
		if(renderControl)
		{
			#if UNITY_STANDALONE_OSX
			//Is this render texture actually created...
			if(!overviewCamera.targetTexture.IsCreated())
			{
				overviewCamera.targetTexture.format = RenderTextureFormat.Default;
				overviewCamera.targetTexture.Create();
			}
			#endif
		}
	}

	
	void OnBecomeVisible()
	{
		if(renderControl)
		{
			this.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
	
	void OnBecomeInvisible()
	{
		if(renderControl)
		{
			overviewCamera.targetTexture.Release();
			overviewCamera.targetTexture.DiscardContents();
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}
}