using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    // Find the best path by
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<OverlayTile> searchTiles)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);
        //Debug.Log(openList[0].name);

        //if (searchTiles.Contains(start))
        //    Debug.Log("Start found");
        //if (searchTiles.Contains(end))
        //    Debug.Log("End found");

        while (openList.Count > 0)
        {
            //Debug.Log(openList.Count);
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);
            if (currentOverlayTile == end)
            {
                // return found path
                //Debug.Log("Path found");
                return GetFinishedList(start, end);
            }
            var neighbourTiles = new List<OverlayTile>();
            if (GameManager.Instance.GameState == GameState.PlayerTurn)
            {
                neighbourTiles = GridManager.Instance.GetNeightbourTilesMove(currentOverlayTile, searchTiles, UnitManager.Instance.SelectedUnit);
            }
            if (GameManager.Instance.GameState == GameState.EnemyTurn)
            {
                //Debug.Log("Search enemy");
                neighbourTiles = GridManager.Instance.GetNeightbourTilesMove(currentOverlayTile, searchTiles, AIManager.Instance.EnemyUnit);
            }

            foreach (var tile in neighbourTiles)
            {
                // tile == AIManager.Instance.EnemyUnit.PriorityTarget.OccupiedTile
                if ( GameManager.Instance.GameState != GameState.EnemyTurn && tile.isBlocked || closedList.Contains(tile) )
                {
                    continue;
                }

                tile.G = GetManhattenDistance(start, tile);
                tile.H = GetManhattenDistance(end, tile);

                tile.Previous = currentOverlayTile;


                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }
            
        }
        //Debug.Log("Path not found");
        return new List<OverlayTile>();

    }

    // Find the best path by
    public List<OverlayTile> FindPathShoot(OverlayTile start, OverlayTile end, List<OverlayTile> searchTiles)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            //Debug.Log(openList.Count);
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);
            if (currentOverlayTile == end)
            {
                // return found path
                // Debug.Log("Path found");
                return GetFinishedList(start, end);
            }
            var neighbourTiles = new List<OverlayTile>();
            if (GameManager.Instance.GameState == GameState.PlayerTurn)
            {
                neighbourTiles = GridManager.Instance.GetNeightbourTilesAll(currentOverlayTile, searchTiles, UnitManager.Instance.SelectedUnit);
            }
            if (GameManager.Instance.GameState == GameState.EnemyTurn)
            {
                //Debug.Log("Search enemy");
                neighbourTiles = GridManager.Instance.GetNeightbourTilesAll(currentOverlayTile, searchTiles, AIManager.Instance.EnemyUnit);
            }

            foreach (var tile in neighbourTiles)
            {
                // tile == AIManager.Instance.EnemyUnit.PriorityTarget.OccupiedTile
                if (closedList.Contains(tile))
                {
                    continue;
                }

                tile.G = GetManhattenDistance(start, tile);
                tile.H = GetManhattenDistance(end, tile);

                tile.Previous = currentOverlayTile;


                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }

        }
        //Debug.Log("Path not found");
        return new List<OverlayTile>();

    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile tile)
    {
        return Mathf.Abs(start.gridLocation.x - tile.gridLocation.x) + Mathf.Abs(start.gridLocation.y - tile.gridLocation.y);

        // todo add height calculation
        //int moveS = start.moveCost;
        //int moveT = tile.moveCost;
        //if ( tile.heightLevel != start.heightLevel )
        //{
        //    moveT = moveT + 1;
        //    return Mathf.Abs((start.gridLocation.x + moveS) - (tile.gridLocation.x + moveT)) + Mathf.Abs((start.gridLocation.y + moveS) - (tile.gridLocation.y + moveT));
        //}
        //else
        //{
        //    return Mathf.Abs((start.gridLocation.x + moveS) - (tile.gridLocation.x + moveT)) + Mathf.Abs((start.gridLocation.y + moveS) - (tile.gridLocation.y + moveT));
        //}
    }



    // return the best found path
    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();
        OverlayTile currentTile = end;

        while (currentTile != start)
        {
            currentTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            finishedList.Add(currentTile);

            currentTile = currentTile.Previous;

        }

        finishedList.Reverse();

        return finishedList;
    }

}
