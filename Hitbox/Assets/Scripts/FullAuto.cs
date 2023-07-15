using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FullAuto : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public float ammunition = 30;
    public float fireRate = 0.3f;
    [SerializeField]
    AudioClip gunshot;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(fullAuto);
    }

  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            ammunition = 30;
        }
    }
    public void fullAuto(ActivateEventArgs arg)
    {
        float timer = 0;
        while (ammunition > 0)
        {

            timer += (10.0f * Time.deltaTime);
            if (timer > fireRate)
            {
                GameObject spawnedBullet = Instantiate(bullet);
                spawnedBullet.transform.position = spawnPoint.position;
                spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
                timer = 0;
                --ammunition;
            }
        }
    }
}
