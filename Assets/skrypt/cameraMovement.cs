using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform player;      // gracz
    public Vector3 offset;        // przesuniêcie kamery wzglêdem gracza
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
