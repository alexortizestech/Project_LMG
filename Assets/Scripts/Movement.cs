using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Rewired;
public class Movement : MonoBehaviour
{
    public KeyCode Attack,JumpKey;

    public NailedRigidbody nr;
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;


    [Space]
    [Header("Slash")]

    public float radius;
    public float CountSlash;
    public float ComboTime;
    public float limit;
    public LayerMask enemyLayer;
    public int Damage;
    public bool Combo;
    public GameObject SpriteSlash;


    [Space]
    [Header("Player")]
    public int Health;
    public Vector3 lastPos;
    public GameObject ComboPlaceHolder,door;
    public AnalyticsEventTracker at;
    [SerializeField] public int playerID = 0;
    [SerializeField] public Player player;


    [Space]
    [Header("Management")]
    public Scene currentScene;
    public GameManager gm;
    public bool ipPackage, controlPackage;
    public int ipCount;
    [SerializeField] public int ipKills;
    public int DashNerf;
    public bool canDash, canBulletTime, canNail;
    public float TimeLevel;


    // Start is called before the first frame update
    void Start()
    {
        ipPackage = false;
        controlPackage = false;
        playerID = 0;
        player = ReInput.players.GetPlayer(playerID);
        currentScene = SceneManager.GetActiveScene();
        Health = 1;
        CountSlash = 1;
        Damage = 1;
        //enemyLayer = LayerMask.NameToLayer("Enemy");
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        nr.GetComponent<NailedRigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeLevel = Time.timeSinceLevelLoad;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        ComboTime += 1 * Time.deltaTime;
        if (ComboTime >= limit)
        {
            Damage = 1;
            Combo = false;

        }

        if (Combo == false)
        {
            Damage = 1;
           
        }
        if (coll.onWall &&  canMove   && nr.isHooking)
        {
            if(side != coll.wallSide)
                anim.Flip(side*-1);
           wallGrab = true;
            wallSlide = false;

          
        }
        if (coll.onWall)
        {
            Debug.Log("freeze");
           // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (Input.GetButtonUp("Fire3") || !coll.onWall) // || !canMove
        {
            wallGrab = false;
            wallSlide = false;
          
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if ((coll.onWall || coll.onCeiling) && nr.isHooking)
        {
            //canMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (coll.onGround)
        {
            // nr.CancelHook();
            nr.isHooking = false;
        }
        if (nr.isHooking == false)
        {
         
            
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }


        if (nr.isHooking)
        {
            if (player.GetButtonDown("Jump"))
            {
                nr.CancelHook();
            }
        }

        if (player.GetButtonDown("Jump"))
        {
            if(coll.onGround && (coll.onLeftWall || coll.onRightWall))
            {
                nr.CancelHook();
            }
        }


        if (wallGrab && !isDashing)
        {
            /*rb.gravityScale = 0;
            if(x > .2f || x < -.2f)
            rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));*/
        }
        else
        {
            rb.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
               // WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (player.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (player.GetButtonDown("Slash") && !hasDashed)
        {
            if (CountSlash == 1 && canDash)
            {
                if (xRaw != 0 || yRaw != 0)
                    Dash(xRaw, yRaw);
            }
            
        }

        if (isDashing)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
               

                enemy.GetComponent<EnemyBehaviour>().TakeDamage(Damage);
                
                if (enemy.GetComponent<EnemyBehaviour>().Health>Damage)
                {
                    Combo = false;
                    Debug.Log("Failed Kill");
                }
                Debug.Log("Killed");


            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }

        if (CountSlash == 1)
        {
            SpriteSlash.SetActive(true);
        }else if (CountSlash == 0)
        {
            SpriteSlash.SetActive(false);
        }


        if (Health <= 0)
        {
            Die();
        }

        /*if (nr.isHooking)
        {
            lastPos = transform.position;
        }*/

        if (Combo)
        {
            ComboPlaceHolder.SetActive(true);
        }else if (!Combo)
        {
            ComboPlaceHolder.SetActive(false);
        }

        if (ipCount >= ipKills)
        {
            ipPackage = true;
        }

        if (ipPackage == true)
        {
            door.SetActive(true);
        }

        if (controlPackage == true)
        {
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void Die()
    {

        AnalyticsResult analyticsResult = Analytics.CustomEvent("PlayerDeath", new Dictionary<string, object>{
           {
               "Level",currentScene.name }});
        Debug.Log("result" + analyticsResult);


        AnalyticsResult analyticsTime = Analytics.CustomEvent("TimePlayed", new Dictionary<string, object>
        {
            {"Level",currentScene.name},
            { "TimePlayed",TimeLevel} });
        Analytics.FlushEvents();
        gm.GameOver();
        Destroy(this.gameObject);
    }
    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {

        if (coll.onWall || coll.onCeiling)
        {
            nr.CancelHook();
        }
        CountSlash = 0;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed/DashNerf;

      
        StartCoroutine(DashWait());
        
        
    }

    public void Killed()
    {
       
        if (Combo)
        {
           
            Damage += 1;
        }
        else if (!Combo)
        {
            Combo = true;
        }

        CountSlash = 1;
        ComboTime = 0;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);



        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        //if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up  + wallDir / 2.5f), true);
       // Jump((Vector2.up), true);
        wallJumped = true;
    }

    private void WallSlide()
    {
      /*  if(coll.wallSide != side)
         anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);*/
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<VisionRange>().canSeePlayer == true && !isDashing)
            {
                Health--;
            }
            Debug.Log("Trigger Works");

        }

        if (other.CompareTag("ipPackage"))
        {
            ipPackage = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("ControlPackage"))
        {
           
            controlPackage = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Door"))
        {
            gm.Win();
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isDashing)
            {
                other.GetComponent<EnemyBehaviour>().Failed = true;
                other.GetComponent<EnemyBehaviour>().myFunctionDone = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (nr.isHooking)
            {
                //transform.position = lastPos;
            }
           
        }
    }
}
