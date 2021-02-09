using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Player
{
    [SerializeField] string name;
    public Vector3 position;

    public string GetName()
    {
        return name;
    }
    public void SetName(string _name)
    {
        name = _name;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public void SetPosition(Vector3 _position)
    {
        position = _position;
    }

    public Player(string _name,Vector3 _position)
    {
        name = _name;
        position = _position;
    }
}
