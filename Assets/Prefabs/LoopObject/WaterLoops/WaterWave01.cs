using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave01 : MonoBehaviour
{
    private Animator anim;
    int randNum;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("Loop") == false)
        {
            randNum = Random.Range(8, 12);
            StartCoroutine(LoopCo(randNum));
        }
    }

    private IEnumerator LoopCo(int num)
    {
        anim.SetBool("Loop", true);
        yield return new WaitForSeconds(num);
        anim.SetBool("Loop", false);
    }
}

