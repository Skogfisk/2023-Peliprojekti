using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RangeFinder
{
    public static RangeFinder Instance;
    void Awake()
    {
        Instance = this;
    }

    public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int range)
    {
        var inRangeTiles = new List<OverlayTile>();
        int stepCount = range;
        if (GameManager.Instance.Attacking == true) stepCount = range + 1;
        // add range for ranged attacks if unit is on higher altitude
        if (GameManager.Instance.Attacking == true && range > 2 && startingTile.heightLevel == 2) stepCount = range + 2;
        if (GameManager.Instance.Attacking == true && range > 2 && startingTile.heightLevel == 3) stepCount = range + 3;

        if (GameManager.Instance.Special1 == true) stepCount = range + 1;
        if (GameManager.Instance.Special2 == true)
        {
            stepCount = range + 1;
            BasePlayer playerU = UnitManager.Instance.SelectedUnit as BasePlayer;
            if (playerU.special2 == "Snipe" && range > 2 && startingTile.heightLevel == 2) stepCount = range + 2;
            if (playerU.special2 == "Snipe" && range > 2 && startingTile.heightLevel == 3) stepCount = range + 3;
        }
        


        inRangeTiles.Add(startingTile);

        //Should contain the surroundingTiles of the previous step. 
        List<OverlayTile> tilesForPreviousStep = new List<OverlayTile>();
        tilesForPreviousStep.Add(startingTile);
        while (stepCount > 1)
        {
            var surroundingTiles = new List<OverlayTile>();

            foreach (var item in tilesForPreviousStep)
            {
                if (GameManager.Instance.GameState == GameState.PlayerTurn)
                {
                    if (GameManager.Instance.Attacking == false && GameManager.Instance.Special1 == false && GameManager.Instance.Special2 == false)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesMove(item, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                    }
                    if (GameManager.Instance.Attacking == true)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(item, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                    }
                    if (GameManager.Instance.Special1 == true)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(item, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                    }
                    if (GameManager.Instance.Special2 == true)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(item, new List<OverlayTile>(), UnitManager.Instance.SelectedUnit));
                    }
                }
                if (GameManager.Instance.GameState == GameState.EnemyTurn)
                {
                    if (GameManager.Instance.Attacking == false)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesMove(item, new List<OverlayTile>(), AIManager.Instance.EnemyUnit));
                    }
                    if (GameManager.Instance.Attacking == true)
                    {
                        surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTilesAttack(item, new List<OverlayTile>(), AIManager.Instance.EnemyUnit));
                    }
                }
            }
            
            inRangeTiles.AddRange(surroundingTiles);
            tilesForPreviousStep = surroundingTiles.Distinct().ToList();
            if(GameManager.Instance.Attacking || GameManager.Instance.Special1 || GameManager.Instance.Special2) stepCount = stepCount - 1;
            else stepCount = stepCount - 2;
        }
        List<OverlayTile> returnList = inRangeTiles.Distinct().ToList();
        // if ranged attack make unable to shoot in melee range
        if (GameManager.Instance.GameState == GameState.PlayerTurn)
        {
            BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
            if (GameManager.Instance.Attacking == true && range > 1 || GameManager.Instance.Special2 == true && playerUnit.special2 == "Snipe" || GameManager.Instance.Special2 == true && playerUnit.special2 == "Throwingknife")
            {
                returnList.RemoveRange(1, 4);
            }
        }
        returnList.RemoveAt(0);

        return returnList;
    }


    // todo make better rangefinder with tile heigth and move cost differences

    //   public List<OverlayTile> GetTilesInRange2(OverlayTile startingTile, int actions)
    //   {
    //       var inRangeTiles = new List<OverlayTile>();
    //       int stepCount = 0;
    //       int actionsLeft = actions;
    //
    //       inRangeTiles.Add(startingTile);
    //
    //       //Should contain the surroundingTiles of the previous step. 
    //       var tilesForPreviousStep = new List<OverlayTile>();
    //       tilesForPreviousStep.Add(startingTile);
    //       while (stepCount < actions)
    //       {
    //           var surroundingTiles = new List<OverlayTile>();
    //
    //           foreach (var item in tilesForPreviousStep)
    //           {
    //               surroundingTiles.AddRange(GridManager.Instance.GetNeightbourTiles(item, new List<OverlayTile>()));
    //           }
    //
    //           inRangeTiles.AddRange(surroundingTiles);
    //           tilesForPreviousStep = surroundingTiles.Distinct().ToList();
    //           stepCount = stepCount + 2;
    //       }
    //
    //       return inRangeTiles.Distinct().ToList();
    //   }

    public List<OverlayTile> GetEnemyInRangeTiles(BaseUnit EnemyUnit)
    {
        return GetTilesInRange(EnemyUnit.OccupiedTile, EnemyUnit.Actions);
        
    }

}
