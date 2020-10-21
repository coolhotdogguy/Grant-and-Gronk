using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] forestRabbitSprites;
    [SerializeField] SpriteRenderer[] iceRabbitSprites;
    [SerializeField] SpriteRenderer[] dryRabbitSprites;

    bool forestRabbits;
    bool iceRabbits;
    bool dryRabbits;

    void Start()
    {
        HandleRabbitVisibility(FindObjectOfType<TilemapSwapper2>().currentPlanetType);
    }

    public void HandleRabbitVisibility(int levelType)
    {
        switch(levelType)
        {
            case 0:
                forestRabbits = true;
                iceRabbits = false;
                dryRabbits = false;
                break;
            case 1:
                forestRabbits = false;
                iceRabbits = true;
                dryRabbits = false;
                break;
            case 2:
                forestRabbits = false;
                iceRabbits = false;
                dryRabbits = true;
                break;
        }

        for (int i = 0; i < forestRabbitSprites.Length; i++)
        {
            forestRabbitSprites[i].enabled = forestRabbits;
        }
        for (int i = 0; i < iceRabbitSprites.Length; i++)
        {
            iceRabbitSprites[i].enabled = iceRabbits;
        }
        for (int i = 0; i < dryRabbitSprites.Length; i++)
        {
            dryRabbitSprites[i].enabled = dryRabbits;
        }
    }
}
