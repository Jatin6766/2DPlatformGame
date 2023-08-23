using System.Collections;
using UnityEngine;
using static PlayerShoot;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;

    [SerializeField] LayerMask groundLayer;
    
    Animator animator;
    
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    Vector2 playeroffset;
    BoxCollider2D boxcollider2D;
    Vector2 checkBoxSize;

    private void Awake()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        checkBoxSize = boxcollider2D.size * transform.localScale;
        playeroffset = boxcollider2D.offset * (Vector2)transform.localScale;
    }


    private bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position + playeroffset, checkBoxSize, 0, groundLayer);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject) // Ignore self-collision
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        CharacterMOvements();

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerJump);
        }


        if (Input.GetMouseButtonDown(0))
        {
            OnShootBullet?.Invoke(isFacingRight);
            animator.SetTrigger("Attack");
        }
    }

    private void CharacterMOvements()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        if ((isFacingRight && moveDirection < 0) || (!isFacingRight && moveDirection > 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage()
    {
        HUDController.Instance.PlayerLife -= 1;

        if (HUDController.Instance.PlayerLife <= 0)
        {
            animator.SetTrigger("Die");
            //play dead sound and Hide
            StartCoroutine(OnDie());
        }
        else
        {
            animator.SetTrigger("Hit");
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerHit);
        }
    }

    IEnumerator OnDie()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        GamePlayScreenManager.Instance.OnGameOver();
        SoundManager.Instance.PlaySound(SoundManager.Instance.playerDie);
    }
}
