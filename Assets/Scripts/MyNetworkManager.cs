using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManager : MonoBehaviour
{
    public bool isAtStartup = true;
    [SerializeField] Text connectedCount = null;
    [SerializeField] GameObject player = null;
    Dictionary<int, GameObject> clients= new Dictionary<int, GameObject>();

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
            Instantiate<GameObject>(player);
            Client _client = player.GetComponent<Client>();
            _client.SetName("Jacky");
            int _id = Random.Range(0, 1000);
            _client.SetId(_id);
            clients.Add(_id, _client.gameObject);
            _client.SetupClient();

        }

    }
    void UpdateUI()
    {
        if (connectedCount)
            connectedCount.text = $"Nombre de clients : {clients.Count}";

        foreach(KeyValuePair<int,GameObject> _player in clients)
        {
            if (_player.Value.GetComponent<Client>().GetClient()==null)
            { 
               clients.Remove(_player.Key);
            }
            
        }
    }
    void RemoveClient(NetworkMessage netMsg)
    {
        MessageRegisterClient _translate = netMsg.ReadMessage<MessageRegisterClient>();
        Debug.LogError(_translate.id);
        Debug.Log(clients[_translate.id].GetComponent<Client>().GetClient());
        //NetworkClient.allClients.Remove(clients[_translate.id].GetComponent<Client>().GetClient());
        //clients.Remove(_translate.id);
        

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
