using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{

    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseUnit SelectedUnit;
    public GameObject unitContainer;

    public BaseUnit randomPrefab;
    public OverlayTile randomSpawnTile;

    public int playerCount;

    [SerializeField] public BaseUnit _knightUnit;
    [SerializeField] public BaseUnit _archerUnit;
    [SerializeField] public BaseUnit _mageUnit;
    [SerializeField] public BaseUnit _clericUnit;
    [SerializeField] public BaseUnit _rogueUnit;

    public int enemyCount;
    public int BanditCount;
    public int ThugCount;
    public int BossCount;
    public int ArcherCount;
    public int DKnightCount;

    void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }
    // 
    public void SpawnPlayers()
    {

        for (int i = 0; i < playerCount; i++)
        {
            // random spawn units
            //randomPrefab = GetRandomUnit<BaseUnit>(Faction.Player);

            // spawn one of each unit
            randomPrefab = GetUnit<BaseUnit>(Faction.Player, i);
            
            var spawnedUnit = Instantiate(randomPrefab, unitContainer.transform).GetComponent<BaseUnit>();

            // set units
            if (i == 0)
            {

                if (_knightUnit == null)
                {
                    _knightUnit = spawnedUnit;
                }
                else
                    spawnedUnit = _knightUnit;

                if (UnitStats1.Spawned == false)
                {
                    UnitStats1.Spawned = true;

                    UnitStats1.UnitName = spawnedUnit.UnitName;
                    UnitStats1.ImageName = spawnedUnit.ImageName;
                    UnitStats1.Health = spawnedUnit.Health;
                    UnitStats1.MaxHealth = spawnedUnit.MaxHealth;
                    UnitStats1.Actions = spawnedUnit.Actions;
                    UnitStats1.MaxActions = spawnedUnit.MaxActions;

                    UnitStats1.AttackPower = spawnedUnit.AttackPower;
                    UnitStats1.AttackRange = spawnedUnit.AttackRange;
                    UnitStats1.AttackSkill = spawnedUnit.AttackSkill;
                    UnitStats1.DefenceSkill = spawnedUnit.DefenceSkill;

                    UnitStats1.HeadArmor = spawnedUnit.HeadArmor;
                    UnitStats1.BodyArmor = spawnedUnit.BodyArmor;
                    UnitStats1.ArmsArmor = spawnedUnit.ArmsArmor;
                    UnitStats1.LegsArmor = spawnedUnit.LegsArmor;
                }
                else
                {
                    spawnedUnit.UnitName = UnitStats1.UnitName;
                    spawnedUnit.ImageName = UnitStats1.ImageName;
                    spawnedUnit.Health = UnitStats1.Health;
                    spawnedUnit.MaxHealth = UnitStats1.MaxHealth;
                    spawnedUnit.Actions = UnitStats1.Actions;
                    spawnedUnit.MaxActions = UnitStats1.MaxActions;

                    spawnedUnit.AttackPower = UnitStats1.AttackPower;
                    spawnedUnit.AttackRange = UnitStats1.AttackRange;
                    spawnedUnit.AttackSkill = UnitStats1.AttackSkill;
                    spawnedUnit.DefenceSkill = UnitStats1.DefenceSkill;

                    spawnedUnit.HeadArmor = UnitStats1.HeadArmor;
                    spawnedUnit.BodyArmor = UnitStats1.BodyArmor;
                    spawnedUnit.ArmsArmor = UnitStats1.ArmsArmor;
                    spawnedUnit.LegsArmor = UnitStats1.LegsArmor;
                }
            }
            else if (i == 1)
            {
                if (_archerUnit == null)
                {
                    _archerUnit = spawnedUnit;
                }
                else spawnedUnit = _archerUnit;

                if (UnitStats2.Spawned == false)
                {
                    UnitStats2.Spawned = true;

                    UnitStats2.UnitName = spawnedUnit.UnitName;
                    UnitStats2.ImageName = spawnedUnit.ImageName;
                    UnitStats2.Health = spawnedUnit.Health;
                    UnitStats2.MaxHealth = spawnedUnit.MaxHealth;
                    UnitStats2.Actions = spawnedUnit.Actions;
                    UnitStats2.MaxActions = spawnedUnit.MaxActions;

                    UnitStats2.AttackPower = spawnedUnit.AttackPower;
                    UnitStats2.AttackRange = spawnedUnit.AttackRange;
                    UnitStats2.AttackSkill = spawnedUnit.AttackSkill;
                    UnitStats2.DefenceSkill = spawnedUnit.DefenceSkill;

                    UnitStats2.HeadArmor = spawnedUnit.HeadArmor;
                    UnitStats2.BodyArmor = spawnedUnit.BodyArmor;
                    UnitStats2.ArmsArmor = spawnedUnit.ArmsArmor;
                    UnitStats2.LegsArmor = spawnedUnit.LegsArmor;
                }
                else
                {
                    spawnedUnit.UnitName = UnitStats2.UnitName;
                    spawnedUnit.ImageName = UnitStats2.ImageName;
                    spawnedUnit.Health = UnitStats2.Health;
                    spawnedUnit.MaxHealth = UnitStats2.MaxHealth;
                    spawnedUnit.Actions = UnitStats2.Actions;
                    spawnedUnit.MaxActions = UnitStats2.MaxActions;

                    spawnedUnit.AttackPower = UnitStats2.AttackPower;
                    spawnedUnit.AttackRange = UnitStats2.AttackRange;
                    spawnedUnit.AttackSkill = UnitStats2.AttackSkill;
                    spawnedUnit.DefenceSkill = UnitStats2.DefenceSkill;

                    spawnedUnit.HeadArmor = UnitStats2.HeadArmor;
                    spawnedUnit.BodyArmor = UnitStats2.BodyArmor;
                    spawnedUnit.ArmsArmor = UnitStats2.ArmsArmor;
                    spawnedUnit.LegsArmor = UnitStats2.LegsArmor;
                }
            }
            else if (i == 2)
            {
                if (_mageUnit == null)
                {
                    _mageUnit = spawnedUnit;
                }
                else spawnedUnit = _mageUnit;

                if (UnitStats3.Spawned == false)
                {
                    UnitStats3.Spawned = true;

                    UnitStats3.UnitName = spawnedUnit.UnitName;
                    UnitStats3.ImageName = spawnedUnit.ImageName;
                    UnitStats3.Health = spawnedUnit.Health;
                    UnitStats3.MaxHealth = spawnedUnit.MaxHealth;
                    UnitStats3.Actions = spawnedUnit.Actions;
                    UnitStats3.MaxActions = spawnedUnit.MaxActions;

                    UnitStats3.AttackPower = spawnedUnit.AttackPower;
                    UnitStats3.AttackRange = spawnedUnit.AttackRange;
                    UnitStats3.AttackSkill = spawnedUnit.AttackSkill;
                    UnitStats3.DefenceSkill = spawnedUnit.DefenceSkill;

                    UnitStats3.HeadArmor = spawnedUnit.HeadArmor;
                    UnitStats3.BodyArmor = spawnedUnit.BodyArmor;
                    UnitStats3.ArmsArmor = spawnedUnit.ArmsArmor;
                    UnitStats3.LegsArmor = spawnedUnit.LegsArmor;
                }
                else
                {
                    spawnedUnit.UnitName = UnitStats3.UnitName;
                    spawnedUnit.ImageName = UnitStats3.ImageName;
                    spawnedUnit.Health = UnitStats3.Health;
                    spawnedUnit.MaxHealth = UnitStats3.MaxHealth;
                    spawnedUnit.Actions = UnitStats3.Actions;
                    spawnedUnit.MaxActions = UnitStats3.MaxActions;

                    spawnedUnit.AttackPower = UnitStats3.AttackPower;
                    spawnedUnit.AttackRange = UnitStats3.AttackRange;
                    spawnedUnit.AttackSkill = UnitStats3.AttackSkill;
                    spawnedUnit.DefenceSkill = UnitStats3.DefenceSkill;

                    spawnedUnit.HeadArmor = UnitStats3.HeadArmor;
                    spawnedUnit.BodyArmor = UnitStats3.BodyArmor;
                    spawnedUnit.ArmsArmor = UnitStats3.ArmsArmor;
                    spawnedUnit.LegsArmor = UnitStats3.LegsArmor;
                }
            }
            else if (i == 3)
            {
                if (_clericUnit == null)
                { 
                    _clericUnit = spawnedUnit;
                }
                else spawnedUnit = _clericUnit;

                if (UnitStats4.Spawned == false)
                {
                    UnitStats4.Spawned = true;

                    UnitStats4.UnitName = spawnedUnit.UnitName;
                    UnitStats4.ImageName = spawnedUnit.ImageName;
                    UnitStats4.Health = spawnedUnit.Health;
                    UnitStats4.MaxHealth = spawnedUnit.MaxHealth;
                    UnitStats4.Actions = spawnedUnit.Actions;
                    UnitStats4.MaxActions = spawnedUnit.MaxActions;

                    UnitStats4.AttackPower = spawnedUnit.AttackPower;
                    UnitStats4.AttackRange = spawnedUnit.AttackRange;
                    UnitStats4.AttackSkill = spawnedUnit.AttackSkill;
                    UnitStats4.DefenceSkill = spawnedUnit.DefenceSkill;

                    UnitStats4.HeadArmor = spawnedUnit.HeadArmor;
                    UnitStats4.BodyArmor = spawnedUnit.BodyArmor;
                    UnitStats4.ArmsArmor = spawnedUnit.ArmsArmor;
                    UnitStats4.LegsArmor = spawnedUnit.LegsArmor;
                }
                else
                {
                    spawnedUnit.UnitName = UnitStats4.UnitName;
                    spawnedUnit.ImageName = UnitStats4.ImageName;
                    spawnedUnit.Health = UnitStats4.Health;
                    spawnedUnit.MaxHealth = UnitStats4.MaxHealth;
                    spawnedUnit.Actions = UnitStats4.Actions;
                    spawnedUnit.MaxActions = UnitStats4.MaxActions;
                    
                    spawnedUnit.AttackPower = UnitStats4.AttackPower;
                    spawnedUnit.AttackRange = UnitStats4.AttackRange;
                    spawnedUnit.AttackSkill = UnitStats4.AttackSkill;
                    spawnedUnit.DefenceSkill = UnitStats4.DefenceSkill;
                    
                    spawnedUnit.HeadArmor = UnitStats4.HeadArmor;
                    spawnedUnit.BodyArmor = UnitStats4.BodyArmor;
                    spawnedUnit.ArmsArmor = UnitStats4.ArmsArmor;
                    spawnedUnit.LegsArmor = UnitStats4.LegsArmor;
                }
            }
            else if (i == 4)
            {
                if (_rogueUnit == null)
                { 
                    _rogueUnit = spawnedUnit;
                }
                else spawnedUnit = _rogueUnit;

                if (UnitStats5.Spawned == false)
                {
                    UnitStats5.Spawned = true;

                    UnitStats5.UnitName = spawnedUnit.UnitName;
                    UnitStats5.ImageName = spawnedUnit.ImageName;
                    UnitStats5.Health = spawnedUnit.Health;
                    UnitStats5.MaxHealth = spawnedUnit.MaxHealth;
                    UnitStats5.Actions = spawnedUnit.Actions;
                    UnitStats5.MaxActions = spawnedUnit.MaxActions;

                    UnitStats5.AttackPower = spawnedUnit.AttackPower;
                    UnitStats5.AttackRange = spawnedUnit.AttackRange;
                    UnitStats5.AttackSkill = spawnedUnit.AttackSkill;
                    UnitStats5.DefenceSkill = spawnedUnit.DefenceSkill;

                    UnitStats5.HeadArmor = spawnedUnit.HeadArmor;
                    UnitStats5.BodyArmor = spawnedUnit.BodyArmor;
                    UnitStats5.ArmsArmor = spawnedUnit.ArmsArmor;
                    UnitStats5.LegsArmor = spawnedUnit.LegsArmor;
                }
                else
                {
                    spawnedUnit.UnitName = UnitStats5.UnitName;
                    spawnedUnit.ImageName = UnitStats5.ImageName;
                    spawnedUnit.Health = UnitStats5.Health;
                    spawnedUnit.MaxHealth = UnitStats5.MaxHealth;
                    spawnedUnit.Actions = UnitStats5.Actions;
                    spawnedUnit.MaxActions = UnitStats5.MaxActions;
                    
                    spawnedUnit.AttackPower = UnitStats5.AttackPower;
                    spawnedUnit.AttackRange = UnitStats5.AttackRange;
                    spawnedUnit.AttackSkill = UnitStats5.AttackSkill;
                    spawnedUnit.DefenceSkill = UnitStats5.DefenceSkill;
                    
                    spawnedUnit.HeadArmor = UnitStats5.HeadArmor;
                    spawnedUnit.BodyArmor = UnitStats5.BodyArmor;
                    spawnedUnit.ArmsArmor = UnitStats5.ArmsArmor;
                    spawnedUnit.LegsArmor = UnitStats5.LegsArmor;
                }

            }

            int spawnX = Random.Range(GridManager.Instance.bounds.min.x + 3, GridManager.Instance.bounds.min.x + 5);
            int spawnY = Random.Range(GridManager.Instance.bounds.min.y + 8, GridManager.Instance.bounds.max.y - 8);

            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                spawnX = Random.Range(-2, 3);
                spawnY = Random.Range(-2, 0);

            }

            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                spawnX = Random.Range(GridManager.Instance.bounds.min.x + 2, GridManager.Instance.bounds.min.x + 3);
                spawnY = Random.Range(GridManager.Instance.bounds.min.y + 8, GridManager.Instance.bounds.max.y - 8);

            }

            randomSpawnTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);
            // check tile is not occupied
            if (randomSpawnTile.OccupiedUnit != null)
            {
                while (randomSpawnTile.OccupiedUnit != null)
                {
                    spawnX = Random.Range(GridManager.Instance.bounds.min.x + 2, GridManager.Instance.bounds.min.x + 5);
                    spawnY = Random.Range(GridManager.Instance.bounds.min.y + 8, GridManager.Instance.bounds.max.y - 8);
                    if (SceneManager.GetActiveScene().buildIndex == 4)
                    {
                        spawnX = Random.Range(-2, 3);
                        spawnY = Random.Range(0, 1);

                    }
                    randomSpawnTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);
                }
            }

            // set unit stats from saved

            PositionCharacterOnTile(spawnedUnit, randomSpawnTile);
            randomSpawnTile.OccupiedUnit = spawnedUnit;
            randomSpawnTile.isBlocked = true;
            spawnedUnit.OccupiedTile = randomSpawnTile;
            spawnedUnit.ControlledTiles = new List<OverlayTile>();
            spawnedUnit.ControlledTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(spawnedUnit.OccupiedTile, new List<OverlayTile>(), spawnedUnit));
            foreach (OverlayTile child in spawnedUnit.ControlledTiles)
            {
                child.ControlledUnits.Add(spawnedUnit);
            }

        }
        randomPrefab = null;
        randomSpawnTile = null;
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {

        for (int i = 0; i < enemyCount; i++)
        {
            int spawnedUnits = 0;

            // spawn deffirent enemies depending on level
            if (DKnightCount > 0)
            {
                randomPrefab = GetUnit<BaseUnit>(Faction.Enemy, 4);
                DKnightCount--;
                spawnedUnits++;
            }
            else if (BossCount > 0)
            {
                randomPrefab = GetUnit<BaseUnit>(Faction.Enemy, 2);
                BossCount--;
                spawnedUnits++;
            }
            else if (ArcherCount > 0)
            {
                randomPrefab = GetUnit<BaseUnit>(Faction.Enemy, 3);
                ArcherCount--;
                spawnedUnits++;
            }
            else if (ThugCount > 0)
            {
                randomPrefab = GetUnit<BaseUnit>(Faction.Enemy, 1);
                ThugCount--;
                spawnedUnits++;
            }
            else if (BanditCount > 0)
            {
                randomPrefab = GetUnit<BaseUnit>(Faction.Enemy, 0);
                BanditCount--;
                spawnedUnits++;
            }
            else if (spawnedUnits != enemyCount)
            {
                randomPrefab = GetRandomUnit<BaseUnit>(Faction.Enemy);
                spawnedUnits++;
            }

            var spawnedUnit = Instantiate(randomPrefab, unitContainer.transform).GetComponent<BaseUnit>();

            int spawnX = Random.Range(GridManager.Instance.bounds.max.x - 6, GridManager.Instance.bounds.max.x - 2);
            int spawnY = Random.Range(GridManager.Instance.bounds.min.y + 5, GridManager.Instance.bounds.max.y - 4);

            if (SceneManager.GetActiveScene().buildIndex == 4 )
            {
                spawnX = Random.Range(GridManager.Instance.bounds.min.x + 5, GridManager.Instance.bounds.max.x - 5);
                if (i < 3 || i > 6 )
                {
                    spawnY = Random.Range(GridManager.Instance.bounds.min.y + 3, -5);

                }
                else
                {
                    spawnY = Random.Range(4, GridManager.Instance.bounds.max.y - 2);
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                if (i == 2)
                {
                    spawnX = Random.Range(2, 4);
                    spawnY = Random.Range(3, 5);
                }
                else if (i == 3 || i == 8)
                {
                    spawnX = Random.Range(2, 4);
                    spawnY = Random.Range(-3, -5);
                }
                else if(i == 4)
                {
                    spawnX = Random.Range(7, 9);
                    spawnY = Random.Range(-3, 3);
                }
                else if(i > 4 && i < 7)
                {
                    spawnX = Random.Range(2, 6);
                    spawnY = Random.Range(-2, 3);
                }
                else
                {
                    spawnX = Random.Range(GridManager.Instance.bounds.max.x - 6, GridManager.Instance.bounds.max.x - 2);
                    spawnY = Random.Range(GridManager.Instance.bounds.min.y + 7, GridManager.Instance.bounds.max.y - 6);
                }
            }
            else
            {
                spawnX = Random.Range(GridManager.Instance.bounds.max.x - 6, GridManager.Instance.bounds.max.x - 2);
                spawnY = Random.Range(GridManager.Instance.bounds.min.y + 7, GridManager.Instance.bounds.max.y - 6);
            }

            randomSpawnTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);
            // check tile is not occupied
            if (randomSpawnTile.OccupiedUnit != null)
            {
                while (randomSpawnTile.OccupiedUnit != null)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 4 )
                    {
                        spawnX = Random.Range(GridManager.Instance.bounds.min.y + 6, GridManager.Instance.bounds.max.x - 4);
                        if (i < 3 || i > 6)
                        {
                            spawnY = Random.Range(GridManager.Instance.bounds.min.y + 2, -5);

                        }
                        else
                        {
                            spawnY = Random.Range(4, GridManager.Instance.bounds.max.y - 2);
                        }
                    }
                    else
                    {
                        spawnX = Random.Range(GridManager.Instance.bounds.max.x - 6, GridManager.Instance.bounds.max.x - 2);
                        spawnY = Random.Range(GridManager.Instance.bounds.min.y + 5, GridManager.Instance.bounds.max.y - 5);
                    }

                    randomSpawnTile = GridManager.Instance.GetUnitSpawnTile(spawnX, spawnY);
                }
            }
            PositionCharacterOnTile(spawnedUnit, randomSpawnTile);
            randomSpawnTile.OccupiedUnit = spawnedUnit;
            randomSpawnTile.isBlocked = true;
            spawnedUnit.OccupiedTile = randomSpawnTile;
            spawnedUnit.ControlledTiles = new List<OverlayTile>();
            spawnedUnit.ControlledTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(spawnedUnit.OccupiedTile, new List<OverlayTile>(), spawnedUnit));
            foreach (OverlayTile child in spawnedUnit.ControlledTiles)
            {
                child.ControlledUnits.Add(spawnedUnit);
            }

        }
        randomPrefab = null;
        randomSpawnTile = null;
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }

    // returns a random unit of faction
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    // returns unit i of faction
    private T GetUnit<T>(Faction faction, int i) where T : BaseUnit
    {
        List<ScriptableUnit> units = _units.Where(u => u.Faction == faction).ToList();
        return (T)units[i].UnitPrefab;
    }

    // places unit on tile
    public void PositionCharacterOnTile(BaseUnit unit, OverlayTile tile)
    {
        unit.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.4001f, tile.transform.position.z);
        unit.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        unit.OccupiedTile = tile;
    }

    public void SetSelectedUnit(BaseUnit unit)
    {
        SelectedUnit = unit;
        MenuManager.Instance.ShowSelectedUnit();
    }
    // select all units from side panel
    public void SelectKnight()
    {
        if (MouseController.Instance.path.Count == 0)
        {
            GameManager.Instance.Attacking = false;
            GameManager.Instance.Special1 = false;
            GameManager.Instance.Special2 = false;
            SelectedUnit = _knightUnit;
            MenuManager.Instance.ShowSelectedUnit();
            MouseController.Instance.GetInRangeTiles();
        }
    }
    public void SelectArcher()
    {
        if (MouseController.Instance.path.Count == 0)
        {
            GameManager.Instance.Attacking = false;
            GameManager.Instance.Special1 = false;
            GameManager.Instance.Special2 = false;
            SelectedUnit = _archerUnit;
            MenuManager.Instance.ShowSelectedUnit();
            MouseController.Instance.GetInRangeTiles();
        }
    }
    public void SelectMage()
    {
        if (MouseController.Instance.path.Count == 0)
        {
            GameManager.Instance.Attacking = false;
            GameManager.Instance.Special1 = false;
            GameManager.Instance.Special2 = false;
            SelectedUnit = _mageUnit;
            MenuManager.Instance.ShowSelectedUnit();
            MouseController.Instance.GetInRangeTiles();
        }
    }
    public void SelectCleric()
    {
        if (MouseController.Instance.path.Count == 0)
        {
            GameManager.Instance.Attacking = false;
            GameManager.Instance.Special1 = false;
            GameManager.Instance.Special2 = false;
            SelectedUnit = _clericUnit;
            MenuManager.Instance.ShowSelectedUnit();
            MouseController.Instance.GetInRangeTiles();
        }
    }
    public void SelectRogue()
    {
        if (MouseController.Instance.path.Count == 0)
        {
            GameManager.Instance.Attacking = false;
            GameManager.Instance.Special1 = false;
            GameManager.Instance.Special2 = false;
            SelectedUnit = _rogueUnit;
            MenuManager.Instance.ShowSelectedUnit();
            MouseController.Instance.GetInRangeTiles();
        }
    }
    
}
