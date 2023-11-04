using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//player object has Money Component

public class Money : MonoBehaviour
{

    public static int _score;
    private static bool _isDirty;
    [SerializeField] public Text _scoreText;



    static void AddScore(int amtToAdd)
    {
        _score += amtToAdd;
        _isDirty = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isDirty)
        {
            _scoreText.text = _score.ToString(); // essentially doing Update Text
            _isDirty = false;
        }
    }


    private void UpdateText()
    {
        _scoreText.text = _score.ToString();
    }
}
