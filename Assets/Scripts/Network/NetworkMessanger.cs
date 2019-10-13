using UnityEngine;
using UnityEngine.Networking;

public class NetworkMessanger : NetworkMessageHandler
{
	[SerializeField]
	private Game game;

	public const short snake_msg = 1337;

	
	
	public override void OnStartServer()
	{
		Debug.Log("OnStartServer");
		base.OnStartServer();

		RegisterNetworkMessages();		
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		Debug.Log("OnStartClient");
		RegisterNetworkMessages();
	}


	private void RegisterNetworkMessages()
	{
		Debug.Log("RegisterNetworkMessages");
		//NetworkManager.singleton.client.RegisterHandler(snake_msg, OnReceiveMovementMessage);
		if(isServer)
		{
			Debug.Log("isServer");
			NetworkServer.RegisterHandler(snake_msg, OnReceiveConfirmMessage);
		}
		else
		{
			NetworkManager.singleton.client.RegisterHandler(snake_msg, OnReceiveConfirmMessage);
		}
	}

	private void OnReceiveConfirmMessage(NetworkMessage pMessage)
	{
		SnakeConfirmMessage _msg = pMessage.ReadMessage<SnakeConfirmMessage>();
		Debug.Log("OnReceiveConfirmMessage " + _msg.snakeId);
		if(isServer)
		{
			Debug.Log("SendToAll " + _msg.snakeId);
			NetworkServer.SendToAll(snake_msg, _msg);
		}

		game.OnReceiveConfirmMessage(_msg);
	}

	public void SendConfirmMessage(int pSnakeId, EDirection pDirection)
	{
		if(!IsMultiplayer())
			return;

		Debug.Log($"SendMessage {pSnakeId} - {pDirection}");
		SnakeConfirmMessage msg = new SnakeConfirmMessage();
		msg.snakeId = pSnakeId;
		msg.direction = pDirection;

		NetworkManager.singleton.client.Send(snake_msg, msg);
	}

	public void OnConnected(NetworkMessage pMessage)
	{
		Debug.Log("Connected to server");
	}

	public static bool IsMultiplayer()
	{
		return NetworkManager.singleton.isNetworkActive;
	}
}

