using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorTooltip : MonoBehaviour 
{
	static GameObject tooltip;
	Text tooltipText;
	static string message;

	void Start () 
	{
		tooltip = gameObject;
		tooltipText = GetComponentInChildren<Text> ();
		tooltip.SetActive (false);
	}

	void Update ()
	{
		tooltipText.text = message;
	}

	public static void OpenTooltip (string s)
	{
		tooltip.SetActive (true);
		message = s;
	}

	public void CloseTooltip ()
	{
		tooltip.SetActive (false);
	}
}
