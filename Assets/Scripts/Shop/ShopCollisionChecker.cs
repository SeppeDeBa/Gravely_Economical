using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopCollisionChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _shopNeedsToggling;
    ShopManager _shopManager = null;

    private void Start()
    {
        _shopManager = GameObject.Find("Shop").GetComponentInChildren<ShopManager>();
        Debug.Assert(_shopManager != null, "shopmanager is not found in NPCBehaviour");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something has entered collider");
        if (other.gameObject.tag == "Player")
        {
            _shopManager.gameObject.GetComponent<ShopManager>().ToggleCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something has entered collider");
        if (other.gameObject.tag == "Player")
        {
            _shopManager.gameObject.GetComponent<ShopManager>().ToggleCanvas();
        }
    }


}
