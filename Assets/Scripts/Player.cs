using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Player
{
    [SerializeField] string name;
    [SerializeField] Color color;
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
    public Color GetColor()
    {
        return color;
    }
    public void SetColor(Color _color)
    {
        color = _color;
    }
    public Player(string _name,Vector3 _position,Color _color)
    {
        name = _name;
        position = _position;
        color = _color;
    }
}
