using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{

    public string UnitName;
    public OverlayTile OccupiedTile;
    public List<OverlayTile> ControlledTiles;
    public Faction Faction;

    public string ImageName;

    public int Health;
    public int MaxHealth;
    public int Actions;
    public int MaxActions;
    
    public int AttackPower;
    public int AttackRange;
    public int AttackSkill;
    public int DefenceSkill;

    public int HeadArmor;
    public int BodyArmor;
    public int ArmsArmor;
    public int LegsArmor;

    public bool armDebuff;
    public bool legDebuff;

}
