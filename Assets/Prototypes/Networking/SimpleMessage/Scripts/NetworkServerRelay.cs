using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkServerRelay : NetworkMessageHandler
{
    private void Start()
    {
        if(isServer)
        {
            RegisterNetworkMessages();
        }
    }

    private void RegisterNetworkMessages()
    {
		Debug.Log("server RegisterNetworkMessages");

        NetworkServer.RegisterHandler(keypressed_msg, OnReceiveKeyPressedMessage);
	}

	private void OnReceiveKeyPressedMessage(NetworkMessage _message)
	{
		Debug.Log("server OnReceiveKeyPressedMessage");
		KeyPressedMessage _msg = _message.ReadMessage<KeyPressedMessage>();
		NetworkServer.SendToAll(keypressed_msg, _msg);
	}

}
