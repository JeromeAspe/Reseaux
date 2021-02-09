using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerName = null;
    [SerializeField] MyNetworkManager server = null;
    [SerializeField] Button validate = null;

    public bool IsValid => playerName && server;

    private void Start()
    {
        validate.onClick.AddListener(ValidateSelection);
    }

    void ValidateSelection()
    {
        server.InitClient(playerName.text);
        gameObject.SetActive(false);
    }

}
