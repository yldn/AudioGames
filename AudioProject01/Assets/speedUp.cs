using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class speedUp : MonoBehaviour
{
    ParticleSystem pc;
    FootStep foot;
    // Start is called before the first frame update
    private bool isSpeedUp ;
    void Start()
    {
        pc = this.GetComponent<ParticleSystem>();
        foot = this.transform.parent.gameObject.GetComponent<FootStep>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSpeedUp = true;
        }
        else
            isSpeedUp = false;

            Speedup();
    }
    void Speedup()
    {
            if (isSpeedUp)
            {
                if (foot.isPlayingFootStep)
                {
                    pc.Play(true);
                }
                this.transform.parent.gameObject.GetComponent<RigidbodyThirdPersonController>().movementSettings.ForwardSpeed = 9;
            }
            else
            {
                this.transform.parent.gameObject.GetComponent<RigidbodyThirdPersonController>().movementSettings.ForwardSpeed = 2.5f;
            }
            
    }



}

