using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TouchInput touchInput;
    [SerializeField]
    private PlayerAnimator playerAnimator;

    [System.Serializable]
    public class PlayerClass
    {
        public float gravity;
        public float speed;
        public float JumpHeight;
        public float speedReducePercent;
        public bool onRight, onLeft, onMiddle, onFly;
        public float laneSwitchSpeed;
        public Vector3 startPos;
        public bool isJump = false, isGrounded = false, isSliding;
        [Header("Collider Settings")]
        public Vector3 ColliderDefaultPos;
        public Vector3 ColliderSliderPos;
        public float colliderHeightDefault;
        public float colliderHeightSlide;
    }
    [SerializeField]
    public PlayerClass playerClass;

    [Header("Private Fields")]
    private bool isFly;
    private bool isSkate;
    public bool isHulk;
    Vector3 velocity;


    [Header("Player Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject Lane_1;
    [SerializeField] private GameObject Lane_2;
    [SerializeField] private GameObject Lane_3;
    [SerializeField] private GameObject Lane_Fly;
    [SerializeField] private GameObject MeshObject;
    [SerializeField] private float yVelocity;
    [SerializeField] public bool isStumbled;

    public bool canSwitchLeft = true;
    public bool canSwitchMiddle = true;
    public bool canSwitchRight = true;
    public bool isOnRight, isOnLeft, isOnMiddle;


    private void Start()
    {
        EventController.instance.stumbleEvent += player_Stumbled;
        EventController.instance.flyingEvent += FlyingPower_flyingEvent;
        EventController.instance.skateEvent += Instance_skateEvent;
        EventController.instance.hulkEvent += Instance_hulkEvent;
    }

    private void Instance_hulkEvent(bool obj)
    {
        isHulk = obj;
    }

    private void Instance_skateEvent(bool obj)
    {
        Debug.Log(obj + "Skate");
        isSkate = obj;

    }

    private void FlyingPower_flyingEvent(bool obj)
    {
        if (obj)
        {
            isFly = obj;


        }
        if (!obj)
        {
            isFly = obj;

        }
    }

    private void OnDestroy()
    {
        EventController.instance.stumbleEvent -= player_Stumbled;
        EventController.instance.flyingEvent -= FlyingPower_flyingEvent;
        EventController.instance.skateEvent -= Instance_skateEvent;
    }


    bool isComplete;

    private void Update()
    {
            if (isHulk && !isComplete)
            {
                            isComplete = true;
                MeshObject.transform.DOScale(1.75f, 0.2f).OnComplete(()=> {

                    MeshObject.transform.DOScale(1.5f, 0.25f).OnComplete(()=> {

                        MeshObject.transform.DOScale(2f, 0.2f).OnComplete(() => {
                        });
                    });

                });
            }
            else if (!isHulk && isComplete)
            {
                            isComplete = false;
                MeshObject.transform.DOScale(1.25f, 0.2f).OnComplete(() => {
                    MeshObject.transform.DOScale(1.5f, 0.25f).OnComplete(() => {

                        MeshObject.transform.DOScale(1.0f, 0.2f).OnComplete(() => {
                        });
                    });
                });
        }
        if (GameController.instance.isGameStart && !GameController.instance.isPlayerDead)
        {
            touchInput.swipe_Fn();
            SwitchLane();

            //   PlayerMovement();
        }


        if(isOnLeft)
        {
            transform.position = new Vector3(Lane_3.transform.position.x, transform.position.y, transform.position.z);
        }
        if(isOnMiddle)
        {
            transform.position = new Vector3(Lane_1.transform.position.x, transform.position.y, transform.position.z);
        }
        if(isOnRight)
        {
            transform.position = new Vector3(Lane_2.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {

        if (GameController.instance.isGameStart && !GameController.instance.isPlayerDead)
        {
            // rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.isKinematic = false;
            playerActions_rigid();
            PlayerMovement_rigid();
            //  playerActions();
        }
        if (GameController.instance.isPlayerDead)
        {


            playerAnimator.setCharacterAnimationCollide(true);
            //
            this.gameObject.transform.DOLocalMoveY(playerClass.startPos.y, 0.5f).OnComplete(() =>
            {
                rb.isKinematic = true;

            });
            playerClass.gravity = -50;
            Physics.gravity = new Vector3(0, playerClass.gravity, 0);
        }

        else if (!GameController.instance.isPressedStart)
        {
            //playerClass.onMiddle = true;
            //playerClass.onLeft = false;
            //playerClass.onRight = false;
            playerAnimator.setCharacterAnimationCollide(false);
            playerAnimator.setCharacterAnimationRun(false);
            // playerAnimator.SetBool("Stumble", false);
            playerAnimator.setCharacterAnimationJump(false);
            // yVelocity -= playerSettings.gravity;
            // this.gameObject.transform.position = new Vector3(0f,0f, -147.91f);

            this.gameObject.transform.position = playerClass.startPos;
            rb.isKinematic = true;

        }
        else if (GameController.instance.isPressedStart && GameController.instance.isContinueGame)
        {

            playerAnimator.setCharacterAnimationCollide(false);
            playerAnimator.setCharacterAnimationRun(false);
            // playerAnimator.SetBool("Stumble", false);
            playerAnimator.setCharacterAnimationJump(false);
            // yVelocity -= playerSettings.gravity;
            // this.gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            playerClass.gravity = -50;
            Physics.gravity = new Vector3(0, playerClass.gravity, 0);
        }


    }


    void SwitchLane()
    {
        if (Input.GetKeyDown(KeyCode.D) || touchInput.IfSwipeRight)
        {

            switchRight();
        }
        if (Input.GetKeyDown(KeyCode.A) || touchInput.ifSwipeLeft)
        {
            switchLeft();
        }

        if (isFly)
        {
            playerClass.onFly = isFly;
            playerAnimator.setCharacterAnimationSlide(false);
            playerAnimator.setCharacterAnimationFly(true);
            transform.DOMoveY(Lane_Fly.transform.position.y, 1f);
            playerClass.gravity = 50f;
            Physics.gravity = new Vector3(0, playerClass.gravity, 0);
            rb.useGravity = false;

        }
        if (!isFly && playerClass.onFly)
        {
            rb.useGravity = true;

            playerAnimator.setCharacterAnimationFly(false);
            playerAnimator.setCharacterAnimationJump(true);
            playerClass.onFly = isFly;
            playerClass.gravity = -9f;
            Physics.gravity = new Vector3(0, playerClass.gravity, 0);
            //transform.DOMoveY(playerSettings.startPos.y, 1f).OnComplete(() =>
            //{


            //});
        }

    }

    
    private void switchRight()
    {
       
        if (playerClass.onMiddle)
        {
            isOnRight = false;
            isOnMiddle = false;
            isOnLeft = false;
            if (canSwitchRight)
            {
                playerAnimator.setParentAnimationRight();
                transform.DOMoveX(Lane_2.transform.position.x, playerClass.laneSwitchSpeed).OnComplete(() =>
                 {

                     playerClass.onMiddle = false;
                     playerClass.onRight = true;
                     playerClass.onLeft = false;

                     isOnRight = true;
                     isOnMiddle = false;
                     isOnLeft = false;

                 });
            }
        }
        else if (playerClass.onLeft)
        {
            isOnRight = false;
            isOnMiddle = false;
            isOnLeft = false;
            if (canSwitchMiddle)
            {
                playerAnimator.setParentAnimationRight();
                transform.DOMoveX(Lane_1.transform.position.x, playerClass.laneSwitchSpeed).OnComplete(() =>
                {

                    playerClass.onMiddle = true;
                    playerClass.onRight = false;
                    playerClass.onLeft = false;

                    isOnRight = false;
                    isOnMiddle = true;
                    isOnLeft = false;

                });
            }
        }



    }

    private void switchLeft()
    {
        if (playerClass.onMiddle)
        {
            if (canSwitchLeft)
            {
                isOnRight = false;
                isOnMiddle = false;
                isOnLeft = false;
                playerAnimator.setParentAnimationLeft();
                transform.DOMoveX(Lane_3.transform.position.x, playerClass.laneSwitchSpeed).OnComplete(() =>
                {
                    playerClass.onMiddle = false;
                    playerClass.onRight = false;
                    playerClass.onLeft = true;

                    isOnRight = false;
                    isOnMiddle = false;
                    isOnLeft = true;

                });
            }
        }
        else if (playerClass.onRight)
        {
            if (canSwitchMiddle)
            {
                isOnRight = false;
                isOnMiddle = false;
                isOnLeft = false;
                playerAnimator.setParentAnimationLeft();
                transform.DOMoveX(Lane_1.transform.position.x, playerClass.laneSwitchSpeed).OnComplete(() =>
                {
                    playerClass.onMiddle = true;
                    playerClass.onRight = false;
                    playerClass.onLeft = false;

                    isOnRight = false;
                    isOnMiddle = true;
                    isOnLeft = false;


                });
            }
        }


    }

    //void playerActions()
    //{

    //    if (controller.isGrounded && !playerSettings.onFly)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space) || touchInput.IsSwipedUp)
    //        {

    //            yVelocity = playerSettings.JumpHeight;


    //        }
    //        else if ((Input.GetKeyDown(KeyCode.S) || touchInput.isSwipeDown) && !BikePower.isBikeActive)
    //        {

    //            playerAnimator.setCharacterAnimationSlide(true);

    //        }
    //        else if (isStumbled)
    //        {
    //            playerSettings.speed = (DifficultyController.instance.moveZSpeed - ((playerSettings.StumblePercentage / 100) * DifficultyController.instance.moveZSpeed)) / 100;
    //        }
    //        else
    //        {
    //            playerAnimator.setCharacterAnimationSlide(false);
    //            playerAnimator.setCharacterAnimationJump(false);
    //            playerAnimator.setCharacterAnimationRun(true);
    //            playerSettings.speed = DifficultyController.instance.moveZSpeed;
    //        }

    //    }
    //    else
    //    {
    //        if (!playerSettings.onFly)
    //        {
    //            playerAnimator.setCharacterAnimationSlide(false);
    //            playerAnimator.setCharacterAnimationJump(true);

    //            if (Input.GetKeyDown(KeyCode.S) || touchInput.isSwipeDown)
    //            {
    //                //  yVelocity -= 50;
    //                // touchInput.isSwipeDown = false;

    //            }
    //            else
    //            {
    //                //  yVelocity -= playerSettings.gravity;
    //            }
    //        }

    //    }
    //}




    //void PlayerMovement()
    //{


    //    velocity = new Vector3(0, 0, playerSettings.speed);
    //    velocity.y = yVelocity;

    //    controller.Move(velocity * Time.deltaTime);


    //}

    //private Vector3 lastVelocity, accel;
    //private Vector3 GetAcceleration()
    //{
    //    accel = (rigidbody.velocity - lastVelocity);
    //    lastVelocity = rigidbody.velocity;
    //    return accel;

    //}


    #region Collision Events
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerClass.isGrounded = true;

            if (!playerClass.isJump)
            {
                playerAnimator.setCharacterAnimationJump(false);
                playerAnimator.setCharacterAnimationRun(true);

            }
        }
        //if (collision.gameObject.tag == "Traps")
        //{

        //    Debug.Log("Enter =" + collision.gameObject.tag);


        //}
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerClass.isGrounded = true;


        }
        //if (collision.gameObject.tag == "Traps")
        //{

        //    Debug.Log("Stay " + collision.gameObject.tag);


        //}

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerClass.isGrounded = false;


        }
        //if (collision.gameObject.tag == "Traps")
        //{

        //    // Debug.Log("Exit " + collision.gameObject.tag);


        //}
    }

    #endregion
    float t = 0;
    private void playerActions_rigid()
    {
        if (!GameController.instance.isPlayerDead)
        {
            if (playerClass.isGrounded && !playerClass.onFly)
            {
                playerClass.gravity = -50f;
                Physics.gravity = new Vector3(0, playerClass.gravity, 0);
                t = 0;
                playerClass.speed = DifficultyController.instance.moveZSpeed;
                if (((Input.GetKeyDown(KeyCode.Space) || touchInput.IsSwipedUp) && !playerClass.isJump))
                {
                    Jump();

                }
                else if ((Input.GetKeyDown(KeyCode.S) || touchInput.isSwipeDown) && !BikePower.isBikeActive && !HulkPower.ishulkActive &&!SkatePower.isSkateActive && !playerClass.isJump && !playerClass.isSliding)
                {
                    if (!isSkate && !isHulk)
                    {
                        playerClass.isSliding = true;
                        playerAnimator.setCharacterAnimationSlide(true);
                    }

                }
                else if (isStumbled)
                {
                    playerClass.speed = DifficultyController.instance.moveZSpeed * playerClass.speedReducePercent;
                }
                else if (!playerClass.isJump)
                {
                    playerAnimator.setCharacterAnimationJump(false);
                    playerAnimator.setCharacterAnimationRun(true);

                }

            }
            if (!playerClass.isGrounded && !playerClass.onFly)
            {
                playerClass.speed = DifficultyController.instance.moveZSpeed * playerClass.speedReducePercent;
                playerAnimator.setCharacterAnimationSlide(false);
                t += Time.deltaTime;
                if (t >= 0.05f)
                {
                    playerAnimator.setCharacterAnimationJump(true);
                }

                // Debug.Log("FX: " + t);
                if ((Input.GetKeyDown(KeyCode.S) || touchInput.isSwipeDown))
                {

                    playerClass.gravity = -3000f;
                    Physics.gravity = new Vector3(0, playerClass.gravity, 0);
                    //  yVelocity -= 50;
                    touchInput.isSwipeDown = false;


                }
                else
                {
                    if (playerClass.gravity >= -50)
                        playerClass.gravity -= 2f;
                    Physics.gravity = new Vector3(0, playerClass.gravity, 0);
                }
            }
        }
    }


    private void Jump()
    {
        playerClass.isJump = true;
        playerAnimator.setCharacterAnimationJump(true);
        // rigidbody.AddForce(Vector3.up * playerSettings.JumpHeight, ForceMode.Impulse);
        rb.velocity = Vector3.up * playerClass.JumpHeight;
        StartCoroutine(FixJump());
    }

    private IEnumerator FixJump()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => playerClass.isGrounded);
        playerAnimator.setCharacterAnimationJump(false);
        playerClass.isJump = false;

    }

    private void PlayerMovement_rigid()
    {
        if (!GameController.instance.isPlayerDead)
            rb.transform.Translate(Vector3.forward * playerClass.speed);
        //velocity = new Vector3(0, 0, playerSettings.speed);
        //velocity.y = yVelocity;
        //controller.Move(velocity * Time.deltaTime);


    }

    private void player_Stumbled()
    {
        Debug.Log("Stumble");
        isStumbled = true;
        // Debug.Log("Instance " +restrictedClass. isStumbled);
        StartCoroutine(player_Stumbled_False());
    }

    IEnumerator player_Stumbled_False()
    {
        yield return new WaitForSeconds(0.75f);
        isStumbled = false;
    }



}
