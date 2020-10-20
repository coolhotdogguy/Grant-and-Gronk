using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] float fallSpeed = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().state = PlayerController.AnimationState.falling;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -fallSpeed);
            StartCoroutine(EnablePlayerColliderCoroutine(collision.GetComponent<Collider2D>()));
        }

    }

    IEnumerator EnablePlayerColliderCoroutine(Collider2D playerColider)
    {
        yield return new WaitForSeconds(0.2f);
        playerColider.enabled = true;
    }
}
