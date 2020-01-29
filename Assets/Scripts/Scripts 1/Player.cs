using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public  float           m_speed;
    public  float           m_jumpPower;
    public  float           m_blockTime;

    public  Rigidbody2D            m_rigidBody;
	private SpriteRenderer         m_spriteRenderer; 
    //private GamePlayManager        m_gameManager;
    private MainCharacterAnimation m_mainCharacterAnimation;

    private float           m_floorLevel;
    private bool            m_isGrounded; 
    private bool            m_isBlocking;

    public float            m_health;
    public float            m_maxHealth         = 100;
    public bool             m_die               = false;
    public bool             m_inmortal          = false;
    public Text             m_LifeText;
    public Text             m_InvencibleText;

    public int              m_punchDamage       = 25;


    private void Awake                () 
	{
		Init ();
	}

    private void Init                 () 
	{
        // Get References
        m_rigidBody                   = GetComponent<Rigidbody2D> ();
		m_mainCharacterAnimation      = GetComponent<MainCharacterAnimation>();
        m_spriteRenderer              = GetComponent<SpriteRenderer> ();

        // Set private values
		m_rigidBody.gravityScale      = 0;
		m_floorLevel                  = float.MinValue;
		m_isGrounded                  = true;
	}

    public void  Move                 (float x, float y) 
	{
		if (!m_isBlocking && !m_die)
        {
            // Flip Sprite
            if (x < 0)
            { 
				transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z); 
			}
            else if (x > 0)
            { 
				transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z); 
			}

            // Move the sprite
            m_rigidBody.velocity = new Vector2 (x, m_rigidBody.velocity.y / m_speed) * m_speed; 

            if (m_isGrounded)
            { 
				m_rigidBody.velocity = new Vector2 (m_rigidBody.velocity.x / m_speed, y) * m_speed;

                // Set Animation
                if (m_rigidBody.velocity == Vector2.zero)
                { 
					m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 0); 
				}
                else
                { 
					m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 2);
				}
			} 
		}
	}
    
    public void  Idle                 () 
	{
        // Set AnimationController
        m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition",      0); 
		m_mainCharacterAnimation.ChangeAnimatorState ("blockTransition",       0); 

		m_isGrounded = true;
	}

    public void  Jump                 ()
    {
        if (m_isGrounded && !m_isBlocking)
        { 
			m_isGrounded             = false;
			m_rigidBody.gravityScale = 10; 
			m_rigidBody.velocity     = new Vector2 (m_rigidBody.velocity.x, m_jumpPower); 
			m_floorLevel             = transform.position.y - 0.0001f;

			GetComponent<BoxCollider2D> ().enabled = false; 
			m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 3);
            //m_mainCharacterAnimation.m_animator.SetBool(1, true);
		}
    }

    public void  IsInGround                 () 
	{
		m_rigidBody.gravityScale               = 0; 
		m_isGrounded                           = true;
		m_rigidBody.velocity                   = new Vector2 (m_rigidBody.velocity.x, 0);
		m_floorLevel                           = - 10000;
		GetComponent<BoxCollider2D> ().enabled = true;

		Idle (); 
	}

    private void FixedUpdate          ()
    {
        // Check if player is in ground
        if (transform.position.y <= m_floorLevel) 
		{
			IsInGround (); 
		}
	}

    public void Block()
    {
        StartCoroutine ("block");
    }

    public IEnumerator block() 
	{
		if (m_isGrounded && !m_isBlocking)
        { 
			m_rigidBody.velocity = Vector2.zero;
			m_mainCharacterAnimation.ChangeAnimatorState("blockTransition", 1); 
			m_isBlocking = true;
            m_inmortal = true;
			yield return new WaitForSeconds (m_blockTime); 
			m_isBlocking = false;
            m_inmortal = false;
			Idle();
		}
	}

    public void  Attack               () 
	{
		StartCoroutine ("attack");
	}

    public IEnumerator attack() 
	{
        #region Item
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 1);
        Item item = null;

        for(int i =0; i < coll.Length; i++)
        {
            if(coll[i].GetComponent<Item>() != null)
            {
                item = coll[i].GetComponent<Item>();
                break;
            }
        }

        if (item != null)
        {
            //StartCoroutine("takeWeapon");
                Debug.Log("Guantes");
                ApplyItem(item);
        }
        #endregion

        #region DestructibleItem
        Collider2D[] coll2 = Physics2D.OverlapCircleAll(transform.position, 1);
        DestructibleObject saco = null;


        for (int i = 0; i < coll2.Length; i++)
        {
            if (coll2[i].GetComponent<DestructibleObject>() != null)
            {
                saco = coll2[i].GetComponent<DestructibleObject>();
                Debug.Log("Un saco");
                break;        
            }
        }

        if(saco != null)
        {
            saco.RecieveHit();
            Debug.Log("Le quedan " + saco.healt);
        }
        #endregion

        #region Enemy
        Collider2D[] coll3 = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        Enemy Tayson = null;
        for (int i = 0; i < coll3.Length; i++)
        {
            if (coll3[i].GetComponent<Enemy>() != null)
            {
                Tayson = coll3[i].GetComponent<Enemy>();
                Debug.Log("Un Negro");
                break;
            }
        }

        if (Tayson != null)
        {
            Tayson.RecieveDamage(m_punchDamage);
            Debug.Log("Le quedan " + Tayson.m_life);
        }
        #endregion


        #region Animation
        if (m_isGrounded && !m_isBlocking)
        {
            m_rigidBody.velocity = Vector2.zero;

            m_mainCharacterAnimation.ChangeAnimatorState("movingTransition", 4);
            yield return new WaitForSeconds(0.5f);
            m_mainCharacterAnimation.ChangeAnimatorState("movingTransition", 0);
        }
        #endregion
    }

    public void ApplyItem(Item item)
    {
        item.EnterAction(this);
    }

    public void SetInvencibility(bool Invencibility)
    {
        m_inmortal = Invencibility;
        
    }

    public void GetDamage(float damage)
    {
        if (!m_inmortal)
        {
            m_health -= damage;
        }
    }
    public void GetLife(float life)
    {
        if (m_health + life< m_maxHealth)
        {
            m_health += life;
        }
        else
        {
            m_health = m_maxHealth;
        }
    }

    float seconder;
    private void Update()
    {
        if (m_inmortal)
        {
            seconder += Time.deltaTime;
            if (seconder >= 3)
            {
                m_inmortal = false;
                seconder = 0;
            }
        }
        if (m_health <= 0)
        {
            Die();
        }
        //Update de la UI
        m_InvencibleText.text = "Invencible: "+m_inmortal;
        m_LifeText.text = "Health: " + m_health;
    }

    public void Die()
    {
        m_die = true;
        StartCoroutine("die");
    }

    public IEnumerator die()
    {
        m_mainCharacterAnimation.ChangeAnimatorState("movingTransition", 5);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("0_Menu");
    }

}