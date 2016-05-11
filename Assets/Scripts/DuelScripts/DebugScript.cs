using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour
{
	public bool debugMode;
	public MainGameScript mgs;
	
	public float distanceDebug = 0f;
	public int handSizeDebug = 1;
	public float segmentDebug = 1f;
	public float amountDebug = 1f;
	public float offsetDebug = 0f;

	//Update is called once per frame
	void Update()
	{
		//If we are in debug mode...
		if(debugMode)
		{
			//Draw lines from the rays
			Debug.DrawLine(mgs.P1DrawRay.transform.position, new Vector3(mgs.P1DrawRay.transform.position.x, Vector3.down.y * mgs.maxDistance, mgs.P1DrawRay.transform.position.z), Color.red, 7, true);
			Debug.DrawLine(mgs.P2DrawRay.transform.position, new Vector3(mgs.P2DrawRay.transform.position.x, Vector3.down.y * mgs.maxDistance, mgs.P2DrawRay.transform.position.z), Color.red, 7, true);
			Debug.DrawLine(mgs.P1ExtraDeckRay.transform.position, new Vector3(mgs.P1ExtraDeckRay.transform.position.x, Vector3.down.y * mgs.maxDistance, mgs.P1ExtraDeckRay.transform.position.z), Color.red, 7, true);
			Debug.DrawLine(mgs.P2ExtraDeckRay.transform.position, new Vector3(mgs.P2ExtraDeckRay.transform.position.x, Vector3.down.y * mgs.maxDistance, mgs.P2ExtraDeckRay.transform.position.z), Color.red, 7, true);
			
			//Draw lines from the hand limits
			Debug.DrawLine(mgs.P1HandRightSideLimit.transform.position, mgs.P1HandLeftSideLimit.transform.position, Color.green, 7, true);
			Debug.DrawLine(mgs.P2HandRightSideLimit.transform.position, mgs.P2HandLeftSideLimit.transform.position, Color.green, 7, true);
			
			//Player sight
			Debug.DrawLine(mgs.mouseRayP1.origin, mgs.mouseRayP1.GetPoint(20), Color.cyan, 0.5f);
			Debug.DrawLine(mgs.mouseRayP2.origin, mgs.mouseRayP2.GetPoint(20), Color.cyan, 0.5f);
		}
		
		//If debug mode, switch cameras to simulate player interaction
		if(Input.GetKeyDown("tab") /*&& debugMode*/)
		{
			//Switch cameras
			mgs.player1.GetComponent<Camera>().depth = -mgs.player1.GetComponent<Camera>().depth;
			mgs.player2.GetComponent<Camera>().depth = -mgs.player2.GetComponent<Camera>().depth;
		}
	}
	
	void OnDrawGizmos()
	{
		//If debug mode is on...
		if(debugMode)
		{
			//Draw a test cube
			Gizmos.color = Color.red;
			
			segmentDebug = amountDebug;
			if(segmentDebug == 1)
			{
				segmentDebug++;
			}
			
			offsetDebug = distanceDebug / segmentDebug;
			distanceDebug = Vector3.Distance(mgs.P1HandLeftSideLimit.transform.position, mgs.P1HandRightSideLimit.transform.position);
			for(int i = 0; i < amountDebug; i++)
			{
				Gizmos.DrawCube(Vector3.Lerp(new Vector3(mgs.P1HandLeftSideLimit.transform.position.x + offsetDebug, mgs.P1HandLeftSideLimit.transform.position.y, mgs.P1HandLeftSideLimit.transform.position.z), mgs.P1HandRightSideLimit.transform.position, i / (segmentDebug + handSizeDebug)), new Vector3(1, 1, 3));
			}
		}
	}
	
	public void DrawFieldCheck(Vector3 start, Vector3 end, bool hit = false)
	{
		if(hit)
		{
			Debug.DrawLine(start, end, Color.black, 7);
		}
		else
		{
			Debug.DrawLine(start, end, Color.white, 7);
		}
	}
	
	public void DrawAttackCheck(Vector3 start, Vector3 end)
	{
		Debug.DrawLine(start, end, Color.magenta, 7);
	}
	
	public void DrawSummonCheck(Vector3 start, Vector3 end)
	{
		Debug.DrawLine(start, end, Color.green, 7);
	}
	
	public void DrawSelectionCheck(Vector3 start, Vector3 end)
	{
		Debug.DrawLine(start, end, Color.yellow, 7);
	}
	
	public void DrawActivateCheck(Vector3 start, Vector3 end)
	{
		Debug.DrawLine(start, end, Color.blue, 7);
	}
}