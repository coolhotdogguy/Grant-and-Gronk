using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalCoagulate : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerController>().AddTemporalCoagulate();
        Destroy(gameObject);
    }
}
