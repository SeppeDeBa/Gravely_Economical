using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Think abt singleton usage
public class FamilyListScript : MonoBehaviour //TODO: Make singletons an inheritance
{
    #region SINGLETON INSTANCE

    //variables
    public static bool ApplicationQuitting = false;
   
    private static FamilyListScript _instance;

    [SerializeField]
    static List<FamilyInfoStruct> _familyInfoStructs = new List<FamilyInfoStruct>();

    public static FamilyListScript Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<FamilyListScript>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_FamilyListScript");
                    _instance = newInstance.AddComponent<FamilyListScript>();
                }
            }
            return _instance;
        }
    }

    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
            InstantiateFamilies();


        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    #endregion SINGLETON INSTANCE



    public static FamilyInfoStruct GetRandomFamilyInfoStruct()
    {
        return _familyInfoStructs[Random.Range(0, _familyInfoStructs.Count)];
    }

    //ease of use function
    private static void AddFamily(string familyName, Color familyColor, int familySpeed)
    {
        FamilyInfoStruct familyStructBuffer = new FamilyInfoStruct(familyName, familyColor, familySpeed);
        _familyInfoStructs.Add(familyStructBuffer);
    }


    private void InstantiateFamilies()
    {
        AddFamily("MacGees", Color.red, 1);
        AddFamily("Jimbobs", Color.blue, 3);
        AddFamily("Chimichangas", Color.magenta, 5);
    }
}
