using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagRefill : MonoBehaviour
{
    public int ammoAmount = 30; // The amount of ammo the magazine refills
    public Renderer magRenderer; // Reference to the magazine's renderer component

    // Start is called before the first frame update
    void Start()
    {
        if (magRenderer == null)
        {
            magRenderer = GetComponent<Renderer>(); // If the reference is not set, get it from the component
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to set the magazine's visibility
    public void SetVisible(bool isVisible)
    {
        if (magRenderer != null)
        {
            magRenderer.enabled = isVisible;
        }
    }
}