using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    CorpseInventory _corpseInventory;

    public string InteractionPrompt => _prompt; //=> is an interactionbuddy (for personal references)

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
}
