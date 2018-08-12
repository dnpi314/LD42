using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launch : MonoBehaviour 
{
	int population;
	int cost;

	Text launchDisplay;

	void Start ()
	{
		launchDisplay = transform.GetChild (1).GetComponent<Text> ();
	}

	void Update ()
	{
		launchDisplay.text = string.Format ("{0}B", population);
	}

	public void SetValues (int p, int c)
	{
		population = p;
		cost = c;
	}

	public int GetCapacity ()
	{
		return population;
	}

	public int GetLaunchCost ()
	{
		return cost;
	}
}
