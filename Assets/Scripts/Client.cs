using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    NetworkClient client;
    [SerializeField] Player playerData;
    [SerializeField] int id = 0;
    Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();

    private void Start()
    {
        SetupClient();
    }
    private void Update()
    {
        if(client!=null && client.isConnected)
            SendPosition();
        
    }
    public NetworkClient GetClient()
    {
        return client;
    }
    public void SetPlayer(string _name)
    {
        
        playerData = new Player(_name, transform.position);
        
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
        
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(1237, GetClients);
        _client.Connect("192.168.10.60", 4444);



    }
    public void GetClients(NetworkMessage _msg)
    {
        MessagePositionClient _translate = _msg.ReadMessage<MessagePositionClient>();
        if (_translate.id == id) return;
        if (!players.ContainsKey(_translate.id))
        {
            players.Add(_translate.id, GameObject.CreatePrimitive(PrimitiveType.Sphere));
        }
        players[_translate.id].transform.position = _translate.clientPosition;


    }
    public void SendPosition()
    {
        MessagePositionClient _msg = new MessagePositionClient();
        _msg.clientPosition = transform.position;
        _msg.id = id;
        client.Send(1236, _msg);
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerData.GetName();
        _msg.clientPosition = transform.position;
        _msg.id = id;
        _msg.client = gameObject;
        client.Send(1234, _msg);
    }
    private void OnDestroy()
    {
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerData.GetName();
        _msg.clientPosition = transform.position;
        _msg.id = id;
        GetClient().Send(1235, _msg);
        

    }
    private void OnDrawGizmos()
    {
        foreach (KeyValuePair<int, GameObject> _player in players)
        {
            Gizmos.DrawWireSphere(_player.Value.transform.position, 0.5f);
        }

    }
}
