using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt; //=> is an interactionbuddy (for personal references)

    public bool Interact(InteractorBehaviour interactor)
    {
        Debug.Log("Interacting with grave!");
        return true;
    }
}
