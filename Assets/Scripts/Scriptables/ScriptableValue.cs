using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableValue : ScriptableObject
{
    [SerializeField]
    public int _value;

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

}
