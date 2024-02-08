using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//jme
public abstract class BossStates 
{
    protected Boss boss;

    public BossStates(Boss boss) 
    {
        this.boss = boss;
    }
    public abstract void DoActionUpdate(float dTime);
}

public class BossChase : BossStates
{
    public BossChase(Boss boss): base(boss)
    {
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = true;
    }

    float timer = 0;

    public override void DoActionUpdate(float dTime)
    {
        timer += dTime;
        if(timer > boss.preAtkInterval)
        {
            Debug.Log("boss changing to preatk");
            boss.SetCurrentState(new BossCharge(boss));
        }
    }
}

public class BossCharge : BossStates
{
    public BossCharge(Boss boss) : base(boss)
    {
        boss.interruptHitbox.enabled = true;
        boss.atkHitbox.enabled = false;
    }

    float timer = 0;

    public override void DoActionUpdate(float dTime)
    {
        timer+= dTime;
        if(timer > boss.preAtkDuration)
        {
            Debug.Log("boss changing to atk");
            boss.SetCurrentState(new BossAttack(boss));
        }
    }
}

public class BossAttack : BossStates
{
    public BossAttack(Boss boss) : base(boss)
    {
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = true;
    }

    public override void DoActionUpdate(float dTime)
    {

    }
}

public class BossInterrupt : BossStates
{
    public BossInterrupt(Boss boss) : base(boss)
    {
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = false;
        //both are set to false so can "spit player out"
    }

    public override void DoActionUpdate(float dTime)
    {
        // if want to implement stun is can 

        //but for now it just go back to chase immediately
        boss.SetCurrentState(new BossChase(boss));
    }
}