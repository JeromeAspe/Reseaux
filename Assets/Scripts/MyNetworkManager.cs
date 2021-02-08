using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
    public bool isAtStartup = true;
    [SerializeField] GameObject player = null;
    Dictionary<int, GameObject> clients= new Dictionary<int, GameObject>();

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
            int _id = Random.Range(0, int.MaxValue);
            _client.SetId(_id);
            clients.Add(_id, _client.gameObject);

        }
        Debug.Log(clients.Count);
        
    }

    void RemoveClient(NetworkMessage netMsg)
    {
        MessageRegisterClient _translate = netMsg.ReadMessage<MessageRegisterClient>();
        Debug.LogError(_translate.id);
        clients.Remove(_translate.id);
    }
    // Create a server and listen on a port
    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(1234, OnReceiveName);
        NetworkServer.RegisterHandler(1235, RemoveClient);
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
        //clients.Add(_translate.id, _translate.client);
        NetworkServer.SendToClient(1, 123, new MessageRegisterClient());

    }
}
