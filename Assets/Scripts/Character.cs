using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace characterMovement
{
    public class Character : MonoBehaviour
    {
        protected CapsuleCollider2D bodyCollider;
        protected Rigidbody2D body;
        protected MovementRevamped movementRewampedInputActions;
        protected bool isLookingRight;
        protected SpriteRenderer spriteRenderer;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            Initialisation();
        }

        protected virtual void Initialisation()
        {
            bodyCollider = GetComponent<CapsuleCollider2D>();
            body = GetComponent<Rigidbody2D>();
            movementRewampedInputActions = new MovementRevamped();
            isLookingRight = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void flip()
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.x < 0)
            {
                spriteRenderer.color = Color.yellow;
            }
            else if (transform.localScale.x > 0)
            {
                spriteRenderer.color = Color.white;
            }

        }
        /// <summary>
        /// A utility method to check if player is standing on ground
        /// </summary>
        /// <param name="groundCheckRayCastLength">Length of the ray from center</param>
        /// <param name="groundLayerMask">Ground layer mas</param>
        /// <returns>bool</returns>
        protected virtual bool checkIfStandingOnGround(float groundCheckRayCastLength, LayerMask groundLayerMask)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRayCastLength, groundLayerMask);
            if (hit)
            {
                //Debug.DrawLine(transform.position, hit.point, Color.black);
                return true;
            }
            return false;
        }

    }
}