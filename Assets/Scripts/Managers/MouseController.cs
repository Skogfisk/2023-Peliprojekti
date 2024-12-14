using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;

    public GameObject cursor;
    public float speed;
    public GameObject characterPrefab;

    private PathFinder pathFinder;
    public List<OverlayTile> path;
    public List<OverlayTile> toolPath;
    private RangeFinder rangeFinder;
    public List<OverlayTile> inRangeTiles;
    private List<OverlayTile> targetedTiles;
    private bool controlledAttack = false;

    void Awake()
    {
        Instance = this;
        pathFinder = new PathFinder();
        path = new List<OverlayTile>();
        toolPath = new List<OverlayTile>();
        rangeFinder = new RangeFinder();
        inRangeTiles = new List<OverlayTile>();
        targetedTiles = new List<OverlayTile>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        RaycastHit2D? hit = GetFocusedOnTile();

        // cursor
        if (hit.HasValue)
        {
            // move cursor
            OverlayTile overlayTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            cursor.transform.position = overlayTile.transform.position;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            // did not get working without bugs, todo fix later
            // show tooltip of movement cost
            //if (UnitManager.Instance.SelectedUnit != null )
            //{
            //    if (inRangeTiles.Contains(overlayTile))
            //    {
            //        if (GameManager.Instance.Attacking == false && GameManager.Instance.Special1 == false && GameManager.Instance.Special2 == false)
            //        {
            //            ShowMoveTooltip(overlayTile);
            //        }
            //    }
            //    else
            //    {
            //        TooltipHandler.Instance.HideTooltip();
            //    }
            //}

            // showing targeting of abilities
            if (GameManager.Instance.Special2 == true && UnitManager.Instance.SelectedUnit != null)
            {
                GetInRangeTiles();

                BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
                if (playerUnit.special2 == "Fireball" && inRangeTiles.Contains(overlayTile))
                {
                    foreach (var item in targetedTiles)
                    {
                        item.HideTile();
                    }
                    targetedTiles = new List<OverlayTile>();
                    targetedTiles.Add(overlayTile);
                    targetedTiles.AddRange(GridManager.Instance.GetNeightbourTilesAll(overlayTile, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                    targetedTiles = targetedTiles.Distinct().ToList();

                    foreach (var item in targetedTiles)
                    {
                        Color newColor = new Color(0.8f, 0.2f, 0.2f, 0.75f);
                        item.ShowTile(newColor);
                    }

                }
                if (playerUnit.special2 == "Cleave" && inRangeTiles.Contains(overlayTile))
                {
                    foreach (var item in targetedTiles)
                    {
                        item.HideTile();
                    }
                    targetedTiles = new List<OverlayTile>();
                    targetedTiles.Add(overlayTile);

                    // top
                    if (overlayTile.gridLocation.x == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.x && overlayTile.gridLocation.y == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.y + 1)
                    {
                        targetedTiles.AddRange(GridManager.Instance.GetCleaveTiles(UnitManager.Instance.SelectedUnit.OccupiedTile, new List<OverlayTile>(), "top"));

                    }
                    // right
                    if (overlayTile.gridLocation.x == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.x + 1 && overlayTile.gridLocation.y == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.y)
                    {
                        targetedTiles.AddRange(GridManager.Instance.GetCleaveTiles(UnitManager.Instance.SelectedUnit.OccupiedTile, new List<OverlayTile>(), "right"));

                    }
                    // left
                    if (overlayTile.gridLocation.x == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.x - 1 && overlayTile.gridLocation.y == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.y)
                    {
                        targetedTiles.AddRange(GridManager.Instance.GetCleaveTiles(UnitManager.Instance.SelectedUnit.OccupiedTile, new List<OverlayTile>(), "left"));

                    }
                    // bottom
                    if (overlayTile.gridLocation.x == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.x && overlayTile.gridLocation.y == UnitManager.Instance.SelectedUnit.OccupiedTile.gridLocation.y - 1)
                    {
                        targetedTiles.AddRange(GridManager.Instance.GetCleaveTiles(UnitManager.Instance.SelectedUnit.OccupiedTile, new List<OverlayTile>(), "bottom"));

                    }

                    targetedTiles = targetedTiles.Distinct().ToList();

                    foreach (var item in targetedTiles)
                    {
                        Color newColor = new Color(0.8f, 0.2f, 0.2f, 0.75f);
                        item.ShowTile(newColor);
                    }

                }
            }
            if (overlayTile.OccupiedUnit != null && GameManager.Instance.GameState == GameState.PlayerTurn && !(MenuManager.Instance._targetingPanel.activeSelf))
            {
                MenuManager.Instance.ShowTargetedUnit(overlayTile.OccupiedUnit);
            }
            if (overlayTile.OccupiedUnit == null && GameManager.Instance.GameState == GameState.PlayerTurn && !(MenuManager.Instance._targetingPanel.activeSelf))
            {
                MenuManager.Instance.HideTargetedUnit();
            }
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(0)) // left click
            {
                if (GameManager.Instance.GameState != GameState.PlayerTurn) return;
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                // mage fireball ability
                if (GameManager.Instance.Special2 == true && path.Count == 0 && UnitManager.Instance.SelectedUnit != null)
                {
                    BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
                    if (UnitManager.Instance.SelectedUnit.Actions >= 6 && playerUnit.special2 == "Fireball" && playerUnit.specialCooldown2 == 0 && inRangeTiles.Contains(overlayTile))
                    {

                        targetedTiles = new List<OverlayTile>();
                        targetedTiles.Add(overlayTile);
                        targetedTiles.AddRange(GridManager.Instance.GetNeightbourTilesAll(overlayTile, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                        targetedTiles = targetedTiles.Distinct().ToList();
                        AnimationManager.Instance.AnimateFireball(overlayTile);
                        foreach (var item in targetedTiles)
                        {
                            if (item.OccupiedUnit != null)
                            {
                                CombatManager.Attacker = UnitManager.Instance.SelectedUnit;
                                CombatManager.Defender = item.OccupiedUnit;
                                CombatManager.ResolveAttack(0);
                            }
                        }
                        playerUnit.Actions = playerUnit.Actions - 6;
                        playerUnit.specialCooldown2 = 3;
                        SoundManager.Instance.PlayFireballSound();
                        GetInRangeTiles();
                        MenuManager.Instance.ShowSelectedUnit();
                    }
                }

                if (overlayTile.OccupiedUnit != null && path.Count == 0)
                {
                    // select a unit
                    if (overlayTile.OccupiedUnit.Faction == Faction.Player && GameManager.Instance.Special1 == false && GameManager.Instance.Special2 == false)//&& UnitManager.Instance.SelectedUnit == null)
                    {

                        DeselectUnit();
                        UnitManager.Instance.SetSelectedUnit(overlayTile.OccupiedUnit);
                        GetInRangeTiles();
                    }
                    // attacking
                    // todo better check for attack range that ignores obstacles but conciders cover
                    else
                    {
                        if (GameManager.Instance.Attacking == true)
                        {
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 || UnitManager.Instance.SelectedUnit.UnitName == "Rogue" && UnitManager.Instance.SelectedUnit.Actions >= 3)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Enemy && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Attacking == true)
                                {
                                    var enemy = (BaseUnit)overlayTile.OccupiedUnit;
                                    CombatManager.ShowTargeting(UnitManager.Instance.SelectedUnit, enemy);
                                    GetInRangeTiles();
                                }
                            }
                        }
                        // special abilities
                        if (GameManager.Instance.Special1 == true || GameManager.Instance.Special2 == true)
                        {
                            // todo good special ability targeting
                            BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 && playerUnit.special1 == "Melee")
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Enemy && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special1 == true)
                                {
                                    CombatManager.ShowTargeting(UnitManager.Instance.SelectedUnit, (BaseUnit)overlayTile.OccupiedUnit);
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 && playerUnit.special1 == "Pierce" && playerUnit.specialCooldown1 == 0)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Enemy && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special1 == true)
                                {
                                    CombatManager.ShowTargeting(UnitManager.Instance.SelectedUnit, (BaseUnit)overlayTile.OccupiedUnit);
                                    playerUnit.specialCooldown1 = 1;
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 && playerUnit.special1 == "Heal" && playerUnit.specialCooldown1 == 0)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Player && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special1 == true)
                                {
                                    overlayTile.OccupiedUnit.Health = overlayTile.OccupiedUnit.Health + UnitManager.Instance.SelectedUnit.AttackPower;
                                    if (overlayTile.OccupiedUnit.Health > overlayTile.OccupiedUnit.MaxHealth) overlayTile.OccupiedUnit.Health = overlayTile.OccupiedUnit.MaxHealth;
                                    MenuManager.Instance.ShowDamage(UnitManager.Instance.SelectedUnit.AttackPower.ToString(), overlayTile.OccupiedUnit);
                                    UnitManager.Instance.SelectedUnit.Actions = UnitManager.Instance.SelectedUnit.Actions - 4;
                                    playerUnit.specialCooldown1 = 1;
                                    SoundManager.Instance.PlayHealSound();
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 && playerUnit.special2 == "Buff" && playerUnit.specialCooldown2 == 0)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Player && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special2 == true)
                                {
                                    BasePlayer targetUnit = overlayTile.OccupiedUnit as BasePlayer;
                                    targetUnit.buffed = true;
                                    MenuManager.Instance.ShowDamage("Buffed", overlayTile.OccupiedUnit);
                                    UnitManager.Instance.SelectedUnit.Actions = UnitManager.Instance.SelectedUnit.Actions - 4;
                                    playerUnit.specialCooldown2 = 2;
                                    SoundManager.Instance.PlayBuffSound();
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 6 && playerUnit.special2 == "Snipe" && playerUnit.specialCooldown2 == 0)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Enemy && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special2 == true)
                                {
                                    CombatManager.ShowTargeting(UnitManager.Instance.SelectedUnit, (BaseUnit)overlayTile.OccupiedUnit);
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 3 && playerUnit.special2 == "Throwingknife" && playerUnit.specialCooldown2 == 0)
                            {
                                if (overlayTile.OccupiedUnit.Faction == Faction.Enemy && UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special2 == true)
                                {
                                    CombatManager.ShowTargeting(UnitManager.Instance.SelectedUnit, (BaseUnit)overlayTile.OccupiedUnit);
                                    GetInRangeTiles();
                                }
                            }
                            if (UnitManager.Instance.SelectedUnit.Actions >= 4 && playerUnit.special2 == "Cleave" && playerUnit.specialCooldown1 == 0)
                            {
                                if (UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Special2 == true)
                                {

                                    foreach (var item in targetedTiles)
                                    {
                                        if (item.OccupiedUnit != null)
                                        {
                                            CombatManager.Attacker = UnitManager.Instance.SelectedUnit;
                                            CombatManager.Defender = item.OccupiedUnit;
                                            CombatManager.ResolveAttack(0);
                                        }
                                    }
                                    playerUnit.Actions = playerUnit.Actions - 4;
                                    playerUnit.specialCooldown2 = 1;
                                    GetInRangeTiles();
                                }
                            }
                        }

                    }
                    //Animate(UnitManager.Instance.SelectedUnit);enemyMoveAttack
                    MenuManager.Instance.ShowSelectedUnit();
                }
                // move unit
                // todo checks when a unit moves to not allow intercating
                else if (UnitManager.Instance.SelectedUnit != null && inRangeTiles.Contains(overlayTile) && GameManager.Instance.Attacking == false && GameManager.Instance.Special1 == false && GameManager.Instance.Special2 == false && path.Count == 0)
                {
                    path = pathFinder.FindPath(UnitManager.Instance.SelectedUnit.OccupiedTile, overlayTile, inRangeTiles);
                    SoundManager.Instance.PlayWalkingSound();
                    // enemies get free attack if moving next to them
                    if (UnitManager.Instance.SelectedUnit.OccupiedTile.ControlledUnits.Count != 0 && controlledAttack == false)
                    {
                        foreach (BaseUnit child in UnitManager.Instance.SelectedUnit.OccupiedTile.ControlledUnits)
                        {
                            if (child.Faction == Faction.Enemy && child.ControlledTiles.Contains(UnitManager.Instance.SelectedUnit.OccupiedTile))
                            {
                                CombatManager.AttackPlayer(child, UnitManager.Instance.SelectedUnit);
                            }
                        }
                        controlledAttack = true;
                    }
                    controlledAttack = false;
                    UnitManager.Instance.SelectedUnit.OccupiedTile.isBlocked = false;
                    UnitManager.Instance.SelectedUnit.OccupiedTile.OccupiedUnit = null;
                    overlayTile.OccupiedUnit = UnitManager.Instance.SelectedUnit;
                    overlayTile.isBlocked = true;
                    MenuManager.Instance.ShowSelectedUnit();

                }
                else
                {

                    overlayTile.gameObject.GetComponent<OverlayTile>().HideTileDelay();
                    MenuManager.Instance.ShowSelectedUnit();
                    GetInRangeTiles();
                }


            }
            if (Input.GetMouseButtonDown(1) && path.Count == 0)
            {
                if (MenuManager.Instance._targetingPanel.active)
                {
                    MenuManager.Instance.HideTargetPanel();
                }
                else DeselectUnit();

            }

            // move unit
            if (path.Count > 0 && GameManager.Instance.GameState == GameState.PlayerTurn)
            {
                
                MenuManager.Instance.DisableEndTurn();
                MoveAlongPath(UnitManager.Instance.SelectedUnit);
                MenuManager.Instance.ShowSelectedUnit();
            }
        }
    }

    
    // character moves along path
    public void MoveAlongPath(BaseUnit character)
    {
        var step = speed * Time.deltaTime;

        float zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.00001f)
        {
            // enemies get free attack if moving next to them
            if (path[0].ControlledUnits.Count != 0 && path.Count > 1 && controlledAttack == false)
            {
                foreach (BaseUnit child in path[0].ControlledUnits)
                {
                    if (child.Faction == Faction.Enemy && child.ControlledTiles.Contains(path[0]))
                    {
                        CombatManager.AttackPlayer(child, character);
                    }
                }
                controlledAttack = true;

            }
            UnitManager.Instance.PositionCharacterOnTile(character, path[0]);
            character.Actions = character.Actions - path[0].moveCost;
            path[0].gameObject.GetComponent<OverlayTile>().HideTile();
            path.RemoveAt(0);
            controlledAttack = false;
        }

        // when path ends stop and set new controlled tiles
        if (path.Count == 0)
        {
            SoundManager.Instance.StopWalkingSound();
            MenuManager.Instance.EnableEndTurn();
            MenuManager.Instance.ShowSelectedUnit();
            GetInRangeTiles();
            foreach (OverlayTile child in UnitManager.Instance.SelectedUnit.ControlledTiles)
            {
                child.ControlledUnits.Remove(UnitManager.Instance.SelectedUnit);
            }
            UnitManager.Instance.SelectedUnit.ControlledTiles = new List<OverlayTile>();
            UnitManager.Instance.SelectedUnit.ControlledTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(UnitManager.Instance.SelectedUnit.OccupiedTile, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
            foreach (OverlayTile child in UnitManager.Instance.SelectedUnit.ControlledTiles)
            {
                child.ControlledUnits.Add(UnitManager.Instance.SelectedUnit);
            }
        }

    }

    public void ShowMoveTooltip(OverlayTile overlayTile)
    {
        toolPath = pathFinder.FindPath(UnitManager.Instance.SelectedUnit.OccupiedTile, overlayTile, inRangeTiles);
        TooltipHandler.Instance.ShowTooltip("Move cost: " + toolPath.Count * 2);
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public void DeselectUnit()
    {
        UnitManager.Instance.SetSelectedUnit(null);
        GameManager.Instance.Attacking = false;
        GameManager.Instance.Special1 = false;
        GameManager.Instance.Special2 = false;
        GetInRangeTiles();
    }

    public void GetInRangeTiles()
    {

        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }

        if (UnitManager.Instance.SelectedUnit == null)
        {
            return;
        }
        if (GameManager.Instance.Special1 == true)
        {
            BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
            if (playerUnit.special1 == "Melee")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, 1);
            if (playerUnit.special1 == "Pierce")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.AttackRange);
            if (playerUnit.special1 == "Heal")
            {
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, 4);
                inRangeTiles.Add(playerUnit.OccupiedTile);
            }

            foreach (var item in inRangeTiles)
            {
                item.ShowTile();
            }
        }
        else if (GameManager.Instance.Special2 == true)
        {
            BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
            if ( playerUnit.special2 == "Buff")
            {
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, 4);
                inRangeTiles.Add(playerUnit.OccupiedTile);
            }
            if (playerUnit.special2 == "Fireball")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.AttackRange);
            if (playerUnit.special2 == "Snipe")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.AttackRange + 2);
            if (playerUnit.special2 == "Cleave")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.AttackRange);
            if (playerUnit.special2 == "Throwingknife")
                inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, 3);

            foreach (var item in inRangeTiles)
            {
                item.ShowTile();
            }
        }
        else if (GameManager.Instance.Attacking == true)
        {
            inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.AttackRange);


            foreach (var item in inRangeTiles)
            {
                item.ShowTile();
            }
        }
        else
        {
            inRangeTiles = rangeFinder.GetTilesInRange(UnitManager.Instance.SelectedUnit.OccupiedTile, UnitManager.Instance.SelectedUnit.Actions);

            foreach (var item in inRangeTiles)
            {
                item.ShowTile();
            }
        }
    }

    
}

