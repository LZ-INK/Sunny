using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 找机会，全抽取出来统调
/// </summary>
public class PlayerMove : MonoBehaviour
{

    /* [Range(1, 10)]
     public float jumpVelocity;

     [Range(1, 10)]
     public float Movespeed = 5;
     [Range(1, 10)]
     public int ClimbSpeed = 3;

     [Range(1, 10)]
     public int MaxJunpCount = 1;*/

    public PlayerData Data;
    
    public float groundedSkin = 0.05F;
    public float fallMultiplier = 2.5F; //��ɫ�������
    public float lowJumpMultiplier = 2F; //
    public float CrouchMultiplier = 2F; //
    
    public LayerMask JumplayerMask;  //可触发跳跃物检测
    public LayerMask ClimblayerMask; //可攀爬物检测
    public LayerMask ClimbEndlayerMask; //攀爬触地检测

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D cc;

    bool JumpRequest;
    bool isGround;
    bool CrouchRequest;
    bool ClimbRequest;
    int jumpCounter = 0;

    Vector2 playerSize;
    Vector2 boxSize;

    float MoveY;
    float MoveX;
    List<Collider2D> Ladders = new List<Collider2D>();


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        playerSize = cc.size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BestJump();
        Move();
        ClimbLadder();
        Crouch();
        LookUp();
        //把状态检查抽成事件注册
    }

    private void FixedUpdate()
    {
        
        animator.SetBool("isGround", isGround);
        animator.SetBool("CrouchRequest", CrouchRequest);
        animator.SetBool("ClimbRequest", ClimbRequest);
        if (JumpRequest)
        {
            animator.SetTrigger("JumpRequest");
            rb.AddForce(Vector2.up * Data.jumpVelocity, ForceMode2D.Impulse);
            //animator.SetTrigger("JumpRequest");
            JumpRequest =false;
        }
        else
        {
            Vector2 boxCenter = (Vector2)this.transform.position + Vector2.down * (((playerSize.y + boxSize.y) * 0.5F) - cc.offset.y);
            isGround = (Physics2D.OverlapBox(boxCenter,boxSize,0F, JumplayerMask) != null);
            if (isGround)
            {
                jumpCounter = 0;
            }
        }
        if (ClimbRequest)
        {
            animator.SetFloat("ClimbSpeed", Input.GetAxisRaw("Vertical"));
            rb.gravityScale = 0F;
            Vector2 direction = new Vector2(0, MoveY);
            rb.velocity = new Vector2(MoveX, direction.y * Data.ClimbSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Item"))
        {
            c.GetComponent<Item>().TouchOff();
        }
        ContactPoint2D[] points = new ContactPoint2D[2];
        c.GetContacts(points);
        for (int i = 0; i < points.Length; i++)
        {
            if (!Ladders.Contains(c)&& c.CompareTag("Ladder"))
            {
                Ladders.Add(c);
                return;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (Ladders.Contains(c))
        {
            Ladders.Remove(c);
            return;
        }
        
    }
    /*    private void OnCollisionExit2D(Collision2D c)
        {
            if (groundTouched.Contains(c.collider))
            {
                groundTouched.Remove(c.collider);
            }
        }
        private void OnCollisionEnter2D(Collision2D c)
        {
            ContactPoint2D[] points = new ContactPoint2D[2];
            c.GetContacts(points);
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].normal == Vector2.up && !groundTouched.Contains(c.collider)) 
                {
                    groundTouched.Add(c.collider);
                    return;
                }
            }
        }
    */

    private void Move()
    {
        float MoveX = Input.GetAxis("Horizontal") * Data.Movespeed;
        animator.SetFloat("Speed", Mathf.Abs(MoveX));
        //
        if (MoveX != 0)
        {
            Vector3 localScale = transform.localScale;
            if (MoveX < 0) localScale.x = -MathF.Abs(localScale.x);
            else localScale.x = MathF.Abs(localScale.x);
            transform.localScale = localScale;
            animator.SetBool("isStay", false);
        }
        else
        {
            animator.SetBool("isStay", true);
        }

        MoveX *= Time.deltaTime;
        transform.Translate(MoveX, 0, 0);
    }
    private void BestJump()
    {
        /* animator.SetFloat("VelocityY", rb.velocity.y);
         if (rb.velocity.y < 0)//��
         {
             rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
         }else if (rb.velocity.y > 0 & !Input.GetButton("Jump")) 
         {
             rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
         }*/
        animator.SetFloat("VelocityY", rb.velocity.normalized.y);
        if (rb.velocity.y < 0)//��
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 & !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1F;
        }

    }
    private void Jump()
    {
        if (jumpCounter >= Data.MaxJunpCount && !isGround )
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            
            JumpRequest = true;
          
            jumpCounter++;
        }
    }

    private void Crouch()
    {
        Vector2 offset = cc.offset;
        Vector2 size = cc.size;
        if (Input.GetKeyDown(KeyCode.S)&& !CrouchRequest)
        {
            CrouchRequest = true;
            offset.y = (CrouchMultiplier * offset.y) +0.15F;
            size.y /=  CrouchMultiplier;
            cc.offset = offset;
            cc.size = size;
          
        }
        if (CrouchRequest && (Input.GetKeyUp(KeyCode.S)||(!Input.GetKey(KeyCode.S))))
        {
            CrouchRequest = false;
            offset.y = (offset.y - 0.15F) / CrouchMultiplier;
            size.y *=  CrouchMultiplier;
            cc.offset = offset;
            cc.size = size;
        }
        playerSize = cc.size;
    }
    
    private void ClimbLadder()
    {
        Vector2 boxCenter = (Vector2)this.transform.position + Vector2.down * (((playerSize.y + boxSize.y) * 0.5F) - cc.offset.y);
        bool ClimbEnd = (Physics2D.OverlapBox(boxCenter, boxSize, 0F, ClimbEndlayerMask) != null);
        //isGround
        if ((Ladders.Count != 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !isGround) || 
            (Ladders.Count != 0 && isGround && Input.GetKey(KeyCode.W)))
        {
            ClimbRequest =true;
        }
        
        else if (Ladders.Count == 0||(ClimbEnd&& Input.GetKey(KeyCode.S))||(JumpRequest))
        {
            ClimbRequest = false;
        }

        //下检查地退出，检查攀爬↓进入
        //上检查攀爬↑进入 
        if (ClimbRequest)
        {
            MoveY = Input.GetAxis("Vertical");
        }
    }

    private void LookUp()
    {
        if (isGround&& !ClimbRequest && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("LookUp",true);
        }
        else
        {
            animator.SetBool("LookUp", false);
        }
    }

}

