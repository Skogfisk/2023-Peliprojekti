using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] public GameObject _selectedUnitPanel, _selectedUnitName, _selectedUnitHealth, _selectedUnitActions, _selectedUnitImage, _selectedUnitArmor, _selectedUnitPower, _selectedUnitRange, _selectedUnitAttack, _selectedUnitDefence, _selectedUnitBuff, _selectedUnitTank, _selectedUnitArm, _selectedUnitLeg;
    [SerializeField] public GameObject _targetedUnitPanel, _targetedUnitName, _targetedUnitHealth, _targetedUnitActions, _targetedUnitImage, _targetedUnitArmor, _targetedUnitPower, _targetedUnitRange, _targetedUnitAttack, _targetedUnitDefence, _targetedUnitArm, _targetedUnitLeg;
    [SerializeField] public GameObject _actionPanel;
    [SerializeField] public GameObject _knightPanel, _knightImage, _knightHealth, _knightActions;
    [SerializeField] public GameObject _archerPanel, _archerImage, _archerHealth, _archerActions;
    [SerializeField] public GameObject _magePanel, _mageImage, _mageHealth, _mageActions;
    [SerializeField] public GameObject _clericPanel, _clericImage, _clericHealth, _clericActions;
    [SerializeField] public GameObject _roguePanel, _rogueImage, _rogueHealth, _rogueActions;

    [SerializeField] public Button _moveButton, _attackButton, _specialButton1, _specialButton2;
    [SerializeField] public GameObject _targetingPanel, _bodyPanel, _headPanel, _armsPanel, _legsPanel;
    [SerializeField] public GameObject _turnPanel;
    [SerializeField] public Button _endTurnButton;
    [SerializeField] public GameObject _endGamePanel, _turnsPanel, _damagesPanel;
    [SerializeField] public Button _openMenuButton;
    [SerializeField] public GameObject _menuPanel;
    [SerializeField] public Button _returnMenuButton, _nextLevelButton;

    public GameObject _damageText;


    void Awake()
    {
        Instance = this;
        _targetingPanel.SetActive(false);
        _actionPanel.SetActive(false);
        _selectedUnitPanel.SetActive(false);
        _targetedUnitPanel.SetActive(false);

    }

    void Update()
    {
        _knightImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance._knightUnit.ImageName);
        _knightHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance._knightUnit.Health.ToString() + "/" + UnitManager.Instance._knightUnit.MaxHealth.ToString();
        _knightActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance._knightUnit.Actions.ToString() + "/" + UnitManager.Instance._knightUnit.MaxActions.ToString();
        
        _archerImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance._archerUnit.ImageName);
        _archerHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance._archerUnit.Health.ToString() + "/" + UnitManager.Instance._archerUnit.MaxHealth.ToString();
        _archerActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance._archerUnit.Actions.ToString() + "/" + UnitManager.Instance._archerUnit.MaxActions.ToString();
        
        _mageImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance._mageUnit.ImageName);
        _mageHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance._mageUnit.Health.ToString() + "/" + UnitManager.Instance._mageUnit.MaxHealth.ToString();
        _mageActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance._mageUnit.Actions.ToString() + "/" + UnitManager.Instance._mageUnit.MaxActions.ToString();
        
        _clericImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance._clericUnit.ImageName);
        _clericHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance._clericUnit.Health.ToString() + "/" + UnitManager.Instance._clericUnit.MaxHealth.ToString();
        _clericActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance._clericUnit.Actions.ToString() + "/" + UnitManager.Instance._clericUnit.MaxActions.ToString();
        
        _rogueImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance._rogueUnit.ImageName);
        _rogueHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance._rogueUnit.Health.ToString() + "/" + UnitManager.Instance._rogueUnit.MaxHealth.ToString();
        _rogueActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance._rogueUnit.Actions.ToString() + "/" + UnitManager.Instance._rogueUnit.MaxActions.ToString();

        // display if units have less than 2 actions meaning can't move anymore
        if (UnitManager.Instance._knightUnit.Actions < 2)
        {
            Color newC = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _knightActions.GetComponent<Image>().color = newC;
        }
        if (UnitManager.Instance._archerUnit.Actions < 2)
        {
            Color newC = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _archerActions.GetComponent<Image>().color = newC;
        }
        if (UnitManager.Instance._mageUnit.Actions < 2)
        {
            Color newC = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _mageActions.GetComponent<Image>().color = newC;
        }
        if (UnitManager.Instance._clericUnit.Actions < 2)
        {
            Color newC = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _clericActions.GetComponent<Image>().color = newC;
        }
        if (UnitManager.Instance._rogueUnit.Actions < 2)
        {
            Color newC = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _rogueActions.GetComponent<Image>().color = newC;
        }
        // highlight end turn button when no units can move
        if (UnitManager.Instance._knightUnit.Actions < 2 && UnitManager.Instance._archerUnit.Actions < 2 && UnitManager.Instance._mageUnit.Actions < 2 && UnitManager.Instance._clericUnit.Actions < 2 && UnitManager.Instance._rogueUnit.Actions < 2)
        {
            _endTurnButton.GetComponent<Image>().color = Color.yellow;
        }

    }

    public void ShowSelectedUnit()
    {
        if (UnitManager.Instance.SelectedUnit == null)
        {
            _selectedUnitPanel.SetActive(false);
            _selectedUnitName.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitHealth.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitActions.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitPower.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitRange.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitAttack.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitDefence.GetComponentInChildren<TMP_Text>().text = "";
            _selectedUnitImage.GetComponentInChildren<Image>().sprite = null;
            _selectedUnitArmor.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            _selectedUnitArmor.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            _selectedUnitArmor.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            _selectedUnitArmor.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            _actionPanel.SetActive(false);
            _selectedUnitArm.SetActive(false);
            _selectedUnitLeg.SetActive(false);
            _selectedUnitBuff.SetActive(false);
            _selectedUnitTank.SetActive(false);
            _specialButton1.interactable = false;
            _specialButton2.interactable = false;
            _specialButton1.GetComponentInChildren<TMP_Text>().text = "Special 1";
            _specialButton2.GetComponentInChildren<TMP_Text>().text = "Special 2";
            return;
        }
        _selectedUnitPanel.SetActive(true);
        _selectedUnitName.GetComponentInChildren<TMP_Text>().text = UnitManager.Instance.SelectedUnit.UnitName;
        _selectedUnitHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitManager.Instance.SelectedUnit.Health.ToString() + "/" + UnitManager.Instance.SelectedUnit.MaxHealth.ToString();
        _selectedUnitActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitManager.Instance.SelectedUnit.Actions.ToString() + "/" + UnitManager.Instance.SelectedUnit.MaxActions.ToString();
        _selectedUnitPower.GetComponentInChildren<TMP_Text>().text = "Power:" + UnitManager.Instance.SelectedUnit.AttackPower.ToString();
        _selectedUnitRange.GetComponentInChildren<TMP_Text>().text = "Range:" + UnitManager.Instance.SelectedUnit.AttackRange.ToString();
        _selectedUnitAttack.GetComponentInChildren<TMP_Text>().text = "Attack:" + UnitManager.Instance.SelectedUnit.AttackSkill.ToString();
        _selectedUnitDefence.GetComponentInChildren<TMP_Text>().text = "Defence:" + UnitManager.Instance.SelectedUnit.DefenceSkill.ToString();
        _selectedUnitImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitManager.Instance.SelectedUnit.ImageName);
        if (UnitManager.Instance.SelectedUnit.HeadArmor == 1) _selectedUnitArmor.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (UnitManager.Instance.SelectedUnit.BodyArmor == 1) _selectedUnitArmor.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (UnitManager.Instance.SelectedUnit.ArmsArmor == 1) _selectedUnitArmor.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (UnitManager.Instance.SelectedUnit.LegsArmor == 1) _selectedUnitArmor.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);




        _actionPanel.SetActive(true);
        _attackButton.interactable = true;

        if (UnitManager.Instance.SelectedUnit.armDebuff == true)
            _selectedUnitArm.SetActive(true);

        if (UnitManager.Instance.SelectedUnit.legDebuff == true)
            _selectedUnitLeg.SetActive(true);

        BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;

        if (playerUnit.buffed == true)
            _selectedUnitBuff.SetActive(true);
        if (UnitManager.Instance.SelectedUnit.UnitName == "Knight")
        {
            Player1 kightDef = UnitManager.Instance.SelectedUnit as Player1;
            if (kightDef.tanking == true)
                _selectedUnitTank.SetActive(true);
        }

        if (playerUnit.specialActive1 == true )//&& _specialButton1.interactable == false)
        {
            //allows the special button to be used
            if (UnitManager.Instance.SelectedUnit.UnitName == "Knight" && UnitManager.Instance.SelectedUnit.Actions < 4) ;
            else _specialButton1.interactable = true;
            _specialButton1.GetComponentInChildren<TMP_Text>().text = playerUnit.special1;
            if (playerUnit.specialCooldown1 > 0)
            {
                _specialButton1.interactable = false;
                _specialButton1.GetComponentInChildren<TMP_Text>().text = playerUnit.special1 + " wait " + playerUnit.specialCooldown1.ToString();
            }
        }
        if (playerUnit.specialActive2 == true )//&& _specialButton2.interactable == false)
        {
            //allows the special button to be used
            _specialButton2.interactable = true;
            _specialButton2.GetComponentInChildren<TMP_Text>().text = playerUnit.special2;
            if (playerUnit.specialCooldown2 > 0)
            {
                _specialButton2.interactable = false;
                _specialButton2.GetComponentInChildren<TMP_Text>().text = playerUnit.special2 + " wait " + playerUnit.specialCooldown2.ToString();
            }
        }
        if (playerUnit.AttackRange > 2 )
        {
            foreach (BaseUnit enemy in GameManager.Instance.enemyUnits)
            {
                if (enemy.ControlledTiles.Contains(playerUnit.OccupiedTile))
                {
                    _attackButton.interactable = false;
                    _specialButton2.interactable = false;
                }
            }
        }

    }

    public void ShowTargetedUnit(BaseUnit unit)
    {
        HideTargetedUnit();
        _targetedUnitPanel.SetActive(true);
        _targetedUnitName.GetComponentInChildren<TMP_Text>().text = unit.UnitName;
        _targetedUnitHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + unit.Health.ToString() + "/" + unit.MaxHealth.ToString();
        _targetedUnitActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + unit.Actions.ToString() + "/" + unit.MaxActions.ToString();
        _targetedUnitPower.GetComponentInChildren<TMP_Text>().text = "Power:" + unit.AttackPower.ToString();
        _targetedUnitRange.GetComponentInChildren<TMP_Text>().text = "Range:" + unit.AttackRange.ToString();
        _targetedUnitAttack.GetComponentInChildren<TMP_Text>().text = "Attack:" + unit.AttackSkill.ToString();
        _targetedUnitDefence.GetComponentInChildren<TMP_Text>().text = "Defence:" + unit.DefenceSkill.ToString();
        _targetedUnitImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(unit.ImageName);
        if (unit.HeadArmor == 1) _targetedUnitArmor.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (unit.BodyArmor == 1) _targetedUnitArmor.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (unit.ArmsArmor == 1) _targetedUnitArmor.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (unit.LegsArmor == 1) _targetedUnitArmor.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);

        if (unit.armDebuff == true)
            _targetedUnitArm.SetActive(true);

        if (unit.legDebuff == true)
            _targetedUnitLeg.SetActive(true);


    }

    public void HideTargetedUnit()
    {
        _targetedUnitPanel.SetActive(false);
        _targetedUnitName.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitHealth.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitActions.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitPower.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitRange.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitAttack.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitDefence.GetComponentInChildren<TMP_Text>().text = "";
        _targetedUnitImage.GetComponentInChildren<Image>().sprite = null;
        _targetedUnitArmor.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetedUnitArmor.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetedUnitArmor.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetedUnitArmor.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetedUnitArm.SetActive(false);
        _targetedUnitLeg.SetActive(false);

    }

    public void ShowTargetPanel(BaseUnit Attacker, BaseUnit Defender, int RangePenalty)
    {
        if (Defender.HeadArmor == 1) _targetingPanel.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (Defender.BodyArmor == 1) _targetingPanel.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (Defender.ArmsArmor == 1) _targetingPanel.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);
        if (Defender.LegsArmor == 1) _targetingPanel.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(112, 128, 144, 255);

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

        BasePlayer playerUnit = Attacker as BasePlayer;
        if (GameManager.Instance.Special1 == true && playerUnit.special1 == "Melee")
        {
            RangePenalty = 10;
        }
        if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Snipe")
        {
            RangePenalty = RangePenalty - 10;
        }
        if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Throwingknife")
        {
            // Throwingknife accuracy penalty
            RangePenalty = RangePenalty + 3;
        }
        if (GameManager.Instance.Special1 == true && playerUnit.special2 == "Pierce")
        {
            // Throwingknife accuracy penalty
            RangePenalty = 5;
        }

        int TargetPenalty = 0;
        int attackchance = Attacker.AttackSkill - Defender.DefenceSkill - TargetPenalty - RangePenalty + HeightModifier;
        _bodyPanel.GetComponentInChildren<TMP_Text>().text = "Hit chance: " + attackchance.ToString() + "\nDamage: " + Mathf.RoundToInt(Attacker.AttackPower);
        TargetPenalty = 15;
        attackchance = Attacker.AttackSkill - Defender.DefenceSkill - TargetPenalty - RangePenalty + HeightModifier;
        _headPanel.GetComponentInChildren<TMP_Text>().text = "Hit chance: " + attackchance.ToString() + "\nDamage: " + Mathf.RoundToInt(Attacker.AttackPower * 1.5f);
        TargetPenalty = 5;
        attackchance = Attacker.AttackSkill - Defender.DefenceSkill - TargetPenalty - RangePenalty + HeightModifier;
        _armsPanel.GetComponentInChildren<TMP_Text>().text = "Hit chance: " + attackchance.ToString() + "\nDamage: " + Mathf.RoundToInt(Attacker.AttackPower * 0.7f);
        _legsPanel.GetComponentInChildren<TMP_Text>().text = "Hit chance: " + attackchance.ToString() + "\nDamage: " + Mathf.RoundToInt(Attacker.AttackPower * 0.8f);

        _targetingPanel.SetActive(true);
    }

    public void HideTargetPanel()
    {
        _targetingPanel.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetingPanel.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetingPanel.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        _targetingPanel.transform.GetChild(3).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);

        _targetingPanel.SetActive(false);
    }

    public void ShowDamage( string dmg, BaseUnit unit)
    {
        Vector3 pos = new Vector3(unit.transform.position.x, unit.transform.position.y + 0.6001f, 20);
        var popup = Instantiate(_damageText, pos, Quaternion.identity);

        popup.GetComponentInChildren<TMP_Text>().text = dmg;
        BasePlayer playerUnit = UnitManager.Instance.SelectedUnit as BasePlayer;
        if (GameManager.Instance.Special1 == true && playerUnit.special1 == "Heal") popup.GetComponentInChildren<TMP_Text>().color = Color.green;
        else if (GameManager.Instance.Special2 == true && playerUnit.special2 == "Buff") popup.GetComponentInChildren<TMP_Text>().color = Color.cyan;
        else if (GameManager.Instance.Special1 == true && playerUnit.special1 == "Tank") popup.GetComponentInChildren<TMP_Text>().color = Color.cyan;
        else if (dmg != "Miss")
        {
            if (unit.Faction == Faction.Enemy)
            {
                if (dmg != "Miss") popup.GetComponentInChildren<TMP_Text>().color = Color.yellow;
            }
            else
            {
                if (dmg != "Miss") popup.GetComponentInChildren<TMP_Text>().color = Color.red;
            }
        }
        
        StartCoroutine(DeleteDamage(popup));

    }
    int i = 0;
    private IEnumerator DeleteDamage(GameObject popup)
    {
        yield return new WaitForSeconds(2);
        Destroy(popup.gameObject);
    }

    public void DisableEndTurn()
    {
        _endTurnButton.interactable = false;
        _endTurnButton.GetComponentInChildren<TMP_Text>().text = "Enemy Turn";
    }
    public void EnableEndTurn()
    {
        Color newC = new Color(1f, 1f, 1f, 0.3921569f);
        _knightActions.GetComponent<Image>().color = newC;
        _archerActions.GetComponent<Image>().color = newC;
        _mageActions.GetComponent<Image>().color = newC;
        _clericActions.GetComponent<Image>().color = newC;
        _rogueActions.GetComponent<Image>().color = newC;
        _endTurnButton.GetComponent<Image>().color = Color.white;

        _endTurnButton.interactable = true;
        _endTurnButton.GetComponent<Image>().color = Color.white;
        _endTurnButton.GetComponentInChildren<TMP_Text>().text = "End Turn";
    }

    public void SetMoving()
    {
        GameManager.Instance.Attacking = false;
        GameManager.Instance.Special1 = false;
        GameManager.Instance.Special2 = false;
        ShowSelectedUnit();
        MouseController.Instance.GetInRangeTiles();
    }

    public void SetAttacking()
    {
        GameManager.Instance.Attacking = true;
        GameManager.Instance.Special1 = false;
        GameManager.Instance.Special2 = false;
        ShowSelectedUnit();
        MouseController.Instance.GetInRangeTiles();
    }

    public void SetSpecial1()
    {
        if (UnitManager.Instance.SelectedUnit.UnitName == "Knight")
        {
            
            Player1 knight = UnitManager.Instance.SelectedUnit as Player1;
            if (knight.tanking == false && knight.Actions >= 4)
            {
                knight.tanking = true;
                knight.Actions = knight.Actions - 4;
                knight.specialCooldown1 = 1;
                SoundManager.Instance.PlayTankSound();
                ShowDamage("Tanking", knight);
            }
        }
        else
        {
            GameManager.Instance.Special1 = true;
            GameManager.Instance.Special2 = false;
            GameManager.Instance.Attacking = false;
        }
        ShowSelectedUnit();
        MouseController.Instance.GetInRangeTiles();
    }

    public void SetSpecial2()
    {
        GameManager.Instance.Special1 = false;
        GameManager.Instance.Special2 = true;
        GameManager.Instance.Attacking = false;
        ShowSelectedUnit();
        MouseController.Instance.GetInRangeTiles();
    }

    public void WinGame()
    {
        _endGamePanel.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            _nextLevelButton.interactable = false;
        }
        _endGamePanel.GetComponentInChildren<TMP_Text>().text = "Victory";
        int turn = GameManager.Instance.TurnNumber - 1;
        _turnsPanel.GetComponentInChildren<TMP_Text>().text = "You won in " + turn + " turns";
        _damagesPanel.GetComponentInChildren<TMP_Text>().text = "You did " + GameManager.Instance.DamageDone + " damage to enemy and your units took "+ GameManager.Instance.DamageTaken + " damage";
    }

    public void LoseGame()
    {
        _endGamePanel.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            _nextLevelButton.interactable = false;
        }
        _endGamePanel.GetComponentInChildren<TMP_Text>().text = "Defeat";
        int turn = GameManager.Instance.TurnNumber - 1;
        _turnsPanel.GetComponentInChildren<TMP_Text>().text = "You lost in " + turn + " turns";
        _damagesPanel.GetComponentInChildren<TMP_Text>().text = "You did " + GameManager.Instance.DamageDone + " damage to enemy and your units took " + GameManager.Instance.DamageTaken + " damage";
    }

    public void OpenandCloseMenu()
    {
        if (_menuPanel.activeSelf == false)
        {
            _menuPanel.SetActive(true);
        }
        else
        {
            _menuPanel.SetActive(false);
        }
    }

    //public void OpenShop()
    //{
    //    SceneManager.LoadScene(9);
    //}
    public void MenuReturn()
    {
        GameManager.Instance.levelSO._value = 0;
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        // open upgrade shop
        SceneManager.LoadScene(9);

        // save units stats for next level
        // knight
        if (UnitManager.Instance._knightUnit.Health <= 0) // if unit died permanent injury to max health
        {
            UnitStats1.Health = 1;
            UnitStats1.MaxHealth = UnitStats1.MaxHealth - 5;
        }
        else
        {
            UnitStats1.UnitName = UnitManager.Instance._knightUnit.UnitName;
            UnitStats1.ImageName = UnitManager.Instance._knightUnit.ImageName;
            UnitStats1.Health = UnitManager.Instance._knightUnit.Health;
            UnitStats1.MaxHealth = UnitManager.Instance._knightUnit.MaxHealth;
            UnitStats1.Actions = UnitManager.Instance._knightUnit.Actions;
            UnitStats1.MaxActions = UnitManager.Instance._knightUnit.MaxActions;
            UnitStats1.AttackPower = UnitManager.Instance._knightUnit.AttackPower;
            UnitStats1.AttackRange = UnitManager.Instance._knightUnit.AttackRange;
            UnitStats1.AttackSkill = UnitManager.Instance._knightUnit.AttackSkill;
            UnitStats1.DefenceSkill = UnitManager.Instance._knightUnit.DefenceSkill;
            UnitStats1.HeadArmor = UnitManager.Instance._knightUnit.HeadArmor;
            UnitStats1.BodyArmor = UnitManager.Instance._knightUnit.BodyArmor;
            UnitStats1.ArmsArmor = UnitManager.Instance._knightUnit.ArmsArmor;
            UnitStats1.LegsArmor = UnitManager.Instance._knightUnit.LegsArmor;
        }

        //archer
        if (UnitManager.Instance._archerUnit.Health <= 0)
        {
            UnitStats2.Health = 1;
            UnitStats2.MaxHealth = UnitStats2.MaxHealth - 5;
        }
        else
        {
            UnitStats2.UnitName = UnitManager.Instance._archerUnit.UnitName;
            UnitStats2.ImageName = UnitManager.Instance._archerUnit.ImageName;
            UnitStats2.Health = UnitManager.Instance._archerUnit.Health;
            UnitStats2.MaxHealth = UnitManager.Instance._archerUnit.MaxHealth;
            UnitStats2.Actions = UnitManager.Instance._archerUnit.Actions;
            UnitStats2.MaxActions = UnitManager.Instance._archerUnit.MaxActions;
            UnitStats2.AttackPower = UnitManager.Instance._archerUnit.AttackPower;
            UnitStats2.AttackRange = UnitManager.Instance._archerUnit.AttackRange;
            UnitStats2.AttackSkill = UnitManager.Instance._archerUnit.AttackSkill;
            UnitStats2.DefenceSkill = UnitManager.Instance._archerUnit.DefenceSkill;
            UnitStats2.HeadArmor = UnitManager.Instance._archerUnit.HeadArmor;
            UnitStats2.BodyArmor = UnitManager.Instance._archerUnit.BodyArmor;
            UnitStats2.ArmsArmor = UnitManager.Instance._archerUnit.ArmsArmor;
            UnitStats2.LegsArmor = UnitManager.Instance._archerUnit.LegsArmor;
        }

        //mage
        if(UnitManager.Instance._mageUnit.Health <= 0)
        {
            UnitStats3.Health = 1;
            UnitStats3.MaxHealth = UnitStats3.MaxHealth - 5;
        }
        else
        {
            UnitStats3.UnitName = UnitManager.Instance._mageUnit.UnitName;
            UnitStats3.ImageName = UnitManager.Instance._mageUnit.ImageName;
            UnitStats3.Health = UnitManager.Instance._mageUnit.Health;
            UnitStats3.MaxHealth = UnitManager.Instance._mageUnit.MaxHealth;
            UnitStats3.Actions = UnitManager.Instance._mageUnit.Actions;
            UnitStats3.MaxActions = UnitManager.Instance._mageUnit.MaxActions;
            UnitStats3.AttackPower = UnitManager.Instance._mageUnit.AttackPower;
            UnitStats3.AttackRange = UnitManager.Instance._mageUnit.AttackRange;
            UnitStats3.AttackSkill = UnitManager.Instance._mageUnit.AttackSkill;
            UnitStats3.DefenceSkill = UnitManager.Instance._mageUnit.DefenceSkill;
            UnitStats3.HeadArmor = UnitManager.Instance._mageUnit.HeadArmor;
            UnitStats3.BodyArmor = UnitManager.Instance._mageUnit.BodyArmor;
            UnitStats3.ArmsArmor = UnitManager.Instance._mageUnit.ArmsArmor;
            UnitStats3.LegsArmor = UnitManager.Instance._mageUnit.LegsArmor;
        }

        //cleric
        if (UnitManager.Instance._clericUnit.Health <= 0)
        {
            UnitStats4.Health = 1;
            UnitStats4.MaxHealth = UnitStats4.MaxHealth - 5;
        }
        else
        {
            UnitStats4.UnitName = UnitManager.Instance._clericUnit.UnitName;
            UnitStats4.ImageName = UnitManager.Instance._clericUnit.ImageName;
            UnitStats4.Health = UnitManager.Instance._clericUnit.Health;
            UnitStats4.MaxHealth = UnitManager.Instance._clericUnit.MaxHealth;
            UnitStats4.Actions = UnitManager.Instance._clericUnit.Actions;
            UnitStats4.MaxActions = UnitManager.Instance._clericUnit.MaxActions;
            UnitStats4.AttackPower = UnitManager.Instance._clericUnit.AttackPower;
            UnitStats4.AttackRange = UnitManager.Instance._clericUnit.AttackRange;
            UnitStats4.AttackSkill = UnitManager.Instance._clericUnit.AttackSkill;
            UnitStats4.DefenceSkill = UnitManager.Instance._clericUnit.DefenceSkill;
            UnitStats4.HeadArmor = UnitManager.Instance._clericUnit.HeadArmor;
            UnitStats4.BodyArmor = UnitManager.Instance._clericUnit.BodyArmor;
            UnitStats4.ArmsArmor = UnitManager.Instance._clericUnit.ArmsArmor;
            UnitStats4.LegsArmor = UnitManager.Instance._clericUnit.LegsArmor;
        }

        //rogue
        if (UnitManager.Instance._rogueUnit.Health <= 0) // if unit died reduce max health
        {
            UnitStats5.Health = 1;
            UnitStats5.MaxHealth = UnitStats5.MaxHealth - 5;
        }
        else
        {
            UnitStats5.UnitName = UnitManager.Instance._rogueUnit.UnitName;
            UnitStats5.ImageName = UnitManager.Instance._rogueUnit.ImageName;
            UnitStats5.Health = UnitManager.Instance._rogueUnit.Health;
            UnitStats5.MaxHealth = UnitManager.Instance._rogueUnit.MaxHealth;
            UnitStats5.Actions = UnitManager.Instance._rogueUnit.Actions;
            UnitStats5.MaxActions = UnitManager.Instance._rogueUnit.MaxActions;
            UnitStats5.AttackPower = UnitManager.Instance._rogueUnit.AttackPower;
            UnitStats5.AttackRange = UnitManager.Instance._rogueUnit.AttackRange;
            UnitStats5.AttackSkill = UnitManager.Instance._rogueUnit.AttackSkill;
            UnitStats5.DefenceSkill = UnitManager.Instance._rogueUnit.DefenceSkill;
            UnitStats5.HeadArmor = UnitManager.Instance._rogueUnit.HeadArmor;
            UnitStats5.BodyArmor = UnitManager.Instance._rogueUnit.BodyArmor;
            UnitStats5.ArmsArmor = UnitManager.Instance._rogueUnit.ArmsArmor;
            UnitStats5.LegsArmor = UnitManager.Instance._rogueUnit.LegsArmor;
        }
    }

}