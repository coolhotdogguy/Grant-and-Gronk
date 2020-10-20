using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxDistance;

    Vector2 offset;

    private void Update()
    {
        if(Vector2.Distance(Camera.main.transform.position, player.transform.position) > maxDistance)
        {
            offset = ((Vector2)Camera.main.transform.position - (Vector2)player.transform.position).normalized * maxDistance;
            Vector2 targetPos = (Vector2)player.transform.position + offset;
            Camera.main.transform.position = new Vector3(targetPos.x, targetPos.y, Camera.main.transform.position.z);
        }
    }
}
