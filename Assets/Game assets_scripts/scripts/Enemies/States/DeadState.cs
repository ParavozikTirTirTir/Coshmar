using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    //public override void Enter()
    //{
    //    base.Enter();

    //    GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGO.transform.position, stateData.deathBloodParticle.transform.rotation);
    //    GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGO.transform.position, stateData.deathChunkParticle.transform.rotation);

    //    entity.gameObject.SetActive(false);
    //}

    public override void Enter()
    {
        base.Enter();

        // 1. Получаем SpriteRenderer.
        SpriteRenderer spriteRenderer = entity.aliveGO.GetComponent<SpriteRenderer>();
        Vector3 spawnPosition = entity.aliveGO.transform.position;
        if (spriteRenderer != null)
        {
            // 2. Получаем центр спрайта.
            spawnPosition = spriteRenderer.bounds.center;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer не найден у объекта " + entity.aliveGO.name);
        }
        // 3. Создаем частицы в центре спрайта.
        GameObject.Instantiate(stateData.deathBloodParticle, spawnPosition, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticle, spawnPosition, stateData.deathChunkParticle.transform.rotation);


        entity.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
