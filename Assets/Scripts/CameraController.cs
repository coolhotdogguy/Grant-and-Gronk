using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxDistance;

    [HideInInspector] public bool freezeCamera;
    [HideInInspector] public bool winCamera;

    Vector2 offset;

    private void Update()
    {
        if (freezeCamera == true)
        {
            return;
        }

        if (winCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(356.8f, -39.23f, -10), Time.deltaTime/2);
            Camera.main.orthographicSize -= Time.deltaTime/2;
            if (Camera.main.orthographicSize <= 1.5)
            {
                winCamera = false;
                freezeCamera = true;
            }
            return;
        }

        if (Vector2.Distance(Camera.main.transform.position, player.transform.position) > maxDistance)
        {
            offset = ((Vector2)Camera.main.transform.position - (Vector2)player.transform.position).normalized * maxDistance;
            Vector2 targetPos = (Vector2)player.transform.position + offset;
            Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, Camera.main.transform.position.z);
        }
    }

    public void ResetCameraAfterWin()
    {
        Camera.main.orthographicSize = 10.5f;
    }
}
