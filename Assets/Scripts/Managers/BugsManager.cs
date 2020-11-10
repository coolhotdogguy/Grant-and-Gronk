using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsManager : MonoBehaviour
{
    [SerializeField] GameObject[] bugs;

    private void Start()
    {
        for (int i = 0; i < bugs.Length; i++)
        {
            bugs[i].SetActive(FindObjectOfType<PlayerData>().enableBugs);
        }
    }

    public void SquashBugs(bool squashed)
    {
        for (int i = 0; i < bugs.Length; i++)
        {
            bugs[i].SetActive(squashed);
        }
    }
}
