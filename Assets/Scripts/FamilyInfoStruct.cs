using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FamilyInfoStruct //has to be a class to be serializable
{
    public FamilyInfoStruct(string familyName, Color familyColor, int familySpeed)
    {
        _familyName= familyName;
        _familyColor= familyColor;
        _familySpeed = familySpeed;
    }
    public string _familyName;
    public  Color _familyColor;
    public int _familySpeed;
}


