using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    NetworkClient client;
    [SerializeField] Player playerData;
    [SerializeField] int id = 0;
    Dictionary<int, LocalPlayer> players = new Dictionary<int, LocalPlayer>();

    private void Start()
    {
        StartCoroutine("SetupClient");
    }
    private void Update()
    {
        if (client != null && client.isConnected)
            SendPosition();
    }
    public NetworkClient GetClient()
    {
        return client;
    }
    public void SetPlayer(string _name,Color _color)
    {
        playerData = new Player(_name, transform.position,_color);

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

    public IEnumerator SetupClient()
    {
        yield return new WaitForSeconds(0.01f);
        NetworkClient _client = new NetworkClient();
        client = _client;
        int _id = Random.Range(0, int.MaxValue);
        id = _id;
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(1237, GetClients);
        _client.Connect("192.168.10.60", 4444);



    }
    public void ConfirmComponents(NetworkMessage _msg)
    {
        MessageRegisterClient _translate = _msg.ReadMessage<MessageRegisterClient>();
        playerData = new Player(UIManager.LastName, _translate.clientPosition, _translate.clientColor);
    }
    public void GetClients(NetworkMessage _msg)
    {
        MessagePositionClient _translate = _msg.ReadMessage<MessagePositionClient>();
        if (_translate.id == id) return;
        if (!players.ContainsKey(_translate.id))
        {
            GameObject _object = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _object.GetComponent<Renderer>().material.color = _translate.clientColor;
            players.Add(_translate.id, new LocalPlayer(_translate.clientName,_object));
            Debug.LogError(players[_translate.id].GetName());
        }
        players[_translate.id].GetBody().transform.position = _translate.clientPosition;


    }
    public void SendPosition()
    {
        MessagePositionClient _msg = new MessagePositionClient();
        _msg.clientPosition = transform.position;
        _msg.id = id;
        _msg.clientColor = playerData.GetColor();
        client.Send(1236, _msg);
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Color _color = GetComponent<Renderer>().material.color;
        playerData.SetColor(_color);
        playerData.SetName(UIManager.LastName);
        //Debug.Log(GetComponent<Renderer>().material.color);
        MessageRegisterClient _msg = new MessageRegisterClient();
        _msg.clientName = playerData.GetName();
        _msg.clientPosition = transform.position;
        _msg.id = id;
        _msg.clientColor = _color;
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
        foreach (KeyValuePair<int, LocalPlayer> _player in players)
        {
            Gizmos.DrawWireSphere(_player.Value.GetBody().transform.position, 0.5f);
        }

    }
}
