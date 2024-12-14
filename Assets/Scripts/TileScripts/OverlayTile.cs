using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public string TileName;

    public BaseUnit OccupiedUnit;
    public List<BaseUnit> ControlledUnits;

    public int G;
    public int H;

    public int F { get { return G + H; } }

    public bool isBlocked = false;
    public bool isCover = false;

    public int moveCost;

    public int heightLevel;

    public OverlayTile Previous;

    public Vector3Int gridLocation;
    public Vector2Int grid2DLocation { get { return new Vector2Int(gridLocation.x, gridLocation.y); } }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideTile();
        }
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void HideTileDelay()
    {
        Invoke("HideTile", 1);
    }


    public void ShowTile()
    {
        
        if (GameManager.Instance.Attacking || GameManager.Instance.Special1 || GameManager.Instance.Special2) gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.4f, 0.4f, 0.75f);
        //else if(UnitManager.Instance.SelectedUnit != null && MouseController.Instance.inRangeTiles.Contains(this))
        //{
        //    foreach (BaseUnit child in AIManager.Instance.enemyUnits)
        //    {
        //        if (child.ControlledTiles.Contains(this))
        //        {
        //            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.4f, 0.2f, 0.75f);
        //            break;
        //        }
        //    }
        //}
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            foreach (BaseUnit child in ControlledUnits)
            {
                if ( child.Faction == Faction.Enemy && child.ControlledTiles.Contains(this))
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.4f, 0.2f, 0.75f);
                    break;
                }
            }
        }

    }

    public void ShowTile(Color col)
    {

        gameObject.GetComponent<SpriteRenderer>().color = col;

    }

}