using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Material lens;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LightOn() 
    {
        lens.EnableKeyword("_EMISSION");
        light.enabled = true;
    } 
    public void LightOff() 
    { 
        lens.DisableKeyword("_EMISSION");
        light.enabled = false;
    }
}
