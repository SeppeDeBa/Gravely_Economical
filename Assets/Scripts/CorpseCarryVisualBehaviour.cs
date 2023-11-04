using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CorpseCarryVisualBehaviour : MonoBehaviour
{
    [SerializeField]
    CorpseInventory _ownerCorpseInventory;
    [SerializeField]
    GameObject _objectToChangeActiveState;
    [SerializeField]
    TextMesh _textMeshToChange;
    private void Start()
    {
        Debug.Assert(_ownerCorpseInventory != null, "CorpseCarryBehaviour owner corpse is not properly initialised");
        Debug.Assert(_objectToChangeActiveState != null, "CorpseCarryBehaviour _objectToChangeActiveState is not properly initialised");
        Debug.Assert(_textMeshToChange != null, "CorpseCarryBehaviour _textMeshToChange is not properly initialised");
    }

    private void Update()
    {
        if (_ownerCorpseInventory.IsCarryingDirty())
        {
            _objectToChangeActiveState.SetActive(_ownerCorpseInventory._holdingCorpse);
            _textMeshToChange.text = _ownerCorpseInventory.GetCorpseName();
            Color tempColor = _ownerCorpseInventory.GetColor();
            tempColor.a = 1;
            _textMeshToChange.color = tempColor; 
            
        }

    }
}
