using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Player {

    private void Awake() {
        RaycastPickup.foundPlayerEvent += KillPlayer;
        IsRenderingScript.isVisibleEvent += OnBeingRendered;
    }

    private void OnDisable() {
        RaycastPickup.foundPlayerEvent -= KillPlayer;
        IsRenderingScript.isVisibleEvent -= OnBeingRendered;
    }

    private void KillPlayer(GameObject playerGameObject) {
        if(playerGameObject.GetComponent<Hider>())
            this.networkManager.KillPlayer(playerGameObject.GetComponent<Hider>().ClientId);
    }

    private void OnBeingRendered(bool isSeekerVisible) {
        if(!this.IsMainPlayer)
            this.networkManager.PlayerSeesSeeker(isSeekerVisible);
    }

    public void PlayPlayerKillAnimation() {
        animator.SetBool("Attack", true);
        StartCoroutine("KillAnimationCooldown");
    }

    private IEnumerator KillAnimationCooldown() {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack", false);

        StopCoroutine("KillAnimationCooldown");
        yield return null;
    }

    protected override void MoveHandler(int movementType) {
        //Play audio and animations :)
        switch (movementType) {
            case 0:
                animator.SetBool("IsJumping", false);
                animator.SetFloat("Speed", 0f);
                break;
            case 1:
                animator.SetBool("IsJumping", false);
                animator.SetFloat("Speed", 0.5f);
                playerAudio.Walk(PlayerAudioType.SEEKER_WALK);
                break;
            case 2:
                animator.SetBool("IsJumping", false);
                animator.SetFloat("Speed", 1f);
                playerAudio.Walk(PlayerAudioType.SEEKER_RUN);
                break;
            case 3:
                break;
            case 4:
                animator.SetBool("IsJumping", true);
                playerAudio.Jump(PlayerAudioType.SEEKER_JUMP);
                break;
            case 5:
                animator.SetFloat("Speed", 0f);
                break;
            case 6:
                animator.SetFloat("Speed", 0.5f);
                break;
        }
    }

    public void PlayerSeesSeekerHandler(bool isVisible, Hider hider) {
        if(isVisible && this.IsMainPlayer) {
            IndicatorSystem.CreateIndicator(hider, this.transform);
        }
    }

}
