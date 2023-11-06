using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineAsRoadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShopManager.AddRoad(gameObject);
    }
}
