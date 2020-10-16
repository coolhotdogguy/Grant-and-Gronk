using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int playerHealth = 3;

    [SerializeField] Image[] healthUnits;

    public void DamagePlayer()
    {
        if (playerHealth <= 2)
        {
            healthUnits[0].enabled = false;
        }
        if (playerHealth <= 1)
        {
            healthUnits[1].enabled = false;
        }
        if (playerHealth <= 0)
        {
            healthUnits[2].enabled = false;
        }
    }
}
