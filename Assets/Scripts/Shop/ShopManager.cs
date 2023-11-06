using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] _shopItemSOCollection;
    public GameObject[] _shopPanelsGO;
    public ShopTemplate[] _shopPanels;
    public Button[] _myPurchaseBtns;

    static public float _stallTimerIncrease = 0;


    [SerializeField] GameObject _moneyCountGO;
    
    [SerializeField] Material _roadMaterial;

    

    [SerializeField] GameObject _rockToRemove;
    [SerializeField] GameObject _playerGO;

    static List<GameObject> _treesList = new List<GameObject>();
    static List<GameObject> _roadsList = new List<GameObject>();

    private bool _functionalityActive = true;

    void Start()
    {
        
        for (int i = 0; i < _shopItemSOCollection.Length; i++)
        {
            _shopPanelsGO[i].SetActive(true);
        }
        coinUI.text = "Coins: " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();

        _functionalityActive = !_functionalityActive;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var gameObjectToCheck = gameObject.transform.GetChild(i).gameObject;
            if (gameObjectToCheck != _moneyCountGO)
                gameObjectToCheck.SetActive(_functionalityActive);
        }
    }
    public void AddCoins(int coinsAdded = 1)
    {


        coins += coinsAdded;
        if (coins < 0) { coins = 0; };
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < _shopItemSOCollection.Length; i++)
        {
            if (coins >= _shopItemSOCollection[i].baseCost && _shopItemSOCollection[i].hasBeenBought == false)
                _myPurchaseBtns[i].interactable = true;
            else 
                _myPurchaseBtns[i].interactable = false ;

        }
    }

    public void PurchaseItem(int buttonNumber)
    {
        if (coins >= _shopItemSOCollection[buttonNumber].baseCost)
        {
            coins -= _shopItemSOCollection[buttonNumber].baseCost;
            _shopItemSOCollection[buttonNumber].hasBeenBought = true;
            _myPurchaseBtns[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
            coinUI.text = "Coins: " + coins.ToString();
            CheckPurchaseable();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Assert(_shopHitboxGO!= null, "Hitbox of shopchecker is null in ShopManager Object");

        //var hitboxScript = _shopHitboxGO.GetComponent<ShopCollisionChecker>();

        //Debug.Assert(hitboxScript != null, "hitboxScript is null in ShopManager Object");

        //if ()

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            _functionalityActive = !_functionalityActive;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var gameObjectToCheck = gameObject.transform.GetChild(i).gameObject;
                if (gameObjectToCheck != _moneyCountGO)
                    gameObjectToCheck.SetActive(_functionalityActive);
            }
        }
    }

    public void ToggleCanvas()
    {
        _functionalityActive = !_functionalityActive;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var gameObjectToCheck = gameObject.transform.GetChild(i).gameObject;
            if (gameObjectToCheck != _moneyCountGO)
                gameObjectToCheck.SetActive(_functionalityActive);
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < _shopItemSOCollection.Length; i++)
        {
            _shopPanels[i].titleTxt.text = _shopItemSOCollection[i].title;
            _shopPanels[i].descriptionTxt.text = _shopItemSOCollection[i].description;
            _shopPanels[i].costTxt.text = "Coins: " + _shopItemSOCollection[i].baseCost.ToString();
        }
    }
    




    public void TestBuyOne()
    {
        Debug.Log(_roadsList.Count.ToString());
        ActivateFancyRoads();
    }

    public void TestBuyTwo()
    {
        Debug.Log(_treesList.Count.ToString());
        ActivateTrees();
    }


    private void ActivateTrees()
    {
        foreach(GameObject tree in _treesList)
        {
            tree.SetActive(true);
            Debug.Log("TreeSetActive");
        }
    }

    private void ActivateFancyRoads()
    {
        foreach (GameObject road in _roadsList)
        {
           road.GetComponentInChildren<Renderer>().material = _roadMaterial;


            road.SetActive(true);
        }
    }


    public static void AddTree(GameObject tree)
    {
        if (_treesList.Contains(tree) != true)
        {
            _treesList.Add(tree);
        }
        else
        {
            Debug.Log("Tree already exists in TreeList");
        }
    }

    public static void AddRoad(GameObject road)
    {
        if (_roadsList.Contains(road) != true)
        {
            _roadsList.Add(road);
        }
        else
        {
            Debug.Log("Tree already exists in TreeList");
        }
    }


    public void RemoveRock()
    {
        if (_rockToRemove != null)
        {
            _rockToRemove.gameObject.SetActive(false);
        }
    }
    public void IncreaseStallTimer()
    {
        _stallTimerIncrease += 2f;
    }


    public void SpeedUpPlayer()
    {
        var PCBehaviour = _playerGO.GetComponent<PlayerCharacter>();

        Debug.Assert(PCBehaviour != null, "PlayerChar is null in ShopManager.SpeedUpPlayer()");

        PCBehaviour.increaseMovementSpeed(2f);
    }
}
