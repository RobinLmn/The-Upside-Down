using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public GameObject player;
    public CharacterController playerCharController;
    public Hider hider;

    private void Awake()
    {
        instance = this;
    }

    public bool IsHiding
    {
        get { return hider.isHiding; }
    }

    public GameObject Player()
    {
        return player;
    }

    public CharacterController PlayerController()
    {
        return playerCharController;
    }

    public float GetCurrentSpeed()
    {
        return playerCharController.velocity.magnitude;
    }
}
