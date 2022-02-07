using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE;

public class PlayerAttackState : State
{
    Transform player;
    Animator anim;

    float elapTime = 0f;
    float shootRate = .2f;
    float shootAmt = 1;

    public PlayerAttackState(FSM _fsm, Transform _player, Animator _anim) : base(_fsm){
        player = _player;
        anim = _anim;
    }

    public override void Enter(){}

    public override void Exit(){
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
    }

    public override void Update(){
        if(GameConstants.currentAmmunitionCount <= 0){
            fsm.SetCurrentState(fsm.GetState(2));
            return;
        }

        if(Input.GetKey(KeyCode.Z)){
            anim.SetBool("Attack1", true);
            shootRate = .2f;
            shootAmt = 1;
        }
        else if(Input.GetKey(KeyCode.X)){
            anim.SetBool("Attack2", true);
            shootRate = .5f;
            shootAmt = 3;
        }
        else if(Input.GetKey(KeyCode.C)){
            anim.SetBool("Attack3", true);
            shootRate = .4f;
            shootAmt = 1;
        }

        if(Input.GetKey(KeyCode.R)){
            fsm.SetCurrentState(fsm.GetState(2));
            return;
        }

        if(elapTime >= shootRate){
            GameConstants.currentAmmunitionCount -= shootAmt;
            elapTime = 0f;
        }
        else elapTime += Time.deltaTime;

        GameConstants.UpdateAmmoTxt();
    }

    public override void FixedUpdate(){}

    public override void LateUpdate(){}
}
