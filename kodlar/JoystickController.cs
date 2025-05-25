using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickStick;
    public GameObject player;
    public float moveSpeed = 5f;

    public Button attackButton;
    public Button dashButton;
    public Button jumpButton;
    public Button openChestButton;

    private Vector2 startPos;
    private float joystickRadius;
    private float moveHorizontal;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Dash
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private float dashTime;
    public AudioClip dashSound; // Dash sesi

    // Jump
    public float jumpForce = 10f;
    private bool isGrounded = true;
    public AudioClip jumpSound; // Zıplama sesi
    private AudioSource audioSource; // AudioSource bileşeni

    // Attack
    private bool isAttacking = false;

    void Start()
    {
        startPos = joystickBackground.position;
        joystickRadius = joystickBackground.rect.width / 2;
        rb2d = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        audioSource = player.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = player.AddComponent<AudioSource>();
        }

        attackButton.onClick.AddListener(Attack);
        dashButton.onClick.AddListener(Dash);
        jumpButton.onClick.AddListener(Jump);
        openChestButton.onClick.AddListener(OpenChest);
    }

    void Update()
    {
        if (!isDashing && !isAttacking)
        {
            CharacterMovement();
        }

        CharacterAnimation();

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }

        isGrounded = Mathf.Abs(rb2d.linearVelocity.y) < 0.01f;
    }

    void CharacterMovement()
    {
        rb2d.linearVelocity = new Vector2(moveHorizontal * moveSpeed, rb2d.linearVelocity.y);
    }

    void CharacterAnimation()
    {
        anim.SetBool("isRunning", moveHorizontal != 0 && !isAttacking);
        anim.SetBool("isGrounded", isGrounded);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position - startPos;
        position.x = Mathf.Clamp(position.x, -joystickRadius, joystickRadius);
        joystickStick.position = startPos + new Vector2(position.x, 0);

        moveHorizontal = (position.x / joystickRadius) * -1;

        if (moveHorizontal != 0)
        {
            spriteRenderer.flipX = moveHorizontal > 0;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystickStick.position = startPos;
        moveHorizontal = 0f;
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("isAttack");
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            float direction = spriteRenderer.flipX ? 1 : -1;
            rb2d.linearVelocity = new Vector2(direction * dashSpeed, rb2d.linearVelocity.y);
            anim.SetTrigger("Dash");

            // Dash sesi çal
            if (audioSource != null && dashSound != null)
            {
                audioSource.PlayOneShot(dashSound);
            }
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            anim.SetTrigger("isJump");

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    public void OpenChest()
    {
        anim.SetTrigger("OpenChest");
    }
}
