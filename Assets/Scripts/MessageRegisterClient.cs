﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageRegisterClient : MessageBase
{
    public string clientName = "default";
    public Client client = null;
    public int id = 0;
}