using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] GameObject forestBG;
    [SerializeField] GameObject iceBG;
    [SerializeField] GameObject dryBG;

    public void EnableBGs(int planetType) //Whether you're a brother or whether you're a mother, you're stayin' alive, stayin' alive
    {
        if (planetType == 0)
        {
            forestBG.SetActive(true);
            iceBG.SetActive(false);
            dryBG.SetActive(false);
        }
        if (planetType == 1)
        {
            forestBG.SetActive(false);
            iceBG.SetActive(true);
            dryBG.SetActive(false);
        }
        if (planetType == 2)
        {
            forestBG.SetActive(false);
            iceBG.SetActive(false);
            dryBG.SetActive(true);
        }
    }
}
