using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LocalPlayer
{
    [SerializeField] string name;
    [SerializeField] GameObject body;


    public void SetName(string _name)
    {
        name = _name;
    }
    public string GetName()
    {
        return name;
    }
    public GameObject GetBody()
    {
        return body;
    }
    public void SetBody(GameObject _body)
    {
        body = _body;
    }
    public LocalPlayer(string _name, GameObject _body)
    {
        name = _name;
        body = _body;
    }
}
