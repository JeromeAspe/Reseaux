using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
    public bool isAtStartup = true;
    [SerializeField] GameObject player = null;
    Dictionary<int,Client> clients= new Dictionary<int, Client>();

    private void Awake()
    {
        SetupServer();
        
    }
    private void Start()
    {
        //InvokeRepeating("CheckForClients", 0, 2);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate<GameObject>(player);
            Client _client = player.GetComponent<Client>();
            _client.SetName("Jacky");
            _client.SetId(Random.Range(0, int.MaxValue));

        }
        Debug.Log(clients.Count);
        
    }

    void CheckForClients()
    {
        foreach(KeyValuePair<int, Client> _client in clients)
        {
            //if ((!_client.Value) || !(_client.Value.GetClient().isConnected))
            //{
              //  clients.Remove(_client.Key);
            //}
        }
    }
    // Create a server and listen on a port
    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(1234, OnReceiveName);
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
        //Debug.Log(_translate.client);
        clients.Add(_translate.id, _translate.client);
        NetworkServer.SendToClient(1, 123, new MessageRegisterClient());

    }
}
