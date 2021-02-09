using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManager : MonoBehaviour
{
    [SerializeField] Text connectedCount = null;
    [SerializeField] GameObject player = null;
    [SerializeField] Camera cameraFollow = null;
    Dictionary<int, Player> clients= new Dictionary<int, Player>();

    private void Awake()
    {
        SetupServer();

        
    }
    private void Start()
    {
        InvokeRepeating("UpdateUI", 0, 1);

        GameObject _object = Instantiate<GameObject>(player);
        Client _client = player.GetComponent<Client>();
        cameraFollow.gameObject.GetComponent<CameraBehaviour>().SetTarget(_object);
        _client.SetPlayer("Jacky");

    }
    void Update()
    {
        UpdateClientPositions();
    }
    void UpdateUI()
    {
        if (connectedCount)
            connectedCount.text = $"Nombre de clients : {clients.Count}";
    }
    void RemoveClient(NetworkMessage netMsg)
    {
        MessageRegisterClient _translate = netMsg.ReadMessage<MessageRegisterClient>();
        clients.Remove(_translate.id);
        

    }
    // Create a server and listen on a port
    public void SetupServer()
    {
        if (NetworkServer.active) return;
        try
        {
            NetworkServer.Listen(4444);
        }
        catch (System.Exception)
        {
            Debug.Log("Server already initialized");
        }
        
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(1234, OnReceiveName);
        NetworkServer.RegisterHandler(1235, RemoveClient);
        NetworkServer.RegisterHandler(1236, OnReceivePosition);
    }

    void UpdateClientPositions()
    {
        foreach(KeyValuePair<int,Player> _player in clients)
        {
            MessagePositionClient _msg = new MessagePositionClient();
            _msg.clientPosition = _player.Value.GetPosition();
            _msg.id = _player.Key;
            NetworkServer.SendToAll(1237, _msg);
        }
        
    }
    // client function
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        
    }
    public void OnReceiveName(NetworkMessage _msg)
    {
        MessageRegisterClient _translate = _msg.ReadMessage<MessageRegisterClient>();
        clients.Add(_translate.id, new Player(_translate.clientName, _translate.clientPosition));

    }
    public void OnReceivePosition(NetworkMessage _msg)
    {
        MessagePositionClient _translate = _msg.ReadMessage<MessagePositionClient>();
        clients[_translate.id] = new Player(clients[_translate.id].GetName(), _translate.clientPosition);
    }
    private void OnDrawGizmos()
    {
        foreach(KeyValuePair<int,Player> _player in clients)
        {
            Gizmos.DrawWireSphere(_player.Value.GetPosition(), 0.5f);
        }
        
    }
}
