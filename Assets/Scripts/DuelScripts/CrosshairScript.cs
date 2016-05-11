using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour
{
	public Texture2D crosshairTexture;
	public Rect position;
	public bool cursorOn = true;
	public bool SearchForCursorToLock = false;
	
	public bool debugMode = false;
	
	//Use this for initialization
	void Start()
	{
		position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
	}
	
	void Update()
	{
		if(Input.GetButtonDown("Fire1") && SearchForCursorToLock)
		{
			Cursor.lockState = CursorLockMode.Locked;
			
			if(debugMode)
			{
				Cursor.visible = true;
			}
			else
			{
				Cursor.visible = false;
			}
		}
	}
	
	void OnGUI()
	{
		if(cursorOn == true)
		{
			GUI.DrawTexture(position, crosshairTexture);
		}
	}
	
	public void TurnOn()
	{
		cursorOn = true;
		Cursor.lockState = CursorLockMode.Locked;
		
		if(debugMode)
		{
			Cursor.visible = true;
		}
		else
		{
			Cursor.visible = false;
		}
	}
	
	public void TurnOff()
	{
		cursorOn = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	public void CaptureMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
}