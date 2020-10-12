using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{

    bool forestLevel = true;
    bool iceLevel;

    [SerializeField] GameObject forest;
    [SerializeField] GameObject ice;
    [SerializeField] GameObject forestBG;
    [SerializeField] GameObject iceBG;
    [SerializeField] SpriteRenderer water;

    void OnCycleLevel()
    {
        forestLevel = !forestLevel;
        iceLevel = !iceLevel;

        forest.SetActive(forestLevel);
        ice.SetActive(iceLevel);

        forestBG.SetActive(forestLevel);
        iceBG.SetActive(iceLevel);

        if (forestLevel)
        {
            water.color = Color.blue;
        }
        else if (iceLevel)
        {
            water.color = Color.white;
        }
    }
}
