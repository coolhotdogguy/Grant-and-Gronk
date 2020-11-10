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

    PlanetType previousPlanetType;
    PlanetType upcomingPlanetType;
    [HideInInspector] public PlanetType currentPlanetType; //used for BGs

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
                // I think this can be done in a better way
                case PlanetType.Forest: //forest type
                    if (previousPlanetType == PlanetType.Ice) //forest and ice
                    {
                        for (int i = 0; i < iceTiles.Length; i++)
                        {
                            tileMap.SwapTile(iceTiles[i], forestTiles[i]);
                        }
                    }
                    if (previousPlanetType == PlanetType.Desert) //forest and dry
                    {
                        for (int i = 0; i < dryTiles.Length; i++)
                        {
                            tileMap.SwapTile(dryTiles[i], forestTiles[i]);
                        }
                    }
                    levelObjects.SetForestPlanet();
                    icePlanet = false;
                    upcomingPlanetType = PlanetType.Forest;
                    break;

                case PlanetType.Ice:
                    if (previousPlanetType == PlanetType.Forest)
                    {
                        for (int i = 0; i < forestTiles.Length; i++)
                        {
                            tileMap.SwapTile(forestTiles[i], iceTiles[i]);
                        }
                    }
                    if (previousPlanetType == PlanetType.Desert)
                    {
                        for (int i = 0; i < dryTiles.Length; i++)
                        {
                            tileMap.SwapTile(dryTiles[i], iceTiles[i]);
                        }
                    }
                    levelObjects.SetIcePlanet();
                    icePlanet = true;
                    upcomingPlanetType = PlanetType.Ice;
                    break;

                case PlanetType.Desert:
                    if (previousPlanetType == PlanetType.Forest)
                    {
                        for (int i = 0; i < forestTiles.Length; i++)
                        {
                            tileMap.SwapTile(forestTiles[i], dryTiles[i]);
                        }
                    }
                    if (previousPlanetType == PlanetType.Ice)
                    {
                        for (int i = 0; i < iceTiles.Length; i++)
                        {
                            tileMap.SwapTile(iceTiles[i], dryTiles[i]);
                        }
                    }
                    levelObjects.SetDryPlanet();
                    icePlanet = false;
                    upcomingPlanetType = PlanetType.Desert;
                    break;

                default:
                    break;
            }
        }

        previousPlanetType = upcomingPlanetType;
    }

    public void SetPlanetTypeInt(PlanetType upcomingType)
    {
        upcomingPlanetType = upcomingType;
        currentPlanetType = upcomingType; //used for camera BGs
    }
}
