﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerScript : MonoBehaviour
{
	// Start is called before the first frame update
	void OnTriggerEnter2D(Collider2D col)
	{
		BallControlScript.OpenLocker();
	}
}
