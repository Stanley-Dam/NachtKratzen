using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Player {

    protected override void MoveHandler(int movementType) {
        //Play audio :)
        //And animation
        switch (movementType) {
            case 0:
                animator.SetFloat("Speed", 0f);
                break;
            case 1:
                playerAudio.Walk(PlayerAudioType.HIDER_WALK);
                animator.SetFloat("Speed", 0.5f);
                break;
            case 2:
                playerAudio.Walk(PlayerAudioType.HIDER_RUN);
                animator.SetFloat("Speed", 1f);
                break;
            case 3:
                playerAudio.Jump(PlayerAudioType.HIDER_JUMP);
                break;
        }
    }

}
