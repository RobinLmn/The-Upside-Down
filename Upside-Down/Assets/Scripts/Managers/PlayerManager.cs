using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private bool isHiding = false;

    public static PlayerManager instance;

    public GameObject player;
    public CharacterController playerCharController;

    private void Awake()
    {
        instance = this;
    }

    public bool IsHiding
    {
        get { return isHiding; }
        set { isHiding = value; }
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
