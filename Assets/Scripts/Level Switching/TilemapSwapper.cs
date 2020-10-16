using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TilemapSwapper : MonoBehaviour
{

    [SerializeField] TileBase[] forestTiles;
    [SerializeField] TileBase[] iceTiles;
    [SerializeField] TileBase[] dryTiles;
    TileBase[] currentTiles;

    Tilemap currentTileMap;
    int counter;
    public bool stable = true;

    void  Start()
    {
        currentTileMap = GetComponent<Tilemap>();
        currentTiles = forestTiles;
    }


    void OnQInput(InputValue value)
    {
        if (value.isPressed && stable)
        {
            counter++;
            if (counter > 2) {counter = 0;}

            if (counter == 0)//forest
            {
                for (int i = 0; i < dryTiles.Length; i++)
                {
                    currentTileMap.SwapTile(dryTiles[i], forestTiles[i]);
                }
                currentTiles = forestTiles;
            }
            else if (counter == 1)//ice
            {
                for (int i = 0; i < forestTiles.Length; i++)
                {
                    currentTileMap.SwapTile(forestTiles[i], iceTiles[i]);
                }
                currentTiles = iceTiles;
            }
            else if (counter == 2)//dry
            {
                for (int i = 0; i < iceTiles.Length; i++)
                {
                    currentTileMap.SwapTile(iceTiles[i], dryTiles[i]);
                }
                currentTiles = dryTiles;
            }
        }
       /* else if(value.isPressed && !stable) //is unstable, randomly assign tiles
        {
            for (int i = 0; i < forestTiles.Length; i++)
            { int rand = Random.Range(0, 3);
                if (rand == 0)//forest
                {
                    Debug.Log("forest");
                    if (currentTiles[i] != forestTiles[i])
                    {
                        currentTileMap.SwapTile(currentTiles[i], forestTiles[i]);
                    }
                    else
                    {
                        currentTileMap.SwapTile(currentTiles[i], iceTiles[i]);
                    }
                }
                if (rand == 1)//ice
                {
                    Debug.Log("ice");
                    if (currentTiles[i] != iceTiles[i])
                    {
                        currentTileMap.SwapTile(currentTiles[i], iceTiles[i]);
                    }
                    else
                    {
                        currentTileMap.SwapTile(currentTiles[i], dryTiles[i]);
                    }
                }
                if (rand == 2)//dry
                {
                    Debug.Log("dry");
                    if (currentTiles[i] != dryTiles[i])
                    {
                        currentTileMap.SwapTile(currentTiles[i], dryTiles[i]);
                    }
                    else
                    {
                        currentTileMap.SwapTile(currentTiles[i], forestTiles[i]);
                    }
                }

               
            }
        } */


    }


}

