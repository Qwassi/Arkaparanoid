using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
	public int i = -200;

	// Use this for initialization
	void Start ()
	{
		print ("This is my attempt number " + i);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Get current paddle position
		Vector3 paddlePos = new Vector3 (8f, this.transform.position.y, 0f);

		//Get mouse position
		float mousePos = (Input.mousePosition.x / Screen.width * 16)-0.5f;

		//Set new mouse X position for the background with x = -1
		//paddlePos.x = Mathf.Clamp (mousePos, -0.3f, 15.4f);

		//Set new mouse X position
		//Below is the most correct values, which is for the background x = 0
		paddlePos.x = Mathf.Clamp (mousePos, 0.72f, 15.4f);

		this.transform.position = paddlePos;
	}
}
