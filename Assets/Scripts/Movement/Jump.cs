using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace characterMovement
{
    public class Jump : Abilities
    {
        [SerializeField] protected float jumpForce = 5f;
        [SerializeField] float groundCheckRayCastLength = 2f;
        [SerializeField] LayerMask groundLayerMask;
        [SerializeField] int maxJumps = 2;


        protected bool onGround;
        [SerializeField] protected float cayoteTimeBuffer = 1f;
        protected  float currentTime;
        protected int jumpCounter;

        protected override void Initialisation()
        {
            base.Initialisation();
            currentTime = cayoteTimeBuffer;
            jumpCounter = maxJumps;
        }

        // Update is called once per frame
        void Update()
        {
            onGround = checkIfStandingOnGround(groundCheckRayCastLength, groundLayerMask);
            if (onGround)
            {
                currentTime = cayoteTimeBuffer;
                jumpCounter = maxJumps;
            }
            else
            {
                timeLeftToJump();
            }
        }
        /// <summary>
        /// Start a time counter to check if player still has time to jump --> Used to implement Cayote jump
        /// </summary>
        private void timeLeftToJump()
        {
            currentTime -= Time.deltaTime;
        }

        /// <summary>
        /// Basic method to apply force on player object on vertical direction to jump
        /// we are only jumping on performed context and only when either player is on ground
        /// or if player still has time to jump or double jump counter
        /// </summary>
        /// <param name="context"></param>

        public void PlayerJump(InputAction.CallbackContext context)
        {
            if (context.started || context.canceled) return;
            if ((!onGround && currentTime < 0) && (jumpCounter <= 0)) return;

            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter -= 1;
        }




    }
}