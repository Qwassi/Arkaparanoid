using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{

	public int timesHit;
	public int maxHit;

	// Use this for initialization
	void Start ()
	{
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		print ("Brick got hit!");
		timesHit++;
		if (timesHit == maxHit) {
			Destroy (gameObject);
		}
	}
}
