using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] _shopItemSOCollection;
    public GameObject[] _shopPanelsGO;
    public ShopTemplate[] _shopPanels;
    public Button[] _myPurchaseBtns;

    void Start()
    {
        for (int i = 0; i < _shopItemSOCollection.Length; i++)
        {
            _shopPanelsGO[i].SetActive(true);
        }
        coinUI.text = "Coins: " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();
    }
    public void AddCoins(int coinsAdded = 1)
    {


        coins += coinsAdded;
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < _shopItemSOCollection.Length; i++)
        {
            if (coins >= _shopItemSOCollection[i].baseCost)
                _myPurchaseBtns[i].interactable = true;
            else 
                _myPurchaseBtns[i].interactable = false ;

        }
    }


    // Update is called once per frame
    void Update()
    {
        
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
    


}
