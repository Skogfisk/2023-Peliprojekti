using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class ScriptableList : ScriptableObject
{
    [SerializeField]
    public List<BaseUnit> _units;

    public List<BaseUnit> Value
    {
        get { return _units; }
        set { _units = value; }
    }

}
