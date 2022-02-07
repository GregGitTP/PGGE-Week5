using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using PGGE;

public class PlayerReloadState : State
{
    Animator anim;

    float elapTime = 0f;

    public PlayerReloadState(FSM _fsm, Animator _anim) : base(_fsm){
        anim = _anim;
    }

    public override void Enter(){
        GameConstants.ReloadAmmoTxt();

        elapTime = 0f;

        anim.SetTrigger("Reload");
    }

    public override void Exit(){
        GameConstants.currentAmmunitionCount = GameConstants.maxAmmunitionCount;
    }

    public override void Update(){}

    public override void FixedUpdate(){}

    public override void LateUpdate(){
        if(elapTime >= anim.GetCurrentAnimatorClipInfo(0)[0].clip.length) fsm.SetCurrentState(fsm.GetState(0));
        else elapTime += Time.deltaTime;
    }
}
