using UnityEngine;
using UnityEngine.UI;

public class JumpController : MonoBehaviour
{
    public Button jumpButton; // UI butonu
    public Rigidbody2D rb2d; // Player Rigidbody2D
    public Animator anim; // Player Animator
    public float jumpForce = 10f;
    private bool isGrounded;

    void Start()
    {
        // Jump butonuna tıklanınca Jump fonksiyonunu çalıştır
        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        // Yerde olup olmadığını kontrol et (basit bir yöntem)
        isGrounded = rb2d.linearVelocity.y == 0;

        // Eğer yere değiyorsa animasyon trigger'ı sıfırla
        if (isGrounded && anim != null)
        {
            anim.ResetTrigger("isJump");
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);

            if (anim != null)
            {
                anim.SetTrigger("isJump");
            }
            else
            {
                Debug.LogWarning("Animator atanmadı!");
            }
        }
    }
}
