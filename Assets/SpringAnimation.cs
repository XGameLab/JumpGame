using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnimation : MonoBehaviour
{
    Animator anim;
    private bool isPlayer = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            isPlayer = true;
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (isPlayer)
        {
            anim.SetTrigger("PlayerOff");
            isPlayer = false;
        }
    }
}
