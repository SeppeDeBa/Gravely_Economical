using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField]
    CorpseInventory _corpseInventory;

    [SerializeField] //serializing for making debugging easier
    List<FamilyInfoStruct> _familyTargets = new List<FamilyInfoStruct>();

    [SerializeField]
    GameObject _npcTargetPoint;

    

    public string InteractionPrompt => _prompt; //=> is an interactionbuddy (for personal references)

    public void Start()
    {
        GraveManager.AddGrave(this);//add self to the grave manager
    }


    public bool Interact(InteractorBehaviour interactor)
    {
        //get player invent, could change by having a public or referenced player invent
        var otherInventory = interactor.GetComponent<CorpseInventory>();

        if (otherInventory == null)
        {
            return false;
        }

        otherInventory.SwapCorpse(_corpseInventory);
        //_corpseInventory.SwapCorpse(otherInventory);

        Debug.Log("Interacting with grave!");
        return true;
    }

    public GameObject GetTargetPoint()
    {
        return _npcTargetPoint;
    }

    public Vector3 GetTargetPointPos()
    {
        return _npcTargetPoint.transform.position;
    }

    public void ResetFamilies()
    {
        _familyTargets.Clear();
    }


    public void AssignFamily(FamilyInfoStruct family)
    {
        _familyTargets.Add(family);
    }

    public bool ContainsFamilyTarget(FamilyInfoStruct familyTarget)
    {
        foreach (var target in _familyTargets)
        {
            if (target._familyName == familyTarget._familyName) return true;
        }
        return false;
    }
}
