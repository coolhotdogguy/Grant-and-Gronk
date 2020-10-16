using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int playerHealth = 3;

    [SerializeField] Image[] healthUnits;

    bool gronkLevel;

    private void Start()
    {
        gronkLevel = FindObjectOfType<PlayerData>().gronkLevel;
    }



    public void DamagePlayer()
    {
        playerHealth--;
    }

    private void Update()
    {
        if (!gronkLevel)
        {
            gronkLevel = FindObjectOfType<PlayerData>().gronkLevel;

            Debug.Log(gronkLevel);

            if (playerHealth == 3)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = true;
            }
            if (playerHealth == 2)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = false;
            }
            if (playerHealth == 1)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
            if (playerHealth <= 0)
            {
                healthUnits[0].enabled = false;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
        }
        else
        {
            healthUnits[0].enabled = false;
            healthUnits[1].enabled = false;
            healthUnits[2].enabled = false;
        }
    }
}
