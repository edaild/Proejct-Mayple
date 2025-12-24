using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class characterConteroller : MonoBehaviour
{
    public VariableJoystick joy;

    public float playerSpeed = 5f;
    public float playerJump = 8f;
    public float hightJump = 15f;
    public Rigidbody2D rb;
    public Collider2D cd;
    public Animator animator;
    public Button JumpButton;

    public GameObject testPanel;
    public Button testUI;

    public bool isJump;
    public bool isGround;
    private bool istextUI;
   private void Start()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        JumpButton.onClick.AddListener(Jump);
        testUI.onClick.AddListener(TestUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        charcterMovePC();
    }

    void charcterMovePC()
    {
        float HorizontalInput = (Input.GetAxisRaw("Horizontal") != 0) ? Input.GetAxisRaw("Horizontal") : joy.Horizontal;
        if (HorizontalInput < 0) Flip(true);
        else  Flip(false);

        Vector3 Derection = new Vector3(HorizontalInput, 0, 0).normalized;
        Vector3 move = Derection * playerSpeed * Time.deltaTime;
        transform.position += move;

        if (Derection != Vector3.zero)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
    }

    void Flip(bool shouldFlip)
    {
        Vector3 scale = transform.localScale;

        if (shouldFlip)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    void Jump()
    {
        if (!isJump && isGround)
        { 
            rb.velocity = Vector2.up * playerJump;
            isJump = true;
            isGround = false;
            StartCoroutine(JumpTime());
        }     
    }

    IEnumerator JumpTime()
    {
        cd.enabled = false;
        yield return new WaitForSeconds(1f);
        cd.enabled = true;
    }

    void TestUI()
    {
        if (istextUI)
        {
            testPanel.gameObject.SetActive(false);
            istextUI = false;
        }
        else
        {
            testPanel.gameObject.SetActive(true);
            istextUI = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = true;
            isJump = false;
        }
    }
}
