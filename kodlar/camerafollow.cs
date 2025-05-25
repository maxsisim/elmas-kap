using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Takip edilecek karakter
    public float smoothSpeed = 0.125f; // Kamera geçiþ hýzý
    public Vector3 offset; // Kamera ile karakter arasýndaki mesafe

    void Start()
    {
        // Kamera baþlangýç pozisyonunu karakterin pozisyonuna ayarla
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }

    void Update()
    {
        // Kamerayý hem sað/sol hem de yukarý/aþaðý yönünde takip et
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Kamerayý yumuþak bir þekilde karakterin konumuna doðru kaydýr
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayý yeni pozisyona ayarla
        transform.position = smoothedPosition;
    }
}
