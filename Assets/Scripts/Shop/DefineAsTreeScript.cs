using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineAsTreeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShopManager.AddTree(gameObject);
        gameObject.SetActive(false);
    }
}
