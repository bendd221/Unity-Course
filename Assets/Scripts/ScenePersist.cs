using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake() {
        int numScenePersisits = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersisits > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersists()
    {
        Destroy(gameObject);
    }
}
