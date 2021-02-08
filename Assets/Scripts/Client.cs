using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    NetworkClient client;
    [SerializeField] string playerName = "player";
    [SerializeField] int id = 0;

    private void Start()
    {
        SetupClient();
    }
    private void Update()
    {
        Debug.Log(id);
    }
    public NetworkClient GetClient()
    {
        Debug.Log(client);
        return client;
    }
    public void SetName(string _name)
    {
        playerName = _name;
    }
    public void SetClient(NetworkClient _client)
    {
        client = _client;
    }
    public int GetId()
    {
        return id;

    }
    public void SetId(int _id)
    {
        id = _id;

    }
    public void SetupClient()
    {
        
        NetworkClient _client = new NetworkClient();
        client = _client;
        int _id = Random.Range(0, int.MaxValue);
        id = _id;
        
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
        _msg.client = gameObject;
        client.Send(1234, _msg);
        Debug.Log(client.connection.connectionId);
    }
    private void OnDestroy()
    {
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerName;
        _msg.id = id;
        GetClient().Send(1235, _msg);
        Debug.Log(GetClient().connection.connectionId);
        //NetworkClient.allClients.Remove(GetClient());
        

    }
    void OnTest(NetworkMessage netMsg)
    {
        GetClient();
    }
}
