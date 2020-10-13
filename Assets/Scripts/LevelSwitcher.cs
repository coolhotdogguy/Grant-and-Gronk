using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{

    bool forestBool = true;
    bool iceBool;
    bool dryBool;

    [SerializeField] GameObject forestTiles;
    [SerializeField] GameObject iceTiles;
    [SerializeField] GameObject dryTiles;
    [SerializeField] GameObject forestBG;
    [SerializeField] GameObject iceBG;
    [SerializeField] GameObject dryBG;

    [SerializeField] GameObject forestRabbit;
    [SerializeField] GameObject iceRabbit;
    [SerializeField] GameObject dryRabbit;

    int counter = 0;

    private void Start()
    {

    }

    void OnCycleLevel()
    {
        counter++;

        if (counter > 2)
        {
            counter = 0;
        }

        if (counter == 0)
        {
            forestBool = true;
            iceBool = false;
            dryBool = false;
        }
        if (counter == 1)
        {
            iceBool = true;
            forestBool = false;
            dryBool = false;
        }
        if (counter == 2)
        {
            dryBool = true;
            forestBool = false;
            iceBool = false;
        }


        forestTiles.SetActive(forestBool);
        iceTiles.SetActive(iceBool);
        dryTiles.SetActive(dryBool);

        forestBG.SetActive(forestBool);
        iceBG.SetActive(iceBool);
        dryBG.SetActive(dryBool);

        forestRabbit.gameObject.GetComponent<SpriteRenderer>().enabled = forestBool;
        iceRabbit.gameObject.GetComponent<SpriteRenderer>().enabled = iceBool;
        dryRabbit.gameObject.GetComponent<SpriteRenderer>().enabled = dryBool;

    }

}
