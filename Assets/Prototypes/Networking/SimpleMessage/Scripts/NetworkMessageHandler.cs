using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkMessageHandler : NetworkBehaviour
{
    public const short keypressed_msg = 1338;
	
	public class KeyPressedMessage : MessageBase
	{
        public string objectTransformName;
		public bool pressed = true;
	}
	
}
