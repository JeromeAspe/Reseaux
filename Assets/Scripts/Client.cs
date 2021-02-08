using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    NetworkClient client = null;
    [SerializeField] string playerName = "player";
    int id = 0;

    private void Start()
    {
        
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
    public int GetId()
    {
        return id;

    }
    public void SetId(int _id)
    {
        id = _id;
        Debug.Log(id);

    }
    public void SetupClient()
    {
        
        NetworkClient _client = new NetworkClient();
        client = _client;
        Debug.Log(id);
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(123, OnTest);
        _client.Connect("127.0.0.1", 4444);
        
    }
    public void OnConnected(NetworkMessage netMsg)
    {

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
        //client.Send(1235, _msg);
        NetworkClient.allClients.Remove(GetClient());

    }
    void OnTest(NetworkMessage netMsg)
    {
        
    }
}
