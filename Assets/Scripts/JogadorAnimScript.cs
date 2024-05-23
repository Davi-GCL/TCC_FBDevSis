using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorAnimScript : MonoBehaviour
{
    public Animator animatorController;

    private bool isPushing = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F)) 
        {
            isPushing = !isPushing;
        }
        animatorController.SetBool("pushing", isPushing);
    }
}
