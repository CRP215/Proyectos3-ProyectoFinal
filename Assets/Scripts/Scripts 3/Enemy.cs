using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject               m_targetPosition;
    public Transform                m_hitPosition;
    public float                    m_minDistanceBetweenFighters;
    public float                    m_speed;
    public float                    m_minTimeBeforeAttack;
    public float                    m_maxTimeBeforeAttack;
    public int                      m_life;
    public int                      m_damage;

    private Rigidbody2D             m_rigidBody;
    private MainCharacterAnimation  m_mainCharacterAnimation;
    private bool                    m_isDead                    = false;
    private bool                    m_doingAttack               = false;

    public Vector3                  m_desviation                = Vector2.zero;

    private void Start        ()
    {
        m_rigidBody                 = GetComponent<Rigidbody2D> ();
		m_mainCharacterAnimation    = GetComponent<MainCharacterAnimation>();
        m_targetPosition            = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update       ()
    {
        if (m_targetPosition.transform.position.x > transform.position.x)
        { 
			transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
		}
        else
        { 
			transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z);
		}

		if (Mathf.Abs(m_targetPosition.transform.position.x + m_desviation.x - transform.position.x) > m_minDistanceBetweenFighters ||
            Mathf.Abs(m_targetPosition.transform.position.y + m_desviation.y - transform.position.y) > m_minDistanceBetweenFighters)
        {
            Vector3 direction = (m_targetPosition.transform.position + m_desviation) - transform.position; 
            Move (direction.normalized.x, direction.normalized.y); // move to enemy
        }
        else if (!m_doingAttack)
        { 
            m_doingAttack = true;
            StartCoroutine ("attack");
		}
    }

    public void  Move         (float x, float y) 
	{
        if (!m_isDead)
        {
            // Flip
            if (x < 0)
        { 
			transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z); 
		}
        else if (x > 0)
        { 
			transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z); 
		}

        // Move
        
            m_rigidBody.velocity = new Vector2(x, y) * m_speed;
        
		

        // Animation
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

    public IEnumerator attack () 
	{
		if (!m_isDead)
        { 
			m_rigidBody.velocity = Vector2.zero; 
    		m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 4); 

            Collider2D[] coll = Physics2D.OverlapCircleAll(m_hitPosition.position, 1);
            
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].tag.Equals("Player"))
                {
                    m_targetPosition.GetComponent<Player>().GetDamage (m_damage);
                    break;
                }
            }
		}

        yield return new WaitForSeconds (0.3f); 
        m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 0);
        yield return new WaitForSeconds (Random.Range (m_minTimeBeforeAttack, m_maxTimeBeforeAttack)); 
        m_doingAttack = false;
	}

    public void RecieveDamage (int damage)
    {
        m_life -= damage;
        
        if (m_life <= 0)
        {
            Die();
            m_isDead = true;
        }
    }

    public void Die()
    {
        StartCoroutine("die");
    }

    public IEnumerator die()
    {
        m_mainCharacterAnimation.ChangeAnimatorState("movingTransition", 5);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
