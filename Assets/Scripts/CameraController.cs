using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxDistance;
    [SerializeField] float cameraSpeed;
    Rigidbody2D rb2d;

    Vector2 offset;

    private void Start()
    {
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Vector2.Distance(Camera.main.transform.position, player.transform.position) > maxDistance)
        {
            offset = ((Vector2)Camera.main.transform.position - (Vector2)player.transform.position).normalized * maxDistance;
            Vector2 targetPos = (Vector2)player.transform.position + offset;
            Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, Camera.main.transform.position.z);
        }
    }


    /*
    [SerializeField] Transform player;
    [SerializeField] float camTimeOffset;
    [SerializeField] Vector2 camPositionOffset;

    [Header("Boundary Box")]
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;
    [SerializeField] float bottomLimit;

    void Update()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.transform.position;
        endPosition.x += camPositionOffset.x;
        endPosition.y += camPositionOffset.y;
        endPosition.z = -10f;

        transform.position = Vector3.Lerp(startPosition, endPosition, camTimeOffset * Time.deltaTime);

        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z

            );

    } */
}
