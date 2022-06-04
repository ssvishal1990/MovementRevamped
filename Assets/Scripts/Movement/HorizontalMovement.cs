using System.Collections;
using UnityEngine;

namespace characterMovement
{
    public class HorizontalMovement : Abilities
    {
        [SerializeField] protected float m_Speed;
        [SerializeField] protected float accelerationMagnitude;
        [SerializeField] protected float acceleration;
        [SerializeField] protected float deacceleration;
        [SerializeField] protected float velocityPower;

        private Vector2 direction;
        protected float currentX;
        // Use this for initialization
        protected override void Initialisation()
        {
            base.Initialisation();
        }

        // Update is called once per frame
        void Update()
        {
            //MoveThroughForce();
            SetHorizontalDirectionValue();

        }

        private void SetHorizontalDirectionValue()
        {
            movementRewampedInputActions.Enable();  
            currentX = movementRewampedInputActions.Player.Move.ReadValue<Vector2>().x;
            if (currentX < 0 && isLookingRight)
            {
                flip();
                isLookingRight = false;
            }else if (currentX > 0 && !isLookingRight)
            {
                flip();
                isLookingRight = true;
            }

        }


        private void FixedUpdate()
        {
            //Move();
            moveThroughForce();
        }

        private void moveThroughForce()
        {
            float targetSpeed = currentX * m_Speed;
            float speedDif = targetSpeed - body.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deacceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velocityPower) * Mathf.Sign(speedDif);
            float fc = (float)Mathf.Round(movement * 100f) / 100f;
            if (!Mathf.Approximately(fc, 0.0f))
            {
                body.AddForce(movement * Vector2.right);
            }
        }
        private void Move()
        {
            float targetSpeed = currentX * m_Speed;
            int  accelValue = Mathf.RoundToInt((Mathf.Abs(targetSpeed) > 0.01f ? accelerationMagnitude : -1 * accelerationMagnitude));
            Debug.Log(accelValue);
            if (Mathf.Abs(body.velocity.x) > m_Speed)
            {
                //body.velocity = new Vector2(currentX * m_Speed, body.velocity.y);
                body.AddForce(new Vector2(currentX , 0f) * m_Speed, ForceMode2D.Impulse);
            }
            else
            {
                //body.velocity += new Vector2(currentX * accelValue, body.velocity.y);
                body.AddForce(new Vector2(currentX, 0f) * accelValue, ForceMode2D.Impulse);
            }
        }
    }
}