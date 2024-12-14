using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public static BaseUnit Attacker;
    public static BaseUnit Defender;
    public bool enemyMoveAttack = false;


    void Awake()
    {
        Instance = this;
    }


    public static void ShowTargeting(BaseUnit AttackUnit, BaseUnit DefendUnit )
    {
        Attacker = AttackUnit;
        Defender = DefendUnit;

        // todo range calculation
        int range = 0;
        int rangePenalty = 0;
        BasePlayer playerUnit = Attacker as BasePlayer;
        if (AttackUnit.AttackRange > 1 || GameManager.Instance.Special2 == true && playerUnit.special2 == "Throwingknife")
        {
            range = GridManager.Instance.GetTilesPathShoot(AttackUnit, DefendUnit);
            Debug.Log(range);
            rangePenalty = range * 2;
        }

        MenuManager.Instance.ShowTargetPanel(Attacker, Defender, rangePenalty);
        MenuManager.Instance.ShowTargetedUnit(Defender);
    }

    // AI attacking player function
    public static void AttackPlayer(BaseUnit AttackUnit, BaseUnit DefendUnit)
    {
        // todo range calculation

        Attacker = AttackUnit;
        Defender = DefendUnit;

        // randomize targeted part
        int TargetPart = 0;
        int rand = Random.Range(0, 9);
        if (rand >= 0 && rand < 4) TargetPart = 0;
        if (rand >= 4 && rand < 6) TargetPart = 2;
        if (rand >= 6 && rand < 8) TargetPart = 3;
        if (rand >= 8 && rand < 9) TargetPart = 1;

        ResolveAttack(TargetPart);
    }

    public static void ResolveAttack(int TargetPart)
    {
        if (Attacker.UnitName == "Rogue")
        {
            if (GameManager.Instance.Attacking == true) Attacker.Actions = Attacker.Actions - 3;
            else if (GameManager.Instance.Special1 == true) Attacker.Actions = Attacker.Actions - 4;
            else if (GameManager.Instance.Special2 == true) Attacker.Actions = Attacker.Actions - 3;
        }
        else if (Attacker.UnitName == "Archer" && GameManager.Instance.Special2)
        {
            Attacker.Actions = Attacker.Actions - 6;
        }
        else if (Attacker.UnitName == "Knight" && GameManager.Instance.Special2)
        {
            // cleave not take additional actions
            Attacker.Actions = Attacker.Actions;
        }
        else if (Attacker.UnitName == "Mage" && GameManager.Instance.Special2) ;
        else Attacker.Actions = Attacker.Actions - 4;

        MenuManager.Instance.HideTargetPanel();
        //
        int TargetPenalty = 0;
        if (TargetPart == 1)
        {
            TargetPenalty = 15;
        }
        if (TargetPart == 2 | TargetPart == 3)
        {
            TargetPenalty = 5;
        }

        int rangePenalty = 0;
        BasePlayer playerUnit = Attacker as BasePlayer;

        // range calculation where each tile of distance adds -2 penalty to hit
        int range = 0;
        if (Attacker.UnitName == "Mage" && GameManager.Instance.Special2) ;
        else if (Attacker.AttackRange > 1 || GameManager.Instance.Special2 == true && playerUnit.special2 == "Throwingknife")
        {
            range = GridManager.Instance.GetTilesPathShoot(Attacker, Defender);
            Debug.Log(range);
            rangePenalty = range * 2;
            if (GameManager.Instance.GameState != GameState.EnemyTurn)
            { 
                if (Attacker.UnitName == "Mage")
                {
                    AnimationManager.Instance.ShootMagic(Attacker, Defender);
                }
                else
                {
                    AnimationManager.Instance.ShootArrow(Attacker, Defender);

                }
            }
        }
        if (Instance.enemyMoveAttack == true && playerUnit.special1 == "Melee") //|| GameManager.Instance.GameState == GameState.EnemyTurn && Attacker.UnitName == "Poacher" && )
        {
            rangePenalty = 20;
            Instance.enemyMoveAttack = false;
        }
        if (GameManager.Instance.Special1 == true && playerUnit.special1 == "Melee") //|| GameManager.Instance.GameState == GameState.EnemyTurn && Attacker.UnitName == "Poacher" && )
        {
            // ranged units bad in melee
            rangePenalty = 20;
        }
        if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Snipe")
        {
            // snipe accuracy bonus
            rangePenalty = rangePenalty - 10;
        }
        if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Throwingknife")
        {
            // Throwingknife accuracy penalty
            rangePenalty = rangePenalty + 3;
        }
        if (GameManager.Instance.Special1 == true && playerUnit.special2 == "Pierce")
        {
            // Pierce accuracy penalty
            rangePenalty = 5;
        }
        if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Fireball")
        {
            // Fireball accuracy bonus
            rangePenalty = rangePenalty - 10;
        }
        if (Attacker.UnitName == "Knight" && GameManager.Instance.Special2)
        {
            rangePenalty = 5;
            // cleave has small accuracy penalty
        }

        int HeightModifier = 0;
        // height calculation
        if (Attacker.OccupiedTile.heightLevel > Defender.OccupiedTile.heightLevel)
        {
            HeightModifier = 3;
            if (Attacker.OccupiedTile.heightLevel > Defender.OccupiedTile.heightLevel + 1) HeightModifier = 6;
        }
        if (Attacker.OccupiedTile.heightLevel < Defender.OccupiedTile.heightLevel)
        {
            HeightModifier = -3;
            if (Attacker.OccupiedTile.heightLevel < Defender.OccupiedTile.heightLevel - 1) HeightModifier = -6;
        }
        int DefSkill = Defender.DefenceSkill;
        if (Defender.legDebuff == true)
        {
            DefSkill = Defender.DefenceSkill - 10;
        }

        Debug.Log(Attacker.AttackSkill - Defender.DefenceSkill - TargetPenalty - rangePenalty + HeightModifier);
        // calculate attacking, todo add height and range distance penalties/bonuses and cover
        if (Attacker.AttackSkill - DefSkill - TargetPenalty - rangePenalty + HeightModifier > Random.Range(0, 100))
        {
            int damage;
            bool armorhit = false;
            if (TargetPart == 1)
            {
                damage = Mathf.RoundToInt(Attacker.AttackPower * 1.5f);
                if (Defender.HeadArmor == 1)
                {
                    if (Attacker.UnitName == "Rogue" && GameManager.Instance.Special1 == true) ;
                    else if (50 < Random.Range(0, 100))
                    {
                        damage = Mathf.RoundToInt(damage * 0.6f);
                        armorhit = true;
                    }
                }
            }
            else if (TargetPart == 2)
            {
                damage = Mathf.RoundToInt(Attacker.AttackPower * 0.8f);
                Defender.armDebuff = true;

                if (Defender.ArmsArmor == 1)
                {
                    if (Attacker.UnitName == "Rogue" && GameManager.Instance.Special1 == true) ;
                    else if (50 < Random.Range(0, 100))
                    {
                        damage = Mathf.RoundToInt(damage * 0.6f);
                        armorhit = true;
                    }
                }
            }
            else if (TargetPart == 3)
            {
                damage = Mathf.RoundToInt(Attacker.AttackPower * 0.8f);
                Defender.legDebuff = true;

                if (Defender.LegsArmor == 1)
                {
                    if (Attacker.UnitName == "Rogue" && GameManager.Instance.Special1 == true) ;
                    else if (50 < Random.Range(0, 100))
                    {
                        damage = Mathf.RoundToInt(damage * 0.6f);
                        armorhit = true;
                    }
                }
            }
            else
            {
                damage = Attacker.AttackPower;
                if (Attacker.UnitName == "Mage" && GameManager.Instance.Special2) ;
                else if (Defender.BodyArmor == 1)
                {
                    if (Attacker.UnitName == "Rogue" && GameManager.Instance.Special1 == true) ;
                    else if (50 < Random.Range(0, 100))
                    {
                        damage = Mathf.RoundToInt(damage * 0.6f);
                        armorhit = true;
                    }
                }
            }

            if (Attacker.Faction == Faction.Player)
            {
                // if unit is buffed other skills except fireball do more damage
                BasePlayer playerAtt = Attacker as BasePlayer;
                if (playerAtt.buffed && playerAtt.special2 != "Fireball")
                    damage = Mathf.RoundToInt(damage * 1.3f);
            }
            if (GameManager.Instance.Special1 == true && playerUnit.special1 == "Melee" && Attacker.UnitName == "Mage")
            {
                damage = Mathf.RoundToInt(damage * 0.5f);
            }
            if (GameManager.Instance.Special2 == true && playerUnit.special1 == "Throwingknife")
            {
                damage = Mathf.RoundToInt(damage * 0.8f);
            }
            if (Defender.UnitName == "Knight")
            {
                Player1 kightDef = Defender as Player1;
                if (kightDef.tanking)
                    damage = Mathf.RoundToInt(damage * 0.75f);
            }

            if (Attacker.armDebuff == true)
            {
                damage = Mathf.RoundToInt(damage * 0.75f);
            }

            if (GameManager.Instance.GameState == GameState.PlayerTurn) GameManager.Instance.DamageDone = GameManager.Instance.DamageDone + damage;
            else GameManager.Instance.DamageTaken = GameManager.Instance.DamageTaken + damage;



            MenuManager.Instance.ShowDamage(damage.ToString(), Defender);

            // play combat sounds
            if (range > 1)
            {
                SoundManager.Instance.PlayArrowSound();
            }

            if (armorhit == true)
            {
                SoundManager.Instance.PlayArmorSound();
            }
            else
            {
                SoundManager.Instance.PlayHitSound();
            }

            Defender.Health = Defender.Health - damage;

        }
        else
        {
            // miss notification
            MenuManager.Instance.ShowDamage("Miss", Defender);
            SoundManager.Instance.PlayMissSound();
        }
        if (GameManager.Instance.GameState == GameState.PlayerTurn)
        { 
            MenuManager.Instance.HideTargetedUnit();
            MenuManager.Instance.ShowSelectedUnit();
        }

        if (Defender.Health <= 0)
        {
            SoundManager.Instance.PlayDeathSound();
            Defender.OccupiedTile.OccupiedUnit = null;
            Defender.OccupiedTile.isBlocked = false;
            if (Defender == UnitManager.Instance.SelectedUnit && GameManager.Instance.GameState == GameState.PlayerTurn)
            {
                UnitManager.Instance.SelectedUnit = null;
                MenuManager.Instance.ShowSelectedUnit();
            }
            if (Defender == AIManager.Instance.EnemyUnit && GameManager.Instance.GameState == GameState.EnemyTurn)
            {
                AIManager.Instance.EnemyUnit = null;
                MenuManager.Instance.HideTargetedUnit();
            }
            foreach (OverlayTile child in Defender.ControlledTiles)
            {
                child.ControlledUnits.Remove(Defender);
            }
            Defender.ControlledTiles = new List<OverlayTile>();
            if(Defender == AIManager.Instance.EnemyUnit && GameManager.Instance.GameState == GameState.EnemyTurn && AIManager.Instance.AIwalking == true)
            {
                AIManager.Instance.AIdead = true;
                AIManager.Instance.ClearWalkingAI(Defender);
            }
            else
            {
                Destroy(Defender.gameObject);
            }
        }
    }

    public static void TargetBody()
    {
        ResolveAttack(0);
    }
    public static void TargetHead()
    {
        ResolveAttack(1);
    }
    public static void TargetArms()
    {
        ResolveAttack(2);
    }
    public static void TargetLegs()
    {
        ResolveAttack(3);
    }
}
