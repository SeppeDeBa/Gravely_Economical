using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
    public bool hasBeenBought = false;

    private void OnDisable()//reset since it's a scriptable object
    {
        hasBeenBought = false;
    }

}
