using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Player {

    private bool isJumping = false;

    public void PlayDeathAnimation() {
        animator.SetBool("Dead", true);
    }

    protected override void MoveHandler(int movementType) {
        localBodyObjects.isSeeker = false;

        //Play audio :)
        //And animation
        switch (movementType) {
            case 0:
                animator.SetBool("Crouching", false);
                animator.SetFloat("Speed", 0f);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
            case 1:
                playerAudio.Walk(PlayerAudioType.HIDER_WALK);
                animator.SetFloat("Speed", 0.5f);
                animator.SetBool("Crouching", false);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
            case 2:
                playerAudio.Walk(PlayerAudioType.HIDER_RUN);
                animator.SetFloat("Speed", 1f);
                animator.SetBool("Crouching", false);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
            case 3:
                playerAudio.Walk(PlayerAudioType.HIDER_WALK);
                animator.SetFloat("Speed", -1f);
                animator.SetBool("Crouching", false);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
            case 4:
                if(!isJumping) {
                    playerAudio.Jump(PlayerAudioType.HIDER_JUMP);
                    animator.SetBool("IsJumping", true);
                    animator.SetBool("Crouching", false);
                    isJumping = true;
                }
                break;
            case 5:
                if (!animator.GetBool("Crouching"))
                    animator.SetBool("Crouching", true);

                animator.SetFloat("Speed", 0f);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
            case 6:
                if(!animator.GetBool("Crouching"))
                    animator.SetBool("Crouching", true);

                animator.SetFloat("Speed", 1f);

                if (isJumping) {
                    animator.SetBool("IsJumping", false);
                    isJumping = false;
                }

                break;
        }
    }

}
