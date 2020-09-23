using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    Rigidbody _RB;

    [SerializeField]
    InputMaster _IM;

    // Start is called before the first frame update
    void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        _IM = new InputMaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
