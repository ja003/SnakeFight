using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Debugger : MonoBehaviour
{
	[SerializeField]
	private bool hideDebugUI;

	[SerializeField]
	private GameObject debugConsole;
	[SerializeField]
	private NetworkManagerHUD networkHud;

	[SerializeField]
	private bool onePlayer;
	public static bool OnePlayer;

	void Start()
    {
		OnePlayer = onePlayer;
		if(hideDebugUI)
		{
			debugConsole.SetActive(false);
			networkHud.enabled = false;
		}
    }

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			ChangeTimeScale(0.1f);
		}
		else if(Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			ChangeTimeScale(-0.1f);
		}
	}

	private static void ChangeTimeScale(float pDiff)
	{
		Time.timeScale += pDiff;
		Debug.Log("Set timescale to " + Time.timeScale);
	}
}
