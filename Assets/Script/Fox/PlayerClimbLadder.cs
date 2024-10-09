using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    public LayerMask layerMask;

    bool ClimbRequest;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D cc;

    Vector2 playerSize;
    Vector2 boxSize;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        playerSize = cc.size;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        
    }
}
