using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertManager : MonoBehaviour
{
    public GameObject monster;
    [SerializeField] private float roamInvertPeriod = 6f;
    [SerializeField] private float searchInvertPeriod = 1f;
    [SerializeField] private float attackInvertPeriod = 3f;

    private float randAmount = 1f; // Adding randomness to the period

    private Inverter inverter;
    private MonsterWorldObject monsterWorldObject;

    private State curState;
    private float curInvertPeriod;

	// Start is called before the first frame update
	void Start()
    {
        inverter = GetComponent<Inverter>();
        monsterWorldObject = monster.GetComponent<MonsterWorldObject>();
        curState = monsterWorldObject.GetMonsterState();

        StartCoroutine(InvertWithPeriod());
    }

    // Update is called once per frame
    void Update()
    {
        curState = monsterWorldObject.GetMonsterState();

        switch (curState.ToString()) // Not great code, but since we only have 3 states it will do
        {
            case "RoamState":
                curInvertPeriod = roamInvertPeriod;
                break;
            case "SearchState":
                curInvertPeriod = searchInvertPeriod;
                break;
            case "AttackState":
                curInvertPeriod = attackInvertPeriod;
                break;
        }
    }

    private IEnumerator InvertWithPeriod()
    {
        while (true)
        {
            yield return new WaitForSeconds(curInvertPeriod + Random.Range(-randAmount, randAmount));
			//inverter.Invert();
		}
    }

    public bool IsInverted()
    {
       return inverter.inverted;
    }
}
