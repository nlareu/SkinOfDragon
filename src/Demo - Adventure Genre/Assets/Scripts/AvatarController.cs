using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public float debugvar = 10f;

    public float AxisSensitive = 0.7f;
    public float CollisionDetectionRadius = 5f;
    public GameObject CollisionDetectorBottom;
    public GameObject CollisionDetectorLeft;
    public GameObject CollisionDetectorRight;
    public GameObject CollisionDetectorTop;
    public LayerMask ItemsLayerMask; 
    //public bool IsJumping = false;
    //public float JumpHeight = 250.0f;
    public int PlayerNumber { get; protected set; }
    public float Speed = 6.0F;
    private AvatarStates state = AvatarStates.Normal;
    public AvatarStates State
    {
        get
        {
            return this.state;
        }
        set
        {
            if (this.state != value)
            {
                this.state = value;

                //Set variables depending new state.
                switch (this.state)
                {
                    #region Normal
                    case AvatarStates.Normal:
                        {
                            //this.spriteRendered.color = Color.white;

                            //this.rigidBody.gravityScale = 1f;

                            ////Enable again collision with other avatars, except with those that are stunned.
                            //AppController
                            //    .GetPlayers()
                            //    .ForEach(item =>
                            //    {
                            //        if ((item.PlayerNumber != this.PlayerNumber) && (item.State != AvatarStates.Stunned))
                            //        {
                            //            Physics2D.IgnoreCollision(this.boxCollider, item.boxCollider, false);
                            //        }
                            //    });

                            break;
                        }
                    #endregion
                }
            }
        }
    }

    public Inventory Inventory = new Inventory();

    public GameObject ActiveInventory_Valde;
    public GameObject ActiveInventory_ValdeConAgua;

    private Animator animator;
    internal BoxCollider2D boxCollider;
    private string playerName
    {
        get { return "Player" + this.PlayerNumber + "-"; }
    }

    private AvatarStates previousState;
    //private SpriteRenderer spriteRendered;
    internal Rigidbody2D rigidBody;
    private GameObject currentCollisionDetector;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        //this.spriteRendered = GetComponent<SpriteRenderer>();
        this.tag = "Player";


        this.PlayerNumber = AppController.AddPlayer(this);
    }

    void FixedUpdate()
    {
        switch (this.State)
        {
            #region Normal
            case AvatarStates.Normal:
                {
                    this.CheckAvatarMove();

                    break;
                }
            #endregion
        }


        this.previousState = this.State;

        //Debug.Log(string.Format("Axis H: {0}, V: {1}.", (float)Input.GetAxis(this.playerName + "Horizontal"), (float)Input.GetAxis(this.playerName + "Vertical")));
    }

    private void CheckAvatarMove()
    {
        Vector2 moveVector = new Vector2();
        float axisHor = Input.GetAxis(this.playerName + "Horizontal");
        float axisVer = Input.GetAxis(this.playerName + "Vertical");

        if ((Input.GetButton(this.playerName + "Left"))
            || (axisHor <= -this.AxisSensitive))
        {
            moveVector += Vector2.left * this.Speed * Time.deltaTime;

            this.currentCollisionDetector = this.CollisionDetectorLeft;

            this.CollisionDetectorBottom.SetActive(false);
            this.CollisionDetectorLeft.SetActive(true);
            this.CollisionDetectorRight.SetActive(false);
            this.CollisionDetectorTop.SetActive(false);

            this.animator.SetBool("Moving", true);
            this.animator.SetFloat("MoveX", -1.5f);
        }
        else if ((Input.GetButton(this.playerName + "Right"))
            || (axisHor >= this.AxisSensitive))
        {
            moveVector += Vector2.right * this.Speed * Time.deltaTime;

            this.currentCollisionDetector = this.CollisionDetectorRight;

            this.CollisionDetectorBottom.SetActive(false);
            this.CollisionDetectorLeft.SetActive(false);
            this.CollisionDetectorRight.SetActive(true);
            this.CollisionDetectorTop.SetActive(false);

            this.animator.SetBool("Moving", true);
            this.animator.SetFloat("MoveX", 1.5f);
        }
        else
        {
            this.animator.SetFloat("MoveX", 0f);
        }

        if ((Input.GetButton(this.playerName + "Up"))
            || (axisHor <= -this.AxisSensitive))
        {
            moveVector += Vector2.up * this.Speed * Time.deltaTime;

            this.currentCollisionDetector = this.CollisionDetectorTop;

            this.CollisionDetectorBottom.SetActive(false);
            this.CollisionDetectorLeft.SetActive(false);
            this.CollisionDetectorRight.SetActive(false);
            this.CollisionDetectorTop.SetActive(true);

            this.animator.SetBool("Moving", true);
            this.animator.SetFloat("MoveY", 1.5f);
        }
        else if ((Input.GetButton(this.playerName + "Down"))
            || (axisHor >= this.AxisSensitive))
        {
            moveVector += Vector2.down * this.Speed * Time.deltaTime;

            this.currentCollisionDetector = this.CollisionDetectorBottom;

            this.CollisionDetectorBottom.SetActive(true);
            this.CollisionDetectorLeft.SetActive(false);
            this.CollisionDetectorRight.SetActive(false);
            this.CollisionDetectorTop.SetActive(false);

            this.animator.SetBool("Moving", true);
            this.animator.SetFloat("MoveY", -1.5f);
        }
        else
        {
            this.animator.SetFloat("MoveY", 0f);
        }

        if (moveVector == Vector2.zero)
            this.animator.SetBool("Moving", false);

        //else
        //{
        //    this.animator.SetBool("Moving", false);
        //    //this.animator.SetFloat("MoveX", 0.5f);
        //}

        //if (Input.GetButton(this.playerName + "Jump") && this.IsJumping == false)
        //{
        //    this.rigidBody.AddForce(Vector2.up * this.JumpHeight);

        //    this.IsJumping = true;
        //}


        if (Input.GetButtonDown(this.playerName + "Action"))
        {
            Collider2D itemCollider = Physics2D.OverlapCircle(this.currentCollisionDetector.transform.position, this.CollisionDetectionRadius, this.ItemsLayerMask);

            if (itemCollider)
            {
                Debug.Log("Item action fired!");

                var ic = itemCollider.GetComponent<ItemController>();

                ic.FireAction(this);
            }
        }


        //this.rigidBody.velocity = new Vector2(moveVector.x, this.rigidBody.velocity.y);
        this.transform.Translate(moveVector);
    }


    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    CollisionsManager.ResolveCollision(this.gameObject, col.gameObject, col);
    //}
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    CollisionsManager.ResolveCollision(this.gameObject, col.gameObject, col);
    //}

    //#region Subclasses

    //enum CollisionDetectors
    //{
    //    Bottom,
    //    Left,
    //    Right,
    //    Top,
    //}

    //#endregion
}