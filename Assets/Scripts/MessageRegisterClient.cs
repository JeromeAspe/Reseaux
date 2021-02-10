using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageRegisterClient : MessageBase
{
    public string clientName = "default";
    public Vector3 clientPosition = Vector3.zero;
    public int id = 0;
    public Color clientColor = Color.white;
}
