using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ShopMenu : MonoBehaviour
{

    [SerializeField] private ScriptableValue levelSO, speedSO;

    [SerializeField] public GameObject _moneyPanel;
    [SerializeField] public GameObject _knightPanel, _knightImage, _knightHealth, _knightActions, _knightPower, _knightAttack, _knightDefence;
    [SerializeField] public GameObject _archerPanel, _archerImage, _archerHealth, _archerActions, _archerPower, _archerAttack, _archerDefence;
    [SerializeField] public GameObject _magePanel, _mageImage, _mageHealth, _mageActions, _magePower, _mageAttack, _mageDefence;
    [SerializeField] public GameObject _clericPanel, _clericImage, _clericHealth, _clericActions, _clericPower, _clericAttack, _clericDefence;
    [SerializeField] public GameObject _roguePanel, _rogueImage, _rogueHealth, _rogueActions, _roguePower, _rogueAttack, _rogueDefence;

    // money for buying and costs of different upgrades
    private int money = 1000;
    private int healCost = 50;
    private int healthCost = 200;
    private int actionCost = 500;
    private int powerCost = 300;
    private int attackCost = 100;
    private int defenceCost = 100;

    void Update()
    {
        _moneyPanel.GetComponentInChildren<TMP_Text>().text = "Money left: " + money.ToString();
        _knightHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitStats1.Health.ToString() + "/" + UnitStats1.MaxHealth.ToString();
        _knightActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitStats1.MaxActions.ToString();
        _knightPower.GetComponentInChildren<TMP_Text>().text = "AttackPower:" + UnitStats1.AttackPower.ToString();
        _knightAttack.GetComponentInChildren<TMP_Text>().text = "AttackSkill:" + UnitStats1.AttackSkill.ToString();
        _knightDefence.GetComponentInChildren<TMP_Text>().text = "DefenceSkill:" + UnitStats1.DefenceSkill.ToString();

        _archerHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitStats2.Health.ToString() + "/" + UnitStats2.MaxHealth.ToString();
        _archerActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitStats2.MaxActions.ToString();
        _archerPower.GetComponentInChildren<TMP_Text>().text = "AttackPower:" + UnitStats2.AttackPower.ToString();
        _archerAttack.GetComponentInChildren<TMP_Text>().text = "AttackSkill:" + UnitStats2.AttackSkill.ToString();
        _archerDefence.GetComponentInChildren<TMP_Text>().text = "DefenceSkill:" + UnitStats2.DefenceSkill.ToString();

        _mageHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitStats3.Health.ToString() + "/" + UnitStats3.MaxHealth.ToString();
        _mageActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitStats3.MaxActions.ToString();
        _magePower.GetComponentInChildren<TMP_Text>().text = "AttackPower:" + UnitStats3.AttackPower.ToString();
        _mageAttack.GetComponentInChildren<TMP_Text>().text = "AttackSkill:" + UnitStats3.AttackSkill.ToString();
        _mageDefence.GetComponentInChildren<TMP_Text>().text = "DefenceSkill:" + UnitStats3.DefenceSkill.ToString();

        _clericHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitStats4.Health.ToString() + "/" + UnitStats4.MaxHealth.ToString();
        _clericActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitStats4.MaxActions.ToString();
        _clericPower.GetComponentInChildren<TMP_Text>().text = "AttackPower:" + UnitStats4.AttackPower.ToString();
        _clericAttack.GetComponentInChildren<TMP_Text>().text = "AttackSkill:" + UnitStats4.AttackSkill.ToString();
        _clericDefence.GetComponentInChildren<TMP_Text>().text = "DefenceSkill:" + UnitStats4.DefenceSkill.ToString();

        _rogueHealth.GetComponentInChildren<TMP_Text>().text = "Health:" + UnitStats5.Health.ToString() + "/" + UnitStats5.MaxHealth.ToString();
        _rogueActions.GetComponentInChildren<TMP_Text>().text = "Actions:" + UnitStats5.MaxActions.ToString();
        _roguePower.GetComponentInChildren<TMP_Text>().text = "AttackPower:" + UnitStats5.AttackPower.ToString();
        _rogueAttack.GetComponentInChildren<TMP_Text>().text = "AttackSkill:" + UnitStats5.AttackSkill.ToString();
        _rogueDefence.GetComponentInChildren<TMP_Text>().text = "DefenceSkill:" + UnitStats5.DefenceSkill.ToString();
    }

    void Awake()
    {
        _knightImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitStats1.ImageName);
        _archerImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitStats2.ImageName);
        _mageImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitStats3.ImageName);
        _clericImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitStats4.ImageName);
        _rogueImage.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(UnitStats5.ImageName);

        // different money for later levels
        if (levelSO._value == 2 || levelSO._value == 3)
            money = 1200;
        if (levelSO._value == 4 || levelSO._value == 5 || levelSO._value == 6 )
            money = 1500;

    }

    public void StartGame ()
    {
        SceneManager.LoadScene(8);
    }

    public void ReturnToMenu()
    {
        levelSO._value = 0;
        SceneManager.LoadScene(0);
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    // buying upgrades for knight
    public void KnightHeal()
    {
        if (money >= healCost && UnitStats1.Health < UnitStats1.MaxHealth)
        {
            UnitStats1.Health = UnitStats1.Health + UnitStats1.MaxHealth /2;
            if (UnitStats1.Health > UnitStats1.MaxHealth)
                UnitStats1.Health = UnitStats1.MaxHealth;
            money = money - healCost;
        }
    }
    public void KnightHealth()
    {
        if (money >= healthCost)
        {
            UnitStats1.Health = UnitStats1.Health + 5;
            UnitStats1.MaxHealth = UnitStats1.MaxHealth + 5;
            money = money - healthCost;
        }
    }
    public void KnightActions()
    {
        if (money >= actionCost)
        {
            UnitStats1.MaxActions = UnitStats1.MaxActions + 1;
            money = money - actionCost;
        }
    }
    public void KnightPower()
    {
        if (money >= powerCost)
        {
            UnitStats1.AttackPower = UnitStats1.AttackPower + 1;
            money = money - powerCost;
        }
    }
    public void KnightAttack()
    {
        if (money >= attackCost)
        {
            UnitStats1.AttackSkill = UnitStats1.AttackSkill + 2;
            money = money - attackCost;
        }
    }
    public void KnightDefence()
    {
        if (money >= defenceCost)
        {
            UnitStats1.DefenceSkill = UnitStats1.DefenceSkill + 2;
            money = money - defenceCost;
        }
    }

    // buying upgrades for archer
    public void ArcherHeal()
    {
        if (money >= healCost && UnitStats2.Health < UnitStats2.MaxHealth)
        {
            UnitStats2.Health = UnitStats2.Health + UnitStats2.MaxHealth / 2;
            if (UnitStats2.Health > UnitStats2.MaxHealth)
                UnitStats2.Health = UnitStats2.MaxHealth;
            money = money - healCost;
        }
    }
    public void ArcherHealth()
    {
        if (money >= healthCost)
        {
            UnitStats2.Health = UnitStats2.Health + 5;
            UnitStats2.MaxHealth = UnitStats2.MaxHealth + 5;
            money = money - healthCost;
        }
    }
    public void ArcherActions()
    {
        if (money >= actionCost)
        {
            UnitStats2.MaxActions = UnitStats2.MaxActions + 1;
            money = money - actionCost;
        }
    }
    public void ArcherPower()
    {
        if (money >= powerCost)
        {
            UnitStats2.AttackPower = UnitStats2.AttackPower + 1;
            money = money - powerCost;
        }
    }
    public void ArcherAttack()
    {
        if (money >= attackCost)
        {
            UnitStats2.AttackSkill = UnitStats2.AttackSkill + 2;
            money = money - attackCost;
        }
    }
    public void ArcherDefence()
    {
        if (money >= defenceCost)
        {
            UnitStats2.DefenceSkill = UnitStats2.DefenceSkill + 2;
            money = money - defenceCost;
        }
    }
    // buying upgrades for mage
    public void MageHeal()
    {
        if (money >= healCost && UnitStats3.Health < UnitStats3.MaxHealth)
        {
            UnitStats3.Health = UnitStats3.Health + UnitStats3.MaxHealth / 2;
            if (UnitStats3.Health > UnitStats3.MaxHealth)
                UnitStats3.Health = UnitStats3.MaxHealth;
            money = money - healCost;
        }
    }
    public void MageHealth()
    {
        if (money >= healthCost)
        {
            UnitStats3.Health = UnitStats3.Health + 5;
            UnitStats3.MaxHealth = UnitStats3.MaxHealth + 5;
            money = money - healthCost;
        }
    }
    public void MageActions()
    {
        if (money >= actionCost)
        {
            UnitStats3.MaxActions = UnitStats3.MaxActions + 1;
            money = money - actionCost;
        }
    }
    public void MagePower()
    {
        if (money >= powerCost)
        {
            UnitStats3.AttackPower = UnitStats3.AttackPower + 1;
            money = money - powerCost;
        }
    }
    public void MageAttack()
    {
        if (money >= attackCost)
        {
            UnitStats3.AttackSkill = UnitStats3.AttackSkill + 2;
            money = money - attackCost;
        }
    }
    public void MageDefence()
    {
        if (money >= defenceCost)
        {
            UnitStats3.DefenceSkill = UnitStats3.DefenceSkill + 2;
            money = money - defenceCost;
        }
    }
    // buying upgrades for cleric
    public void ClericHeal()
    {
        if (money >= healCost && UnitStats4.Health < UnitStats4.MaxHealth)
        {
            UnitStats4.Health = UnitStats4.Health + UnitStats4.MaxHealth / 2;
            if (UnitStats4.Health > UnitStats4.MaxHealth)
                UnitStats4.Health = UnitStats4.MaxHealth;
            money = money - healCost;
        }
    }
    public void ClericHealth()
    {
        if (money >= healthCost)
        {
            UnitStats4.Health = UnitStats4.Health + 5;
            UnitStats4.MaxHealth = UnitStats4.MaxHealth + 5;
            money = money - healthCost;
        }
    }
    public void ClericActions()
    {
        if (money >= actionCost)
        {
            UnitStats4.MaxActions = UnitStats4.MaxActions + 1;
            money = money - actionCost;
        }
    }
    public void ClericPower()
    {
        if (money >= powerCost)
        {
            UnitStats4.AttackPower = UnitStats4.AttackPower + 1;
            money = money - powerCost;
        }
    }
    public void ClericAttack()
    {
        if (money >= attackCost)
        {
            UnitStats4.AttackSkill = UnitStats4.AttackSkill + 2;
            money = money - attackCost;
        }
    }
    public void ClericDefence()
    {
        if (money >= defenceCost)
        {
            UnitStats4.DefenceSkill = UnitStats4.DefenceSkill + 2;
            money = money - defenceCost;
        }
    }
    // buying upgrades for archer
    public void RogueHeal()
    {
        if (money >= healCost && UnitStats5.Health < UnitStats5.MaxHealth)
        {
            UnitStats5.Health = UnitStats5.Health + UnitStats5.MaxHealth / 2;
            if (UnitStats5.Health > UnitStats5.MaxHealth)
                UnitStats5.Health = UnitStats5.MaxHealth;
            money = money - healCost;
        }
    }
    public void RogueHealth()
    {
        if (money >= healthCost)
        {
            UnitStats5.Health = UnitStats5.Health + 5;
            UnitStats5.MaxHealth = UnitStats5.MaxHealth + 5;
            money = money - healthCost;
        }
    }
    public void RogueActions()
    {
        if (money >= actionCost)
        {
            UnitStats5.MaxActions = UnitStats5.MaxActions + 1;
            money = money - actionCost;
        }
    }
    public void RoguePower()
    {
        if (money >= powerCost)
        {
            UnitStats5.AttackPower = UnitStats5.AttackPower + 1;
            money = money - powerCost;
        }
    }
    public void RogueAttack()
    {
        if (money >= attackCost)
        {
            UnitStats5.AttackSkill = UnitStats5.AttackSkill + 2;
            money = money - attackCost;
        }
    }
    public void RogueDefence()
    {
        if (money >= defenceCost)
        {
            UnitStats5.DefenceSkill = UnitStats5.DefenceSkill + 2;
            money = money - defenceCost;
        }
    }
}
