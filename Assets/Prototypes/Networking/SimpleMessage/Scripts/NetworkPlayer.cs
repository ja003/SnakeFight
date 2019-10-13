using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using PlayerManager;
using System;

public class NetworkPlayer : NetworkMessageHandler
{
	[Header("Player Properties")]
	public string playerID;
	
	private void Start()
	{
		playerID = "player" + GetComponent<NetworkIdentity>().netId.ToString();
		transform.name = playerID;
		Manager.Instance.AddPlayerToConnectedPlayers(playerID, gameObject);

		if(isLocalPlayer)
		{
			Manager.Instance.SetLocalPlayerID(playerID);

			Camera.main.transform.position = transform.position + new Vector3(0, 0, -20);
			Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

			RegisterNetworkMessages();
		}
	}

	private void RegisterNetworkMessages()
	{
		Debug.Log("RegisterNetworkMessages");
		NetworkManager.singleton.client.RegisterHandler(keypressed_msg, OnReceiveKeyPressedMessage);
	}
	
	private void OnReceiveKeyPressedMessage(NetworkMessage _message)
	{
		Debug.Log("OnReceiveKeyPressedMessage");
		KeyPressedMessage _msg = _message.ReadMessage<KeyPressedMessage>();

		if(_msg.objectTransformName != transform.name)
		{
			Manager.Instance.ConnectedPlayers[_msg.objectTransformName].GetComponent<NetworkPlayer>().ReceiveKeyPressedMessage(_msg);
		}
	}

	private void ReceiveKeyPressedMessage(KeyPressedMessage msg)
	{
		Debug.Log("ReceiveKeyPressedMessage " + msg.objectTransformName);
	}

	private void Update()
	{
		if(isLocalPlayer)
		{
			if(Input.GetKeyDown(KeyCode.S))
			{
				Debug.Log("S");
				SendKeyPressedMessage();
			}
		}
	}

	private void SendKeyPressedMessage()
	{
		KeyPressedMessage _msg = new KeyPressedMessage()
		{
			objectTransformName = playerID,
			pressed = true
		};
		NetworkManager.singleton.client.Send(keypressed_msg, _msg);
	}
}
