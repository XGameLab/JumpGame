using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    Animator anim;
    private bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("PlayerOn");
            isPlayer = true;
        }

        if (isPlayer)
        {
            anim.SetTrigger("PlayerOff");
            isPlayer = false;
        }
    }
}
