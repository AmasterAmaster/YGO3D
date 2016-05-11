using UnityEngine;

public static class CameraExtensions
{
	//Uses the camera and an interger to show the layer...
	public static void LayerCullingShow(this Camera cam, int layerMask)
	{
		//Turn on this layer
		cam.cullingMask |= layerMask;
	}
	
	//Uses the camera and a string to show the layer...
	public static void LayerCullingShow(this Camera cam, string layer)
	{
		//Turn on this layer (using the above function)
		LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
	}
	
	//Uses the camera and an interger to hide the layer...
	public static void LayerCullingHide(this Camera cam, int layerMask)
	{
		//Turn off this layer
		cam.cullingMask &= ~layerMask;
	}
	
	//Uses the camera and a string to hide the layer...
	public static void LayerCullingHide(this Camera cam, string layer)
	{
		//Turn off this layer (using the above function)
		LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
	}
	
	//Uses the camera and an interger to toggle the layer...
	public static void LayerCullingToggle(this Camera cam, int layerMask)
	{
		//Changes the bit within this layer (toggles)
		cam.cullingMask ^= layerMask;
	}
	
	//Uses the camera and a string to toggle the layer...
	public static void LayerCullingToggle(this Camera cam, string layer)
	{
		//Toggles this layer (using the above function)
		LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
	}
	
	//Uses the camera and an interger to includes the layer... (Is this layer included?)
	public static bool LayerCullingIncludes(this Camera cam, int layerMask)
	{
		//Includes this layer
		return (cam.cullingMask & layerMask) > 0;
	}
	
	//Uses the camera and a string to includes the layer... (Is this layer included?)
	public static bool LayerCullingIncludes(this Camera cam, string layer)
	{
		//Includes this layer (using the above function)
		return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
	}
	
	//A more robust version of toggling a layer...
	public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn)
	{
		//Get the included layer
		bool included = LayerCullingIncludes(cam, layerMask);
		
		//If this layer is on and not included
		if(isOn && !included)
		{
			//Show the layer
			LayerCullingShow(cam, layerMask);
		}
		//Else, the layer is not on and included
		else if(!isOn && included)
		{
			//Hide the layer
			LayerCullingHide(cam, layerMask);
		}
	}
	
	//A more robust version of toggling a layer...
	public static void LayerCullingToggle(this Camera cam, string layer, bool isOn)
	{
		//Uses a more robust version of toggling the layer (using the above version)
		LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
	}
}