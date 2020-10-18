using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSwapper2 : MonoBehaviour
{
    [SerializeField] TileBase[] forestTiles;
    [SerializeField] TileBase[] iceTiles;
    [SerializeField] TileBase[] dryTiles;

    Tilemap tileMap;
    LevelObjects levelObjects;

    int previousPlanetType;
    int upcomingPlanetType;

    [HideInInspector] public bool icePlanet;

    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
        levelObjects = FindObjectOfType<LevelObjects>();

    }

    public void UpdateTilesAndObjects()
    {
        if (upcomingPlanetType != previousPlanetType)
        {
            switch (upcomingPlanetType)
            {
                case 0: //forest type
                    if (previousPlanetType == 1) //forest and ice
                    {
                        for (int i = 0; i < iceTiles.Length; i++)
                        {
                            tileMap.SwapTile(iceTiles[i], forestTiles[i]);
                        }
                    }
                    if (previousPlanetType == 2) //forest and dry
                    {
                        for (int i = 0; i < dryTiles.Length; i++)
                        {
                            tileMap.SwapTile(dryTiles[i], forestTiles[i]);
                        }
                    }
                    levelObjects.SetForestPlanet();
                    break;

                case 1:
                    if (previousPlanetType == 0)
                    {
                        for (int i = 0; i < forestTiles.Length; i++)
                        {
                            tileMap.SwapTile(forestTiles[i], iceTiles[i]);
                        }
                    }
                    if (previousPlanetType == 2)
                    {
                        for (int i = 0; i < dryTiles.Length; i++)
                        {
                            tileMap.SwapTile(dryTiles[i], iceTiles[i]);
                        }
                    }
                    levelObjects.SetIcePlanet();
                    icePlanet = true;
                    break;

                case 2:
                    if (previousPlanetType == 0)
                    {
                        for (int i = 0; i < forestTiles.Length; i++)
                        {
                            tileMap.SwapTile(forestTiles[i], dryTiles[i]);
                        }
                    }
                    if (previousPlanetType == 1)
                    {
                        for (int i = 0; i < iceTiles.Length; i++)
                        {
                            tileMap.SwapTile(iceTiles[i], dryTiles[i]);
                        }
                    }
                    levelObjects.SetDryPlanet();
                    break;

                default:
                    break;
            }
        }

        previousPlanetType = upcomingPlanetType;
    }

    public void SetPlanetTypeInt(int upcomingType)
    {
        upcomingPlanetType = upcomingType;
    }

}
