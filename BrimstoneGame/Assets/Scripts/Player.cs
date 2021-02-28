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
	
	
	private Rigidbody2D _rigidbody;
	
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

		if (touchingWall) {
			grounded = false;
			_rigidbody.gravityScale = 0;
			_rigidbody.position += new Vector2(movement,-1/2) * Time.deltaTime * MovementSpeed;
		}
        if (touchingWall2)
        {
			grounded = false;
			_rigidbody.gravityScale = 0;
			_rigidbody.position += new Vector2(movement, -1 / 2) * Time.deltaTime * MovementSpeed; 
        }
		if(!touchingWall) {
			_rigidbody.gravityScale = 2;
		}
		if (!touchingWall2)
		{
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
		//grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		//touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, whatIsWall);
		
		//if(touchingWall) {
		//	grounded = false;
		//}
		
        //var movement = Input.GetAxis("Horizontal");
		//transform.position += new Vector3(movement,0,0) * Time.deltaTime * MovementSpeed; //Used to move with arrow keys
		
		if(Input.GetButtonDown("Jump") && grounded) {//Mathf.Abs(_rigidbody.velocity.y) < .001f) { //Makes sure jump(space bar) only works when you're on the ground
			_rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse); //Implulse Force for jumping (Not sure about other types of forces)
		}
		
		if(Input.GetButtonDown("Jump") && touchingWall) {
			WallJump();
		}

		if (Input.GetButtonDown("Jump") && touchingWall2)
		{
			WallJump();

			// animation code start 
		}
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			animator.SetBool("movingRight", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
			animator.SetBool("movingRight", false);
        }
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			animator.SetBool("movingLeft", true);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			animator.SetBool("movingLeft", false);
		}
			//animation code end

	}
	
	void WallJump() {
		_rigidbody.AddForce(new Vector2(-WallJumpForce, JumpForce), ForceMode2D.Impulse);
	}
}
