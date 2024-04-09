using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animfire : MonoBehaviour
{

    private float animTime = 0f;
    private Animator animator;
    [SerializeField]
    private float animMaxTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("tick", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animTime < animMaxTime)
        {
            animTime += Time.deltaTime;

        }
        else
        {
            animator.SetBool("tick", true);
        }

    }
}
