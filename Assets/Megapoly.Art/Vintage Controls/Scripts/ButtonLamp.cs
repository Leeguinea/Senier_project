using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLamp : MonoBehaviour
{
    public enum eColor
    {
            Red,
            Yellow,
            Green,
            Blue,
    }

    public bool lightOn = false;
    public Transform lamp;
    public eColor lightColor;

    Renderer rend;

    ChargeMachine chargeMachine;


    // Start is called before the first frame update
    void Start()
    {
        rend = lamp.GetComponent<Renderer>();

        chargeMachine = FindObjectOfType<ChargeMachine>();
        if (chargeMachine == null)
        {
            lightOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 충전 머신이 없거나 충전머신이 충전중이 아닌 경우
        if (chargeMachine == null || !chargeMachine.isCharging) 
        {
            lightOn = false;
        }
        else
        {
            // If ChargeMachine is found and is charging, set on to true
            lightOn = true;
        }
 
        if (lightOn)
        {
            switch (lightColor)
            {
                case eColor.Red:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f));
                    break;
                case eColor.Yellow:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f));
                    break;
                case eColor.Green:
                    rend.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f));
                    break;
                case eColor.Blue:
                    rend.material.SetColor("_EmissionColor", new Color(0f, 0.33f, 1f, 1f));
                    break;
                default:
                    break;
            }
            
        }
        else
        {
            rend.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }
    }

}
