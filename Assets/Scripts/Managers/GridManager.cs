using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;

    public static GridManager Instance { get { return _instance; } }

    public GameObject overlayTilePrefab;
    public GameObject overlayContainer;
    public GameObject obstacleContainer;
    public GameObject obstaclePrefab;


    public Dictionary<Vector2Int, OverlayTile> map;
    public bool ignoreBottomTiles;

    public BoundsInt bounds;
    public int obstacleCount;
    private List<Sprite> _obstacles;

    public OverlayTile randomSpawnTile;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            _obstacles = Resources.LoadAll<Sprite>("Obstacles").ToList();
        }

    }
    void LateUpdate()
    {
        foreach (Transform child in overlayContainer.transform)
        {
            if (GameManager.Instance.GameState == GameState.PlayerTurn && child.GetComponent<OverlayTile>().OccupiedUnit != null && child.GetComponent<OverlayTile>().OccupiedUnit.Faction == Faction.Player && child.GetComponent<OverlayTile>().OccupiedUnit.Actions == child.GetComponent<OverlayTile>().OccupiedUnit.MaxActions)
                child.GetComponent<OverlayTile>().GetComponent<SpriteRenderer>().color = new Color(0.6f, 1, 0.6f, 0.75f);
        }
    }

    // Start is called before the first frame update
    public void GenerateGrid()
    {
        var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
        
        map = new Dictionary<Vector2Int, OverlayTile>();
        foreach (var tileMap in tileMaps)
        {

            bounds = tileMap.cellBounds;
            // looping through all tiles
            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        if (z == 0 && ignoreBottomTiles)
                            return;

                        var tileLocation = new Vector3Int(x, y, z);

                        // create overlay tile
                        if (tileMap.HasTile(tileLocation))
                        {
                            var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                            var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                            var tilemapTile = tileMap.GetTile(tileLocation);
                            if (tilemapTile.name == "HighTile") // high height tile
                            {
                                overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.375f, cellWorldPosition.z + 1);
                                overlayTile.GetComponent<OverlayTile>().heightLevel = 3;
                            } else if (tilemapTile.name == "MediumTile") // low height
                            {
                                overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.25f, cellWorldPosition.z + 1);
                                overlayTile.GetComponent<OverlayTile>().heightLevel = 2;
                            } else if (tilemapTile.name == "LowTile") // water tile can't be walked in
                            {
                                overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                overlayTile.GetComponent<OverlayTile>().isBlocked = true;
                                overlayTile.GetComponent<OverlayTile>().heightLevel = -10;
                            }
                            else // flat tile
                            {
                                overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.125f, cellWorldPosition.z + 1);
                                overlayTile.GetComponent<OverlayTile>().heightLevel = 1;
                            }
                            //overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;

                            overlayTile.name = "Overlay_" + x + "_" + y;
                            overlayTile.GetComponent<OverlayTile>().gridLocation = tileLocation;
                            map.Add(new Vector2Int(x, y), overlayTile.GetComponent<OverlayTile>());
                        }

                    }
                }
            }
            // spawn some obstacles on map that can't be walked on, todo add cover mechanics
            for (int i = 0; i < obstacleCount; i++)
            {
                var spawnedObstacle = Instantiate(obstaclePrefab, obstacleContainer.transform);
                int spawnX = 0;
                int spawnY = 0;
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    int side = Random.Range(0, 2);
                    if (side == 1)
                    {
                        spawnX = Random.Range(bounds.min.x + 1, -3);

                    }
                    else
                    {
                        spawnX = Random.Range(3, bounds.max.x - 1);
                    }
                    side = Random.Range(0, 2);
                    if (side == 1)
                    {
                        spawnY = Random.Range(bounds.min.y + 1, -3);

                    }
                    else
                    {
                        spawnY = Random.Range(3, bounds.max.y - 1);
                    }
                }
                else if (SceneManager.GetActiveScene().buildIndex == 4 )
                {
                    spawnX = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
                    int side = Random.Range(0, 2);
                    if(side == 1)
                    {
                        spawnY = Random.Range(bounds.min.y + 1, -3);

                    }
                    else
                    {
                        spawnY = Random.Range(3, bounds.max.y - 1);
                    }
                }
                else {
                    spawnX = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
                    spawnY = Random.Range(bounds.min.y + 1, bounds.max.y - 1);
                }
                if (SceneManager.GetActiveScene().buildIndex == 6)
                {
                    if (spawnX < 5 && spawnX > -5)
                    {
                        if (spawnY < 4 && spawnY > -4)
                        {
                            spawnX = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
                            spawnY = Random.Range(bounds.min.y + 1, bounds.max.y - 1);
                        }
                    }
                }
                if (SceneManager.GetActiveScene().buildIndex == 7)
                {
                    if (spawnX < 5 && spawnX > 2)
                    {
                        spawnX = spawnX - Random.Range(4, 9);
                    }
                }
                // todo add check not out of bounds

                randomSpawnTile = GridManager.Instance.GetObstacleSpawnTile(spawnX, spawnY);
                
                spawnedObstacle.transform.position = new Vector3(randomSpawnTile.transform.position.x, randomSpawnTile.transform.position.y + 0.5501f, randomSpawnTile.transform.position.z);
                spawnedObstacle.GetComponent<SpriteRenderer>().sortingOrder = randomSpawnTile.GetComponent<SpriteRenderer>().sortingOrder;
                // add random sprite
                spawnedObstacle.GetComponent<SpriteRenderer>().sprite = _obstacles[Random.Range(0, _obstacles.Count)];
                randomSpawnTile.isBlocked = true;
                randomSpawnTile.isCover = true;
                randomSpawnTile.heightLevel = -10;

            }
        }
                

        GameManager.Instance.ChangeState(GameState.SpawnPlayers);
    }
    // getting a tile to spawn a unit on
    public OverlayTile GetUnitSpawnTile(int x, int y)
    {

        Vector2Int locationToSpawn = new Vector2Int(x, y);
        OverlayTile tile = new OverlayTile();

        if (map.ContainsKey(locationToSpawn))
        {
            if (map[locationToSpawn].GetComponent<OverlayTile>().isBlocked == true)
            {
                int randX = Random.Range(-1, 2);
                int randY = Random.Range(-1, 2);

                tile = GetUnitSpawnTile(x + randX, y + randY);
            }
            else
            {
                tile = map[locationToSpawn].GetComponent<OverlayTile>();
            }
        }
        return tile;
    }

    // getting a tile to spawn obstacle on
    public OverlayTile GetObstacleSpawnTile(int x, int y)
    {

        Vector2Int locationToSpawn = new Vector2Int(x, y);
        OverlayTile tile = new OverlayTile();

        if (map.ContainsKey(locationToSpawn))
        {
            if (map[locationToSpawn].GetComponent<OverlayTile>().isBlocked == true)
            {
                int randX = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
                int randY = Random.Range(bounds.min.y + 1, bounds.max.y - 1);

                tile = GetObstacleSpawnTile(randX, randY);
            }
            else
            {
                tile = map[locationToSpawn].GetComponent<OverlayTile>();
            }
        }
        else
        {
            int randX = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
            int randY = Random.Range(bounds.min.y + 1, bounds.max.y - 1);

            tile = GetObstacleSpawnTile(randX, randY);
        }
        return tile;
    }


    // finds neighbours of each tile for movement
    public List<OverlayTile> GetNeightbourTilesMove(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, BaseUnit character)
    {

        Dictionary<Vector2Int, OverlayTile> tilesToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if( searchTiles.Count > 0 )
        {
            foreach ( var item in searchTiles )
            {
                tilesToSearch.Add(item.grid2DLocation, item);
            }
        } else
        {
            tilesToSearch = GridManager.Instance.map;
        }

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //right
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == null || GameManager.Instance.GameState == GameState.EnemyTurn && tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == AIManager.Instance.EnemyUnit.PriorityTarget)
                if (Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1)
                    if(character.Actions >= tilesToSearch[locationToCheck].GetComponent<OverlayTile>().moveCost || GameManager.Instance.GameState == GameState.EnemyTurn)
                        neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == null || GameManager.Instance.GameState == GameState.EnemyTurn && tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == AIManager.Instance.EnemyUnit.PriorityTarget)
                if (Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1)
                    if (character.Actions >= tilesToSearch[locationToCheck].GetComponent<OverlayTile>().moveCost || GameManager.Instance.GameState == GameState.EnemyTurn)
                        neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //top
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == null || GameManager.Instance.GameState == GameState.EnemyTurn && tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == AIManager.Instance.EnemyUnit.PriorityTarget)
                if (Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1)
                    if (character.Actions >= tilesToSearch[locationToCheck].GetComponent<OverlayTile>().moveCost || GameManager.Instance.GameState == GameState.EnemyTurn)
                        neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == null || GameManager.Instance.GameState == GameState.EnemyTurn && tilesToSearch[locationToCheck].GetComponent<OverlayTile>().OccupiedUnit == AIManager.Instance.EnemyUnit.PriorityTarget)
                if (Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1)
                    if (character.Actions >= tilesToSearch[locationToCheck].GetComponent<OverlayTile>().moveCost || GameManager.Instance.GameState == GameState.EnemyTurn)
                        neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        return neighbours;

    }

    // finds neighbours of each tile for attack
    public List<OverlayTile> GetNeightbourTilesAttack(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, BaseUnit character)
    {

        Dictionary<Vector2Int, OverlayTile> tilesToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if (searchTiles.Count > 0)
        {
            foreach (var item in searchTiles)
            {
                tilesToSearch.Add(item.grid2DLocation, item);
            }
        }
        else
        {
            tilesToSearch = GridManager.Instance.map;
        }

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //right
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (character.AttackRange > 1 || Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1 )
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (character.AttackRange > 1 || Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1 )
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //top
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if (character.AttackRange > 1 || Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1 )
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            if ( character.AttackRange > 1 || Mathf.Abs(currentOverlayTile.GetComponent<OverlayTile>().heightLevel - tilesToSearch[locationToCheck].GetComponent<OverlayTile>().heightLevel) <= 1 )
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        return neighbours;

    }

    // finds neighbours of each tile for attack
    public List<OverlayTile> GetNeightbourTilesAll(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, BaseUnit character)
    {

        Dictionary<Vector2Int, OverlayTile> tilesToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if (searchTiles.Count > 0)
        {
            foreach (var item in searchTiles)
            {
                tilesToSearch.Add(item.grid2DLocation, item);
            }
        }
        else
        {
            tilesToSearch = GridManager.Instance.map;
        }

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //right
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //top
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //top right 
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //top left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //bottom right
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        //bottom left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
        }

        return neighbours;

    }

    // finds neighbours of each tile for attack
    public List<OverlayTile> GetCleaveTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, string direction)
    {

        Dictionary<Vector2Int, OverlayTile> tilesToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if (searchTiles.Count > 0)
        {
            foreach (var item in searchTiles)
            {
                tilesToSearch.Add(item.grid2DLocation, item);
            }
        }
        else
        {
            tilesToSearch = GridManager.Instance.map;
        }

        List<OverlayTile> neighbours = new List<OverlayTile>();
        Vector2Int locationToCheck = new Vector2Int();

        // from top
        if (direction == "top")
        {
            //top
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y + 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //top left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y + 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
        }

        // from right
        if (direction == "right")
        {
            //right
            locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //top right 
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x + 1,
                currentOverlayTile.gridLocation.y + 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //top
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y + 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
        }

        // from left
        if (direction == "left")
        {
            //left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //bottom left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y - 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //bottom
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y - 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
        }
        // from bottom
        if (direction == "bottom")
        {
            //bottom
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y - 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //bottom right
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x + 1,
                currentOverlayTile.gridLocation.y - 1
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
            //right
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x + 1,
                currentOverlayTile.gridLocation.y
            );

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                neighbours.Add(map[locationToCheck].GetComponent<OverlayTile>());
            }
        }

        return neighbours;

    }

    public int GetTilesPathShoot(BaseUnit Attacker, BaseUnit Defender)
    {
        int attackRange = Attacker.AttackRange + Attacker.OccupiedTile.heightLevel;
        int distance = 0;
        List<OverlayTile> tilesForPreviousStep = new List<OverlayTile>();
        tilesForPreviousStep.Add(Attacker.OccupiedTile);

        for (int i = 0; i < attackRange; i++)
        {
            distance++;
            var surroundingTiles = new List<OverlayTile>();
            foreach (var item in tilesForPreviousStep)
            {
                surroundingTiles.AddRange(GetNeightbourTilesAll(item, new List<OverlayTile>(), Attacker));

            }
            
            tilesForPreviousStep = surroundingTiles.Distinct().ToList();
            if (tilesForPreviousStep.Contains(Defender.OccupiedTile))
            {
                break;
            }
        }

        return distance;
    }

}
