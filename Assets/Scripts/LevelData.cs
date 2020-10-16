using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelData : MonoBehaviour
{
    public List<int> collectedTemporalCoagulateIDs;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }


    public void AddCollectIDs(int i)
    {
        collectedTemporalCoagulateIDs.Add(i);
    }
    //dont destroy unless one already exists
    //store all TC IDs in local varibale
    //when tc is collected add to tc to remove 
    //when scene is loaded back, put in remove collected


    //remove tc that was already collected
    //get instance ids from destroyed
    //if destroyed, destroy when called

}
