using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessagePositionClient : MessageBase
{
    public Vector3 clientPosition = Vector3.zero;
    public int id = 0;
    public string clientName = "default";
    public Color clientColor = Color.white;
}
