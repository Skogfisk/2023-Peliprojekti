using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class AIManager : MonoBehaviour
{
    public static AIManager Instance;
    public List<BaseUnit> enemyUnits;
    public List<BaseUnit> playerUnits;
    public BaseEnemy EnemyUnit;

    private PathFinder pathFinder;
    public List<OverlayTile> path;
    private RangeFinder rangeFinder;
    public List<OverlayTile> inRangeTiles;

    private bool controlledAttack = false;
    public bool AIwalking = false;
    public bool AIdead = false;

    private float delay = 0.5f;


    void Awake()
    {
        Instance = this;
        pathFinder = new PathFinder();
        path = new List<OverlayTile>();
        rangeFinder = new RangeFinder();
        inRangeTiles = new List<OverlayTile>();
        delay = delay * GameManager.Instance.speedSO._value;
        Debug.Log(delay);
    }


    public IEnumerator EnemyTurns()
    {
        // list all active units as either enemies or players
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
        foreach (BaseEnemy enemy in enemyUnits)
        {
            EnemyUnit = enemy;
            EvaluatePriorities();
        }
        // each enemy takes actions
        foreach (BaseEnemy enemy in enemyUnits)
        {
            // if enemy can attack without moving attack
            MenuManager.Instance.ShowTargetedUnit(enemy);
            StartCoroutine (EnemyAttack(enemy));

            yield return new WaitForSeconds(delay);

            // todo att animation

            // move, don't wait if not moving
            if (enemy.Actions > 1)
                StartCoroutine (EnemyMove(enemy));
            // brak if enemy died during turn
            if (AIdead == true)
            {
                MenuManager.Instance.HideTargetedUnit();
                AIdead = false;
                Debug.Log("Enemy died continuing");
                continue;
            }
            else
            {
                if (EnemyUnit.DestinationTile == EnemyUnit.OccupiedTile) yield return new WaitForSeconds(delay);
                else yield return new WaitForSeconds(2f);

                // attack after moving if able
                if (enemy.Actions > 3)
                    StartCoroutine(EnemyAttack(enemy));

                yield return new WaitForSeconds(delay);
                MenuManager.Instance.HideTargetedUnit();
            }
        }
        //yield return new WaitForSeconds(3);
        // todo add delays between ai actions
        Debug.Log("Enemy turn done");
        EnemyUnit = null;
        GameManager.Instance.EndEnemyTurn();
    }
    // evaluate priorities for enemy AI for the turn
    public void EvaluatePriorities()
    {
        //yield return new WaitForSeconds(2);
        // todo add decisionmaking about what to do on turn

        // target near playerunits
        inRangeTiles = rangeFinder.GetTilesInRange(EnemyUnit.OccupiedTile, EnemyUnit.AttackRange);
        if (EnemyUnit.PriorityTarget != null && inRangeTiles.Contains(EnemyUnit.PriorityTarget.OccupiedTile))
        {
            return;
        }
        //if(EnemyUnit.AttackRange < 2 && inRangeTiles.Contains(EnemyUnit.PriorityTarget.OccupiedTile))
        //{
        //    return;
        //}
        inRangeTiles = rangeFinder.GetTilesInRange(EnemyUnit.OccupiedTile, EnemyUnit.Actions);
        if (EnemyUnit.PriorityTarget != null && inRangeTiles.Contains(EnemyUnit.PriorityTarget.OccupiedTile))
        {
            return;
        }

        // todo better targeting


        //else if (EnemyUnit.PriorityTarget != null && inRangeTiles.Contains(OverlayTile.OccupiedUnit == Faction.Player))
        //{
        //    OverlayTile tile = inRangeTiles.Contains(OverlayTile.OccupiedUnit.Faction == Faction.Player);
        //    EnemyUnit.PriorityTarget = tile.OccupiedUnit;
        //}
        //else if (EnemyUnit.PriorityTarget == null && inRangeTiles.Contains(OverlayTile.OccupiedUnit == Faction.Player))
        //{
        //    EnemyUnit.PriorityTarget = OverlayTile.OccupiedUnit;
        //}

        // todo target closest unit

        // on fort level 7 enemy waits for player to attack and defends first turns
        if (SceneManager.GetActiveScene().buildIndex == 7 && GameManager.Instance.TurnNumber < 3)
        {
            if (EnemyUnit.UnitName == "Poacher" || EnemyUnit.UnitName == "Bandit")
            {
                //int spawnX = 0;
                //int spawnY = 0;
                //spawnX = Random.Range(EnemyUnit.OccupiedTile.grid2DLocation.x - 1, EnemyUnit.OccupiedTile.grid2DLocation.x + 2);
                //spawnY = Random.Range(EnemyUnit.OccupiedTile.grid2DLocation.y - 1, EnemyUnit.OccupiedTile.grid2DLocation.y + 2);
                //EnemyUnit.DestinationTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);

                EnemyUnit.DestinationTile = EnemyUnit.OccupiedTile;
            }
            else
            {
                int spawnX = Random.Range(2, 5);
                int spawnY = Random.Range(-2, 3); 
                EnemyUnit.DestinationTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);
            }
        }
        // on camp level 5 enemy waits for player to attack and defends first turn
        else if (SceneManager.GetActiveScene().buildIndex == 5 && GameManager.Instance.TurnNumber < 2)
        {
            
            EnemyUnit.DestinationTile = EnemyUnit.OccupiedTile;
            
        }
        // not working good todo better
        //else if (EnemyUnit.PriorityTarget != null) // randomly change target to another random unit
        //{
        //    int rando = Random.Range(1, 5);
        //    if (rando == 1)
        //    {
        //        EnemyUnit.DestinationTile = null;
        //        EnemyUnit.PriorityTarget = playerUnits[Random.Range(0, playerUnits.Count)];
        //    }
        //}
        else if (EnemyUnit.PriorityTarget == null) // target random unit
        {
            EnemyUnit.DestinationTile = null;
            EnemyUnit.PriorityTarget = playerUnits[Random.Range(0, playerUnits.Count)];
        }

    }

        


    IEnumerator EnemyMove(BaseEnemy enemy)
    {
        EnemyUnit = enemy;
        if (EnemyUnit.Actions < 2) yield break;
        AIwalking = true;
        OverlayTile destinationTile = new OverlayTile();
        inRangeTiles = rangeFinder.GetTilesInRange(EnemyUnit.OccupiedTile, EnemyUnit.Actions / 2);

        if (EnemyUnit.PriorityTarget != null)
        {
            destinationTile = EnemyUnit.PriorityTarget.OccupiedTile;
            List<OverlayTile> alltiles = GridManager.Instance.map.Values.ToList();

            if (alltiles.Contains(destinationTile))
                path = pathFinder.FindPath(EnemyUnit.OccupiedTile, destinationTile, alltiles);

            if (path.Count > 4)
            {
                for (int i = 4; i < path.Count; i++)
                {
                    path[i].gameObject.GetComponent<OverlayTile>().HideTile();
                }

                path.RemoveRange(4, path.Count - 4);

            }
            if (path.Count >= 1 && path[path.Count - 1].OccupiedUnit != null)
            {
                path[path.Count - 1].gameObject.GetComponent<OverlayTile>().HideTile();
                path.RemoveAt(path.Count - 1);
            }
            // enemy walks shorter distance if taken an action
            if(EnemyUnit.Actions < 8)
            {
                for (int i = 8; i > EnemyUnit.Actions; i = i - 2)
                {
                    path[0].gameObject.GetComponent<OverlayTile>().HideTile();
                    path.RemoveAt(0);
                }
               
            }
        }
        else if (EnemyUnit.DestinationTile != null && SceneManager.GetActiveScene().buildIndex == 7 && GameManager.Instance.TurnNumber < 2)
        {
            destinationTile = EnemyUnit.DestinationTile;
            List<OverlayTile> alltiles = GridManager.Instance.map.Values.ToList();

            if (alltiles.Contains(destinationTile))
                //Debug.Log("Destination found");

                path = pathFinder.FindPath(EnemyUnit.OccupiedTile, destinationTile, alltiles);

            if (path.Count > 4)
            {
                for (int i = 4; i < path.Count; i++)
                {
                    path[i].gameObject.GetComponent<OverlayTile>().HideTile();
                }

                path.RemoveRange(4, path.Count - 4);

            }
            if (path.Count >= 1 && path[path.Count - 1].OccupiedUnit != null)
            {
                path[path.Count - 1].gameObject.GetComponent<OverlayTile>().HideTile();
                path.RemoveAt(path.Count - 1);
            }
            // enemy walks shorter distance if taken an action
            if (EnemyUnit.Actions < 8)
            {
                for (int i = 8; i > EnemyUnit.Actions; i = i - 2)
                {
                    path[0].gameObject.GetComponent<OverlayTile>().HideTile();
                    path.RemoveAt(0);
                }

            }
        }
        else
        {
            destinationTile = inRangeTiles[Random.Range(0, inRangeTiles.Count)];
            path = pathFinder.FindPath(EnemyUnit.OccupiedTile, destinationTile, inRangeTiles);
            // enemy walks shorter distance if taken an action
            if (EnemyUnit.Actions < 8)
            {
                for (int i = 8; i > EnemyUnit.Actions; i = i - 2)
                {
                    path[0].gameObject.GetComponent<OverlayTile>().HideTile();
                    path.RemoveAt(0);
                }

            }

        }
        if (path.Count > 0)
        {
            SoundManager.Instance.PlayWalkingSound();
        }
        if (path.Count == 0)
        {
            SoundManager.Instance.StopWalkingSound();
        }

        while (path.Count > 0)
        {
            MoveAlongPath(EnemyUnit);
            yield return new WaitForSeconds(0.2f);
        }
        
        // todo animate enemy movement
        
    }


    public void MoveAlongPath(BaseUnit character)
    {
        var step = MouseController.Instance.speed * Time.deltaTime * 200;

        float zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        // player units get free attack if moving next to them
        if (EnemyUnit.OccupiedTile.ControlledUnits.Count != 0 && controlledAttack == false)
        {
            foreach (BaseUnit child in EnemyUnit.OccupiedTile.ControlledUnits)
            {
                if (child.Faction == Faction.Player && child.ControlledTiles.Contains(EnemyUnit.OccupiedTile))
                {
                    CombatManager.Instance.enemyMoveAttack = true;
                    CombatManager.AttackPlayer(child, EnemyUnit);
                    if (AIdead == true)
                    {
                        foreach (OverlayTile tile in path)
                        {
                            tile.gameObject.GetComponent<OverlayTile>().HideTile();
                        }
                        path = new List<OverlayTile>();
                        return;
                    }
                }
            }
            controlledAttack = true;
            
        }

        if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.00001f)
        {

            // player units get free attack if moving next to them
            if (path[0].ControlledUnits.Count != 0 && path.Count > 1 && controlledAttack == false)
            {
                foreach (BaseUnit child in path[0].ControlledUnits)
                {
                    if (child.Faction == Faction.Player && child.ControlledTiles.Contains(path[0]))
                    {
                        CombatManager.AttackPlayer(child, character);
                        if (AIdead == true)
                        {
                            foreach (OverlayTile tile in path)
                            {
                                tile.gameObject.GetComponent<OverlayTile>().HideTile();
                            }
                            path = new List<OverlayTile>();
                            return;
                        }
                    }
                }
                controlledAttack = true;
                
            }

            PositionCharacterOnTile(character, path[0]);
            character.Actions = character.Actions - path[0].moveCost;
            path[0].gameObject.GetComponent<OverlayTile>().HideTile();
            path.RemoveAt(0);

            // if enemy is in player controlled tile chance to stop moving to not take attacks
            if (character.OccupiedTile.ControlledUnits.Count != 0)
            {
                // count how many player units control crrent tile
                int playerU = 0;
                foreach (BaseUnit child in character.OccupiedTile.ControlledUnits)
                {
                    if (child.Faction == Faction.Player && child.ControlledTiles.Contains(character.OccupiedTile))
                    {
                        playerU++;
                    }
                }
                // stop moving if many units control tile
                if ( playerU > 0 )
                {
                    int stop = Random.Range(0, 3);
                    if (stop > 1)
                    {
                        foreach (OverlayTile child in path)
                        {
                            child.gameObject.GetComponent<OverlayTile>().HideTile();
                        }
                        path = new List<OverlayTile>();
                    }
                }
                else if( playerU > 1 )
                {
                    int stop = Random.Range(0, 3);
                    if (stop > 0)
                    {
                        foreach (OverlayTile child in path)
                        {
                            child.gameObject.GetComponent<OverlayTile>().HideTile();
                        }
                        path = new List<OverlayTile>();
                    }
                }
            }
            controlledAttack = false;
        }
        if (path.Count == 0)
        {
            AIwalking = false;
            SoundManager.Instance.StopWalkingSound();
            foreach (OverlayTile child in character.ControlledTiles)
            {
                child.ControlledUnits.Remove(character);
            }
            character.ControlledTiles = new List<OverlayTile>();
            character.ControlledTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(character.OccupiedTile, new List<OverlayTile>(), character));
            foreach (OverlayTile child in character.ControlledTiles)
            {
                child.ControlledUnits.Add(character);
            }
        }
    }

    // if enemy unit dies from free player attack while walking clear references from tile and path and destroy unit
    // enemies dying whien moving breaking game is still not fixed
    public void ClearWalkingAI(BaseUnit character)
    {
        SoundManager.Instance.StopWalkingSound();
        foreach (OverlayTile child in character.ControlledTiles)
        {
            child.ControlledUnits.Remove(character);
        }
        character.ControlledTiles = new List<OverlayTile>();
        foreach (OverlayTile tile in path)
        {
            tile.gameObject.GetComponent<OverlayTile>().HideTile();
        }
        path = new List<OverlayTile>();
        character.OccupiedTile.OccupiedUnit = null;
        character.OccupiedTile.isBlocked = false;

        Debug.Log("Enemy died clearing");
        Destroy(character.gameObject);
        AIwalking = false;
    }
    
    // places unit on tile
    public void PositionCharacterOnTile(BaseUnit unit, OverlayTile tile)
    {
        unit.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.3001f, tile.transform.position.z);
        unit.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        unit.OccupiedTile.isBlocked = false;
        unit.OccupiedTile.OccupiedUnit = null;
        tile.OccupiedUnit = unit;
        tile.isBlocked = true;
        unit.OccupiedTile = tile;

    }

    // Enemy attack function
    IEnumerator EnemyAttack(BaseEnemy enemy)
    {
        // break if not enough acctions to attack
        
        EnemyUnit = enemy;
        if (EnemyUnit.Actions < 4) yield break;

        if (EnemyUnit.Actions > 4)
        {
            GameManager.Instance.Attacking = true;

            inRangeTiles = rangeFinder.GetTilesInRange(EnemyUnit.OccupiedTile, EnemyUnit.AttackRange);
            List<BaseUnit> unitsInRange = new List<BaseUnit>();

            for (int i = 0; i < inRangeTiles.Count; i++)
            {
                if (inRangeTiles[i].OccupiedUnit != null && inRangeTiles[i].OccupiedUnit.Faction == Faction.Player)
                {
                    print(EnemyUnit.UnitName);
                    print(inRangeTiles[i].OccupiedUnit.UnitName);
                    unitsInRange.Add(inRangeTiles[i].OccupiedUnit);

                    if (inRangeTiles[i].OccupiedUnit.Faction == Faction.Player && EnemyUnit.Actions >= 4)
                    {
                        print(EnemyUnit.UnitName + " attacked " + inRangeTiles[i].OccupiedUnit.UnitName);
                        //CombatManager.Instance.enemyMoveAttack = true; why is this here?
                        CombatManager.AttackPlayer(EnemyUnit, inRangeTiles[i].OccupiedUnit);
                        yield return new WaitForSeconds(delay * 1.5f);
                    }
                }

            }
            //print(unitsInRange.Count());
            if (unitsInRange.Count() > 0 && EnemyUnit.Actions > 4)
            {
                EnemyAttack(EnemyUnit);
            }

            EnemyUnit = null;
            GameManager.Instance.Attacking = false;
        }
        
    }
}
