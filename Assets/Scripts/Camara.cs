using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Referencia al jugador
    public float distance = 5f;       // Distancia normal
    public float height = 2f;         // Altura de la cámara
    public float smoothSpeed = 10f;   // Suavizado
    public LayerMask collisionMask;   // Capas que bloquean la cámara

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (player == null) return;

        // Posición deseada detrás del jugador
        Vector3 desiredPosition = player.position
                                - player.forward * distance
                                + Vector3.up * height;

        Vector3 direction = (desiredPosition - player.position).normalized;
        float targetDistance = distance;

        RaycastHit hit;

        // Detecta si hay pared entre jugador y cámara
        if (Physics.Linecast(player.position + Vector3.up * height,
                             desiredPosition,
                             out hit,
                             collisionMask))
        {
            // Acerca la cámara al jugador
            targetDistance = hit.distance - 0.2f;
        }

        // Nueva posición corregida
        Vector3 finalPosition = player.position
                              - player.forward * targetDistance
                              + Vector3.up * height;

        // Movimiento suave
        transform.position = Vector3.SmoothDamp(
            transform.position,
            finalPosition,
            ref currentVelocity,
            1f / smoothSpeed
        );

        // Mira al jugador
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}