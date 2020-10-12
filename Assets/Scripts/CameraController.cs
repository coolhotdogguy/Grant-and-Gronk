using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float camSpeed = 3f;
    [SerializeField] Vector2 followOffset;
    Vector2 boundaryBox;
    Rigidbody2D rb2d;

    private void Start()
    {
        boundaryBox = CalculateBoundaryBox();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 player = this.player.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * player.x); //Player's distance to center of x axis
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * player.y);//Player's distance to center of y axis

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= boundaryBox.x)
        {
            newPosition.x = player.x;
        }
        if (Mathf.Abs(yDifference) >= boundaryBox.y)
        {
            newPosition.y = player.y;
        }

        float moveSpeed;
        if (rb2d.velocity.magnitude > camSpeed)
        {
            moveSpeed = rb2d.velocity.magnitude;
        }
        else
        {
            moveSpeed = camSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 CalculateBoundaryBox()
    {
        Rect camAspectRatio = Camera.main.pixelRect;
        Vector2 boundary = new Vector2(Camera.main.orthographicSize * camAspectRatio.width / camAspectRatio.height, Camera.main.orthographicSize);
        boundary.x -= followOffset.x;
        boundary.y -= followOffset.y;

        return boundary;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boundaryBox = CalculateBoundaryBox();
        Gizmos.DrawWireCube(transform.position, new Vector3(boundaryBox.x * 2, boundaryBox.y * 2, 1));
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
