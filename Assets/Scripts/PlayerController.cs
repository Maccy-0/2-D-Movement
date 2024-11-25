using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float acceleration;
    public Rigidbody2D player;
    float y1;
    float y2;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal") * acceleration, 0);
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        player.velocity = playerInput * playerSpeed;
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
        return Physics2D.Raycast(transform.position + new Vector3(0,-0.6f), Vector3.down, 0.1f);
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
}
