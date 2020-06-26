using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2 position;
    public Vector2 parent;

    public Node(Vector2 _pos, Vector2 _parent)
    {
        position = _pos;
        parent = _parent;
    }

}
