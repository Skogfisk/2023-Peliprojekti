using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipHandler : MonoBehaviour
{

    public static TooltipHandler Instance;

    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    

    private void Awake()
    {
        Instance = this;
        backgroundRectTransform = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        textMeshPro = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        //HideTooltip();
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize;
        Vector2 padding = new Vector2(6f, 10f);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    // show default tooltip
    public void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        SetText(tooltipString);
    }

    // show movement tooltip
    public void ShowTooltipMove(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipString = "Move \nCost 2AP per tile";
        if (UnitManager.Instance.SelectedUnit.Actions < 2)
            tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        SetText(tooltipString);
    }

    // show attack tooltip
    public void ShowTooltipAttack(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipString = "Attack enemy in range \nCost 4AP";
        if (UnitManager.Instance.SelectedUnit.UnitName == "Rogue")
            tooltipString = "Attack enemy in range \nCost 3AP";
        if (UnitManager.Instance.SelectedUnit.Actions < 4 && UnitManager.Instance.SelectedUnit.UnitName != "Rogue")
            tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        if (UnitManager.Instance.SelectedUnit.Actions < 3 && UnitManager.Instance.SelectedUnit.UnitName == "Rogue")
            tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        SetText(tooltipString);
    }

    // show special 1 tooltip
    public void ShowTooltipSpecial1(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipString = "Attack enemy in range \nCost 4AP";
        if (UnitManager.Instance.SelectedUnit.UnitName == "Knight")
        {
            tooltipString = "Reduce incoming damage by 25% for a turn\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Archer" || UnitManager.Instance.SelectedUnit.UnitName == "Mage")
        {
            tooltipString = "Weak melee attack\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Cleric")
        {
            tooltipString = "Heal a friendly unit for " + UnitManager.Instance.SelectedUnit.AttackPower.ToString() + " HP\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Rogue")
        {
            tooltipString = "Attack enemy bypassing armor protection\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";

        }
        BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
        if (playerUnit.specialCooldown1 > 0 )
            tooltipString = tooltipString + "<color=#FFEB04>\nAbility on cooldown for " + playerUnit.specialCooldown1.ToString() + " turn</color>";
        SetText(tooltipString);
    }

    // show special 2 tooltip
    public void ShowTooltipSpecial2(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipString = "Attack enemy in range \nCost 4AP";
        if (UnitManager.Instance.SelectedUnit.UnitName == "Knight")
        {
            tooltipString = "Attack all units in 3 tiles\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Archer" )
        {
            tooltipString = "Attack with more range and accuracy\nCost 6AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 6)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if ( UnitManager.Instance.SelectedUnit.UnitName == "Mage")
        {
            tooltipString = "Attack all units in 9 tiles\nCost 6AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 6)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Cleric")
        {
            tooltipString = "Increace friendly units attack by 30% for a turn\nCost 4AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 4)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";
        }
        if (UnitManager.Instance.SelectedUnit.UnitName == "Rogue")
        {
            tooltipString = "Short ranged attack\nCost 3AP";
            if (UnitManager.Instance.SelectedUnit.Actions < 3)
                tooltipString = tooltipString + "<color=#F00>\nNot enough AP</color>";

        }
        BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
        if (playerUnit.specialCooldown2 > 0)
            tooltipString = tooltipString + "<color=#FFEB04>\nAbility on cooldown for " + playerUnit.specialCooldown2.ToString() + " turn</color>";
        SetText(tooltipString);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    

}

