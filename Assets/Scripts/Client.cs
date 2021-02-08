using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    NetworkClient client = null;
    [SerializeField] string playerName = "player";
    [SerializeField] int id = 0;

    private void Start()
    {
        SetupClient();
    }
    private void Update()
    {
        
    }
    public NetworkClient GetClient()
    {
        return client;
    }
    public void SetName(string _name)
    {
        playerName = _name;
    }
    public void SetId(int _id)
    {
        id = _id;
    }
    public void SetupClient()
    {
        NetworkClient _client = new NetworkClient();
        client = _client;

        //clients.Add(_client);
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(123, OnTest);
        _client.Connect("127.0.0.1", 4444);
        
        //isAtStartup = false;
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        //Debug.Log("connected");
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerName;
        _msg.id = id;
        client.Send(1234, _msg);
        
        //Debug.Log(client.connection.connectionId);
    }
    private void OnDestroy()
    {
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerName;
        _msg.id = id;
        client.Send(1235, _msg);
        //client.Disconnect();

    }
    void OnTest(NetworkMessage netMsg)
    {
        
    }
}
