using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
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
        //Debug.Log("CHASING");
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = true;
        //boss.changeColor(Color.red);
        boss.resetStateImg();
        boss.setStateImg();
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

        if (boss.canMove)
        {
            boss.MoveToPlayer();
        }
    }
}

public class BossCharge : BossStates
{
    public BossCharge(Boss boss) : base(boss)
    {
        //Debug.Log("PREATKING");
        boss.interruptHitbox.enabled = true;
        boss.atkHitbox.enabled = false;
        //boss.changeColor(Color.blue);
        boss.changeStateImg();
        boss.setStateImg();
        boss.PlayChargeSound();
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

        if (boss.canMove)
        {
            boss.MoveToPlayer();
        }
    }
}

public class BossAttack : BossStates
{
    public BossAttack(Boss boss) : base(boss)
    {
        //Debug.Log("ATKING");
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = true;
        //boss.changeColor(Color.red);
        boss.changeStateImg();
        boss.setStateImg();
        boss.PlayAttackSound();
    }

    public override void DoActionUpdate(float dTime)
    {
        boss.attack();
    }
}

public class BossInterrupt : BossStates
{
    public BossInterrupt(Boss boss) : base(boss)
    {
        //Debug.Log("INTED");
        boss.interruptHitbox.enabled = false;
        boss.atkHitbox.enabled = false;
        //both are set to false so can "spit player out"
        //boss.canMove = false;
        boss.thrownPlayer = false;
        //boss.changeColor(Color.green);
        boss.changeStateImg();
        boss.setStateImg();
    }

    float timer = 0;

    public override void DoActionUpdate(float dTime)
    {
        //timer+= dTime;

        //player pos set to head pos n then spit player out
        if (!boss.thrownPlayer)
        {
            boss.throwPlayer();
            boss.moveBack();
        }

        // stop moving for like idk 2 seconds
        /*if (timer > boss.stunDuration)
        {
            boss.canMove = true;
            //go back to chase 
            boss.SetCurrentState(new BossChase(boss));
        }*/

        //aft moving back it starts moving again
    }
}