using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FlamesaberData
{
    public string pillarPrefabName;

    public enum position {Left, Right }
    public position pillarPosition;

    public float delay;
    public float speed;
}