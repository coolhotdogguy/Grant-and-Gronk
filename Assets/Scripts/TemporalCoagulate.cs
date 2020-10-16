using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TemporalCoagulate : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    int instanceID;
    List<int> collected;

    private void Start()
    {
        /*instanceID = gameObject.GetInstanceID();
        if (PlayerPrefs.HasKey(gameObject.GetInstanceID().ToString()))
        {
            Destroy(gameObject);
        }*/

        collected = FindObjectOfType<LevelData>().collectedTemporalCoagulateIDs;
    }

    void Update()
    {   /*
        for (int i = 0; i < collected.Count; i++)
        {
            if (gameObject.GetInstanceID() == collected[i])
            {
                Destroy(gameObject);
                Debug.Log("Destoy?");
            }
        } */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerData.AddToInventoy(this.gameObject);
            FindObjectOfType<LevelData>().AddCollectIDs(gameObject.GetInstanceID());
            //PlayerPrefs.SetInt(gameObject.GetInstanceID().ToString(), gameObject.GetInstanceID());
            Destroy(gameObject);
        }
    }

}
