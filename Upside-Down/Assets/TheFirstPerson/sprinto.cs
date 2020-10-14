using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;
using UnityEngine.UI;

public class sprinto : TFPExtension
{

    //public float jetPower;
    bool limitstamina = true;
    public float stamina;
    public float staminaRegain;
    public float minThreshold;
    public Text staminaGuage;
    public AudioSource jetSound;
    public float soundFadeSpeed;
    float staminaLeft;
    bool sprinting;
    bool regen;
    public float maxVol;

    public override void ExStart(ref TFPData data, TFPInfo info)
    {
        staminaLeft = stamina;
    }

    public override void ExPreMove(ref TFPData data, TFPInfo info)
    {

        if (staminaLeft == 0)
            sprinting = false;
        if (staminaLeft / stamina <= minThreshold && !sprinting)
        {
            data.running = false;
            //info.sprintEnabled = false;
        }

        if (!data.running)
        {
            staminaLeft = Mathf.MoveTowards(staminaLeft, stamina, staminaRegain * Time.deltaTime);
            sprinting = false;
            regen = true;
        }

        if (data.grounded) //(data.grounded || data.slide) &&
        {
            if (data.running && staminaLeft > 0)
            {
                sprinting = true;
                //info.sprintEnabled = true;
                //data.yVel = jetPower;
                if (limitstamina)
                {
                    staminaLeft -= Time.deltaTime;
                    regen = false;
                    if (staminaLeft < 0)
                    {
                        staminaLeft = 0;
                    }
                }
                //jetSound.volume = Mathf.MoveTowards(jetSound.volume, 1.0f, soundFadeSpeed * Time.deltaTime);
                //jetSound.volume = Mathf.MoveTowards(jetSound.volume, 0.0f, soundFadeSpeed * Time.deltaTime);
            }
//            else
//            {
//                
//                staminaLeft = Mathf.MoveTowards(staminaLeft, stamina, staminaRegain * Time.deltaTime);
//            }
        }
        
        else
        {
            sprinting = false;
        }
        if (regen && staminaLeft / stamina <= 0.99)
        {
            //jetSound.volume = Mathf.MoveTowards(jetSound.volume, 0.0f, soundFadeSpeed * Time.deltaTime);
            jetSound.volume = Mathf.MoveTowards(jetSound.volume, maxVol, soundFadeSpeed * Time.deltaTime);
        }
        else
            jetSound.volume = Mathf.MoveTowards(jetSound.volume, 0.0f, soundFadeSpeed * Time.deltaTime);

        if (staminaLeft < stamina && staminaGuage != null)
        {
            staminaGuage.text = "stamina: " + staminaLeft.ToString("F2") + " / " + stamina;
        }
        else if (staminaGuage != null)
        {
            staminaGuage.text = "";
        }
    }
}