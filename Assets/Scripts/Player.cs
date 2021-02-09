using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Player
{
    [SerializeField] string name;
    [SerializeField] Vector3 position;

    public string GetName()
    {
        return name;
    }
    public Vector3 GetPosition()
    {
        return position;
    }

    public Player(string _name,Vector3 _position)
    {
        name = _name;
        position = _position;
    }
}
