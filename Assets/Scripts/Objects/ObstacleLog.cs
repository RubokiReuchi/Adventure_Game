using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLog : MonoBehaviour
{
    private Animator anim;
    public AudioSource fireFx;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smashed(AttackType attackType)
    {
        if (attackType == AttackType.fire)
        {
            fireFx.Play();
            anim.SetBool("Break", true);
            StartCoroutine(breakCo());
        }
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}
