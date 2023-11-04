using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//TODO: Think abt singleton usage
public class GraveManager : MonoBehaviour //TODO: Make singletons an inheritance
{
    #region SINGLETON INSTANCE

    //variables
    public static bool ApplicationQuitting = false;

    private static GraveManager _instance;
    static List<GraveBehaviour> _graveList = new List<GraveBehaviour>();

    public static GraveManager Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<GraveManager>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_GraveManager");
                    _instance = newInstance.AddComponent<GraveManager>();
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

    public static Vector3 GetFamilyTargetPos(FamilyInfoStruct family)
    {
        foreach (GraveBehaviour grave in _graveList)
        {
            if (grave.ContainsFamilyTarget(family))
            {
                return grave.GetTargetPointPos();
            }
        }
        Vector3 tempVector;
        tempVector = Vector3.zero;
        Debug.Log("No target has been found for one of the families in GetFamilyTargetPos");
        return tempVector;
    }

    public static GameObject GetFamilyTargetGameObject(FamilyInfoStruct family)
    {
        Debug.Log("Attepmting to give family target GO");
        foreach (GraveBehaviour grave in _graveList)
        {
            if (grave.ContainsFamilyTarget(family))
            {
                return grave.GetTargetPoint();
            }
        }
         // tempGO = GameObject.Instantiate(gameObject);
        Debug.Log("No target has been found for one of the families in GetFamilyTargetPos");
        return null;
    }

    //ease of use function
    public static void AddGrave(GraveBehaviour grave)
    {
        if (_graveList.Contains(grave) != true)
        {
            _graveList.Add(grave);
           // Debug.Assert(false, "Grave added");
        }
        else
        {
            Debug.Log("Grave already exists in graveList");
        }
    }
}
