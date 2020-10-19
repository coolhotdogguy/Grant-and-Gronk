using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public int collectedTemporalCoagulateInt;
    public bool gronkLevel;
    int playerHealth = 3;
    [SerializeField] Image[] healthUnits;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] GameObject temporalCoagulatesFolder;
    [HideInInspector] public Vector2 playerPosition;
    [SerializeField] GameObject groundGameObject;
    GameObject levelObjectsGameObject;

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        };

        levelObjectsGameObject = FindObjectOfType<LevelObjects>().gameObject;
    }

    private void HandleTemporalCoagulateVisibility()
    {
        if (!gronkLevel)
        {
            temporalCoagulatesFolder.SetActive(true);
        }
        else
        {
            temporalCoagulatesFolder.SetActive(false);
        }
    }

    private void HandleHealthUI()
    {
        if (!gronkLevel)
        {

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

    public void AddToInventoy()
    {
        collectedTemporalCoagulateInt++;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
    }

    public void DamagePlayer()
    {
        playerHealth--;
        HandleHealthUI();
        if (playerHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        TemporalCoagulate[] collectedTemporalCoagulate = FindObjectsOfType<TemporalCoagulate>();
        for (int i = 0; i < collectedTemporalCoagulate.Length; i++)
        {
            collectedTemporalCoagulate[i].gameObject.SetActive(true);
        }

        collectedTemporalCoagulateInt = 0;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();

        playerHealth = 3;
        HandleHealthUI();

        playerPosition = Vector2.zero;
    }

    public void GetGrantPosition(Vector2 position)
    {
        playerPosition = position;
    }

    void OnRInput(InputValue value)
    {
        if (!gronkLevel) //Load Gronk Level
        {
            LoadGronkLevel();

        }
        if (gronkLevel)  //Load Grant Level
        {
            LoadGrantLevel();
        }

        gronkLevel = !gronkLevel;

        HandleHealthUI();
        HandleTemporalCoagulateVisibility();
    }

    private void LoadGrantLevel()
    {
        SceneManager.LoadScene("Grant Level");
        groundGameObject.GetComponent<TilemapRenderer>().enabled = true;
        groundGameObject.GetComponent<TilemapCollider2D>().enabled = true;
        levelObjectsGameObject.SetActive(true);
        groundGameObject.GetComponent<TilemapSwapper2>().UpdateTilesAndObjects();
    }

    private void LoadGronkLevel()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform.position; //store grant position
        groundGameObject.GetComponent<TilemapRenderer>().enabled = false;
        groundGameObject.GetComponent<TilemapCollider2D>().enabled = false;
        levelObjectsGameObject.SetActive(false);

        SceneManager.LoadScene("Gronk Level");
    }
}
