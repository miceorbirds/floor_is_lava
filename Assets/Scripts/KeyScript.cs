using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
	void OnTriggerEnter2D()
	{
		BallControlScript.PickUpKey();
		Destroy(this.gameObject);
	}
}
