using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FullAuto : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public float ammunition = 30f;
    public float fireRate = 0.1f;
    [SerializeField] AudioClip gunshot;

    private XRGrabInteractableTwoAttach interactor;
    private bool isFiring = false;
    private float timer = 0f;

    void Start()
    {
        interactor = GetComponent<XRGrabInteractableTwoAttach>();
        //interactor.activated.AddListener(StartFiring);
        //interactor.selectExited.AddListener(StopFiring);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            ammunition = 30f;
        }
    }

    void Update()
    {
        if (isFiring)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                FireBullet();
                timer = 0f;
            }
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void FireBullet()
    {
        if (ammunition > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(gunshot, 0.5f);
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5f);
            ammunition--;
        }
    }
}
