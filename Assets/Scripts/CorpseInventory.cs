using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CorpseInventory : MonoBehaviour
{
    private bool _CarryingBehaviourDirtyFlag = false;


    [SerializeField]
    public bool _holdingCorpse = false;

    [SerializeField]
    private string _corpseName = null;

    [SerializeField]
    private string _ownerName = null; //for debugging purposes
    [SerializeField]
    Color _familyColor;

    private void Start()
    {
        //1 time manual check if a grave would start with a held corpse and it has no name, that an assert is thrown;
        bool graveIsValid = true; 
        if (_holdingCorpse == true && _corpseName == null)
            graveIsValid = false;

        Debug.Assert(graveIsValid, "A grave has a corpse with no name on init!");
    }

    private void OnDrawGizmos()
    {
        //debug drawing
        if (_holdingCorpse)
        {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);

        }
    }
    public void SwapCorpse(CorpseInventory otherInventory)
    {
        //check if you can get from other Invent
        if (otherInventory == null)
        {
            Debug.Log("Null reference exception, otherInventory is null");
            return;
        }
        var otherActorInventory = otherInventory;
        string debugString = "Corpse given from ";
        if (!_holdingCorpse && otherActorInventory._holdingCorpse) 
        {
            _holdingCorpse = true;
            _corpseName = otherActorInventory._corpseName;
           // _familyColor = otherActorInventory._familyColor;
            otherActorInventory._holdingCorpse = false;

            //debug
            debugString += otherActorInventory._ownerName;
            debugString += " to ";
            debugString += _ownerName;
            //for corpse carrying behaviour
            _CarryingBehaviourDirtyFlag = true;
        }


        //check if you can give (so function works 2 ways)
        else if (_holdingCorpse && !otherActorInventory._holdingCorpse)
        {
            _holdingCorpse = false;
            otherActorInventory._corpseName = _corpseName;
            //otherActorInventory._familyColor = _familyColor;
            otherActorInventory._holdingCorpse = true;

            //debug
            debugString += _ownerName;
            debugString += " to ";
            debugString += otherActorInventory._ownerName;

            _CarryingBehaviourDirtyFlag = true;
        }

        else
        {
            debugString = "Not a valid swap condition";
        }
      
        Debug.Log(debugString);
        
        
    }

    public bool IsCarryingDirty()
    {
        return _CarryingBehaviourDirtyFlag;
    }

    public string GetCorpseName()
    {
        return _corpseName;
    }
    public Color GetColor()
    {
        return _familyColor;
    }
    public void CleanIsCarryingFlag()
    {
        _CarryingBehaviourDirtyFlag = false;
    }
}
