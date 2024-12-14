using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    [SerializeField] public ScriptableValue levelSO, speedSO;

    public int TurnNumber;
    public int DamageDone;
    public int DamageTaken;

    public List<BaseUnit> enemyUnits;
    public List<BaseUnit> playerUnits;

    public bool Attacking;
    public bool Special1;
    public bool Special2;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TurnNumber = 0;
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPlayers:
                UnitManager.Instance.SpawnPlayers();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PlayerTurn:
                StartPlayerTurn();
                break;
            case GameState.EnemyTurn:
                StartEnemyTurn();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    // always done at start of player turn
    public void StartPlayerTurn()
    {
        TurnNumber++;
        enemyUnits = new List<BaseUnit>();
        playerUnits = new List<BaseUnit>();
        foreach (Transform child in UnitManager.Instance.unitContainer.transform)
        {
            if (child.GetComponent<BaseUnit>().Faction == Faction.Enemy)
            {
                enemyUnits.Add(child.GetComponent<BaseUnit>());
            }
            if (child.GetComponent<BaseUnit>().Faction == Faction.Player)
            {
                playerUnits.Add(child.GetComponent<BaseUnit>());
            }
        }
        // check if any team has no units and end game
        Debug.Log(enemyUnits.Count);
        if (enemyUnits.Count == 0)
        {
            Debug.Log("Win");
            MenuManager.Instance.WinGame();
        }
        Debug.Log(playerUnits.Count);
        if (playerUnits.Count == 0)
        {
            Debug.Log("Lose");
            MenuManager.Instance.LoseGame();
        }

        MenuManager.Instance._turnPanel.GetComponentInChildren<TMP_Text>().text = "Turn " + TurnNumber.ToString();
        MenuManager.Instance.EnableEndTurn();
        // get all alive units
        BaseUnit[] units = UnitManager.Instance.unitContainer.transform.GetComponentsInChildren<BaseUnit>();

        // reset units actions and remove buffs and reduce cooldowns
        foreach (BaseUnit unit in units )
        {
            unit.Actions = unit.MaxActions;
            if(unit.Faction == Faction.Enemy)
            {
                unit.armDebuff = false;
                unit.legDebuff = false;
            }
            if(unit.Faction == Faction.Player)
            {
                BasePlayer playerUnit = unit as BasePlayer;
                playerUnit.buffed = false;
                if (playerUnit.specialCooldown1 > 0) playerUnit.specialCooldown1--;
                if (playerUnit.specialCooldown2 > 0) playerUnit.specialCooldown2--;
                if (unit.UnitName == "Knight")
                {
                    Player1 knight = unit as Player1;
                    knight.tanking = false;
                }
            }
        }
    }

    // always done at end of player turn
    public void EndPlayerTurn()
    {
        MouseController.Instance.DeselectUnit();
        MenuManager.Instance.DisableEndTurn();
        ChangeState(GameState.EnemyTurn);
    }

    // always done at start of enemy turn
    public void StartEnemyTurn()
    {
        BaseUnit[] units = UnitManager.Instance.unitContainer.transform.GetComponentsInChildren<BaseUnit>();
        // clear debuffs
        foreach (BaseUnit unit in units)
        {
            if (unit.Faction == Faction.Player)
            {
                unit.armDebuff = false;
                unit.legDebuff = false;
            }
        }
        StartCoroutine (AIManager.Instance.EnemyTurns());
        
    }
    // always done at end of enemy turn
    public void EndEnemyTurn()
    {
        ChangeState(GameState.PlayerTurn);
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnPlayers = 1,
    SpawnEnemies = 2,
    PlayerTurn = 3,
    EnemyTurn = 4
}
