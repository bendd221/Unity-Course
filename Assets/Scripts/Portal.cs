using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] string portalType;
    [SerializeField] string nextLevelName;
    

    public string GetPortalType()
    {
        return  portalType;
    }

    public string GetNextLevelName()
    {
        return  nextLevelName;
    }
}
