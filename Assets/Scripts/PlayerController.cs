using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float acceleration;
    public Rigidbody2D player;
    float y1;
    float y2;
    public float apexHeight;
    public float apexTime;
    bool isGrounded;
    bool isGroundedC;
    public float terminalSpeed;
    public float coyoteTime;
    float coyoteTimeSpent;
    public float gravity;
    bool dying;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        dying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal") * acceleration, 0);
        MovementUpdate(playerInput);

        
        if (Input.GetKeyDown("x") && isGroundedC)
        {
            dying = true;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        player.velocity = playerInput * playerSpeed * Time.deltaTime;
        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.6f), Vector3.down, 0.1f))
        {
            coyoteTimeSpent = coyoteTime;
            isGroundedC = true;
        }
        else
        {
            coyoteTimeSpent -= Time.deltaTime;
            if (coyoteTimeSpent < 0)
            {
                isGroundedC = false;
            }
        }
        Debug.Log(coyoteTimeSpent);
        if (Input.GetKeyDown("w") && isGroundedC)
        {
            StartCoroutine("Jump");
        }

        if (player.velocity.y < -terminalSpeed)
        {
            player.velocity = new Vector2(player.velocity.x, -terminalSpeed);
        }
        Debug.Log(player.velocity);
    }
    private IEnumerator Jump ()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        float i = 0;
        while (i < apexTime){
            player.velocity += new Vector2(0, apexHeight/apexTime);
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;

        }
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        yield return null;
    }
    public bool IsWalking()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position + new Vector3(0,-0.6f), Vector3.down, 0.1f);
        return isGrounded;
    }

    public FacingDirection GetFacingDirection()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            return FacingDirection.right;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            return FacingDirection.left;
        }
        //I wish this could be null
        return FacingDirection.right;
    }

    public bool IsDying()
    {
        if (dying)
        {
            OnDyingAnimationComplete();
            return true;
            
        }
        else
        {
            return false;
        }
    }

    void OnDyingAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
