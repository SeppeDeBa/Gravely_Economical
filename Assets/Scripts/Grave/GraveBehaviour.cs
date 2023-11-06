using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private GameObject _family1Gravestone;
    [SerializeField] private GameObject _family2Gravestone;
    [SerializeField] private GameObject _family3Gravestone;

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
        FamilyInfoStruct familyScriptBuffer = new FamilyInfoStruct("empty", Color.gray, 0); //need to make a full object so that the default color would be gray
        if (_familyTargets.Count == 0)
        {
            _family1Gravestone.SetActive(false);
            _family2Gravestone.SetActive(false);
            _family3Gravestone.SetActive(false);
        }
        else
        {
            //family stone 1
            if (_familyTargets.Count > 0)
            {
                familyScriptBuffer = FamilyListScript.GetFamilyInfoStruct(_familyTargets[0]._familyName);
            }
            _family1Gravestone.GetComponent<Renderer>().material.SetColor("_BaseColor", familyScriptBuffer._familyColor);

            //family stone 2
            if (_familyTargets.Count > 1)

            {
                familyScriptBuffer = FamilyListScript.GetFamilyInfoStruct(_familyTargets[1]._familyName);
            }
            _family2Gravestone.GetComponent<Renderer>().material.SetColor("_BaseColor", familyScriptBuffer._familyColor);

            //family stone 3
            if (_familyTargets.Count > 2)

            {
                familyScriptBuffer = FamilyListScript.GetFamilyInfoStruct(_familyTargets[2]._familyName);
                _family3Gravestone.GetComponent<Renderer>().material.SetColor("_BaseColor", familyScriptBuffer._familyColor);
            }
            else
            {
                _family3Gravestone.SetActive(false);
            }
        }
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
