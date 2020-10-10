using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private bool isHiding = false;

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public bool IsHiding
    {
        get { return isHiding; }
        set { isHiding = value; }
    }
}
