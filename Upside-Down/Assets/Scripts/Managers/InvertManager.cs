using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertManager : MonoBehaviour
{
    public GameObject monster;
    [SerializeField] private float roamInvertPeriod = 30f;
    [SerializeField] private float chaseInvertPeriod = 1f;

    private float randAmount = 1f; // Adding randomness to the period

    private Inverter inverter;
    private MonsterScript monsterScript;

    private State prevState;
    private State curState;
    private float curInvertPeriod = 100f;

    private float timer = 0f;

	// Start is called before the first frame update
	void Start()
    {
        inverter = GetComponent<Inverter>();
        monsterScript = monster.GetComponent<MonsterScript>();
        curState = monsterScript.GetMonsterState();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        prevState = curState;
        curState = monsterScript.GetMonsterState();


        if (curState.ToString() != prevState.ToString()) // When state changes, reset timer and instantly flip if transitioning to chase
        {
            timer = 0f;

            switch (curState.ToString()) // Not great code, but since we only have 3 states it will do
            {
                case "RoamState":
                    curInvertPeriod = roamInvertPeriod;
                    break;
                case "ChaseState":
                    curInvertPeriod = chaseInvertPeriod;
                    inverter.Invert();
                    break;
                case "AttackState":
                    curInvertPeriod = 9999999999f; // hacky shit to prevent inverting when player dies
                    break;
            }
        }

        if (timer >= (curInvertPeriod + Random.Range(-randAmount, randAmount))) // When the timer passes the invert period for the current state, invert
        {
            //inverter.Invert();
            timer = 0f;
        }
    }

    public bool IsInverted()
    {
       return inverter.inverted;
    }
}
