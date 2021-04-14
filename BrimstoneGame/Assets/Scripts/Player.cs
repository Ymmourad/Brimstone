using System.Collections;          //I don't know what these are specifically, but they were already here when I created a new script in Unity 
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { //Not sure what MonoBehavior is but it was here when I created the script
	public float MovementSpeed = 1; //These floats are variables that actually appear within Unity, you can easily change them when you are in Unity
	public float JumpForce = 1;
	public float WallJumpForce = 1;
	bool grounded = false;
	bool touchingWall = false;
	bool touchingWall2 = false; 
	public Transform groundCheck;
	public Transform wallCheck;
	float groundRadius = 0.1f;
	float wallTouchRadius = 0.1f;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public Transform wallCheck2;
	public Animator animator;
	public bool hasJumped = false;	
	private Rigidbody2D _rigidbody;
	float horizontalMovement = 0f;
	private bool extraLife = false; //this variabe checks if the player collects the heart
	private bool hasKey = false; // checks for key 

	// Start is called before the first frame update
	private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>(); //need to this interact with stuff using physics engine(jumping etc, not the same as collision)
	 
	}

    // Update is called once per frame
	private void FixedUpdate() {
		var movement = Input.GetAxis("Horizontal");
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, whatIsWall);
		touchingWall2 = Physics2D.OverlapCircle(wallCheck2.position, wallTouchRadius, whatIsWall);

		// this block of code will check if the player is moving right or left and scale x according to it
		if(movement < 0)
        {
			transform.localScale = new Vector3((float)-1.8,(float) 1.8, 0);
        }
        if(movement > 0)
        {
			transform.localScale = new Vector3((float)1.8, (float)1.8, 0);
        }
		

		if(touchingWall || touchingWall2) {
			grounded = false;
			_rigidbody.gravityScale = 0;
			//_rigidbody.position += new Vector2(movement,-1/2) * Time.deltaTime * MovementSpeed;
			Vector2 v = _rigidbody.velocity;
 			v.y = -1.5F;
 			_rigidbody.velocity = v;

		}
       
		if(!touchingWall && !touchingWall2) {
			_rigidbody.gravityScale = 2;

		}
		

		


		//_rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),_rigidbody.velocity.y);
		_rigidbody.position += new Vector2(movement,0) * Time.deltaTime * MovementSpeed;
		
		//if(!grounded) {
			//_rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal")/2,_rigidbody.velocity.y);
			//_rigidbody.position += new Vector2(movement,0) * Time.deltaTime * MovementSpeed;
	
			//_rigidbody.position += new Vector2(movement,0) * Time.deltaTime * MovementSpeed/2;
		//}
	}
	
    private void Update() {



		//animation for moving right and left
		horizontalMovement = Input.GetAxisRaw("Horizontal");
		animator.SetFloat("speed", Mathf.Abs(horizontalMovement));

       

		//grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		//touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, whatIsWall);

		//if(touchingWall) {
		//	grounded = false;
		//}

		//var movement = Input.GetAxis("Horizontal");
		//transform.position += new Vector3(movement,0,0) * Time.deltaTime * MovementSpeed; //Used to move with arrow keys
		if (grounded) {
			hasJumped = false;
			
		}
        
		
		if(Input.GetButtonDown("Jump") && grounded) {//Mathf.Abs(_rigidbody.velocity.y) < .001f) { //Makes sure jump(space bar) only works when you're on the ground
			
			_rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse); //Implulse Force for jumping (Not sure about other types of forces)
			animator.SetBool("isJumping", true);
		}
		
		if(Input.GetButtonDown("Jump") && touchingWall && hasJumped == false) {
			WallJump();
			hasJumped = true;
			
		}

		if (Input.GetButtonDown("Jump") && touchingWall2 && hasJumped == false)
		{
			WallJump2();
			hasJumped = true;
			
		}
        

	}
	
	void WallJump() {
		_rigidbody.velocity = new Vector2(5f,5f);
		_rigidbody.AddForce(new Vector2(-WallJumpForce, JumpForce), ForceMode2D.Impulse);
	}
	
	void WallJump2() {
		_rigidbody.velocity = new Vector2(5f,5f);
		_rigidbody.AddForce(new Vector2(WallJumpForce, JumpForce), ForceMode2D.Impulse);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("apple"))
        {
			JumpForce += 10;
			Destroy(collision.gameObject);
			
        }
        if (collision.CompareTag("heart"))
        {
			extraLife = true; 
			Destroy(collision.gameObject);
        }
        if (collision.CompareTag("key"))
        {
			hasKey = true;
			Destroy(collision.gameObject);
        }

        if (collision.CompareTag("chest"))
        {
            if (hasKey)
            {
				Destroy(collision.gameObject);
            }
        }

    }
}
