using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonOn : MonoBehaviour
{
    public GameObject onButton1;
    public GameObject onButton2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onButton1.SetActive(onButton1.activeSelf ? false : true);
            onButton2.SetActive(onButton2.activeSelf ? false : true);
        }
    }
}
