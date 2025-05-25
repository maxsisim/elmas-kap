using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Takip edilecek karakter
    public float smoothSpeed = 0.125f; // Kamera ge�i� h�z�
    public Vector3 offset; // Kamera ile karakter aras�ndaki mesafe

    void Start()
    {
        // Kamera ba�lang�� pozisyonunu karakterin pozisyonuna ayarla
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }

    void Update()
    {
        // Kameray� hem sa�/sol hem de yukar�/a�a�� y�n�nde takip et
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Kameray� yumu�ak bir �ekilde karakterin konumuna do�ru kayd�r
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kameray� yeni pozisyona ayarla
        transform.position = smoothedPosition;
    }
}
