using System;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class MonsterScript : MonoBehaviour
{
    MonsterAI monsterAI;
    Rigidbody monsterRb;

    [SerializeField] private FPSController player;

    [SerializeField] public float roamAggroRange;
    [SerializeField] public float roamSpeed;

    [SerializeField] public float chaseSpeed;
    [SerializeField] public float chaseAggroRange;
	[SerializeField] public float chaseDuration;
	[SerializeField] public float chaseCooldown; // How long before the monster can chase again

    [SerializeField] public float attackRange;


    private void Awake()
    {
        monsterAI = new MonsterAI(this, player);
        monsterRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        monsterAI.UpdateState();
    }

    public void SetSpeedZero()
    {
        monsterRb.velocity = new Vector3(0f, 0f, 0f);
    }

    public State GetMonsterState()
    {
        return monsterAI.GetState();
    }

    public Vector3 GetVectorToPlayer()
    {
        return (player.transform.position - this.transform.position).normalized;
	}

    void DebugState()
    {
        Debug.Log(monsterAI.GetState());
    }



    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, roamAggroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseAggroRange);
    }
}
