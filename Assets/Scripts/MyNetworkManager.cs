using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManager : MonoBehaviour
{
    public bool isAtStartup = true;
    [SerializeField] Text connectedCount = null;
    [SerializeField] GameObject player = null;
    Dictionary<int, Player> clients= new Dictionary<int, Player>();

    private void Awake()
    {
        SetupServer();

        
    }
    private void Start()
    {
        InvokeRepeating("UpdateUI", 0, 1);
        
        
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject _object =  Instantiate<GameObject>(player);
            Client _client = player.GetComponent<Client>();
            _client.SetPlayer("Jacky");
            NetworkServer.Spawn(_object);

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            NetworkServer.SpawnObjects();
        }
       

    }
    void UpdateUI()
    {
        if (connectedCount)
            connectedCount.text = $"Nombre de clients : {clients.Count}";

        foreach(KeyValuePair<int, Player> _player in clients)
        {
            Debug.Log(NetworkClient.allClients.Count);
            
        }
    }
    void RemoveClient(NetworkMessage netMsg)
    {
        MessageRegisterClient _translate = netMsg.ReadMessage<MessageRegisterClient>();
        Debug.LogError(_translate.id);
        //Debug.Log(clients[_translate.id].GetComponent<Client>().GetClient());
        //NetworkClient.allClients.Remove(clients[_translate.id].GetComponent<Client>().GetClient());
        clients.Remove(_translate.id);
        

    }
    // Create a server and listen on a port
    public void SetupServer()
    {
        Debug.Log(NetworkServer.active);
        if (NetworkServer.active) return;
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(1234, OnReceiveName);
        NetworkServer.RegisterHandler(1235, RemoveClient);
        NetworkServer.RegisterHandler(1236, OnReceivePosition);
        NetworkServer.RegisterHandler(123, RemoveClient);
        isAtStartup = false;
    }

    void SetupClient()
    {

    }
    // client function
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        
    }
    public void OnReceiveName(NetworkMessage _msg)
    {
        MessageRegisterClient _translate = _msg.ReadMessage<MessageRegisterClient>();
        Debug.Log(_translate.clientName);
        Debug.Log(_translate.clientPosition);
        clients.Add(_translate.id, new Player(_translate.clientName, _translate.clientPosition));
        Debug.Log(_translate.id);
        NetworkServer.SendToClient(1, 123, new MessageRegisterClient());

    }
    public void OnReceivePosition(NetworkMessage _msg)
    {
        MessagePositionClient _translate = _msg.ReadMessage<MessagePositionClient>();
        Debug.Log(_translate.clientPosition);
    }
    public void Test()
    {
        Debug.Log("ddd");
    }
}
