using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAuto : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public float ammunition = 30f;
    public float fireRate = 0.1f;
    [SerializeField] AudioClip gunshot;

    private bool isFiring = false;
    private bool isReloading = false;
    private float timer = 0f;

    private MagRefill currentMagazine;

    void Update()
    {
        if (isFiring && ammunition > 0)
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
        else
        {
            Debug.Log("Out of ammo. Insert a new magazine.");
            Reload();
        }
    }

    public void Reload()
    {
        if (currentMagazine != null && !isReloading)
        {
            Debug.Log("Reloading...");
            isReloading = true;
            StartCoroutine(EnableMagazineRenderer()); // Coroutine to re-enable the magazine's renderer after a short delay
            RefillAmmo(currentMagazine.ammoAmount);
        }
    }

    public void RefillAmmo(int amount)
    {
        ammunition += amount;
        ammunition = Mathf.Clamp(ammunition, 0, 30);
        Debug.Log("Ammo refilled. Current ammo: " + ammunition);
    }

    private IEnumerator EnableMagazineRenderer()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay time if needed
        currentMagazine.SetVisible(true); // Enable the magazine's renderer after the delay
        isReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mag"))
        {
            currentMagazine = other.GetComponent<MagRefill>();
            if (currentMagazine != null)
            {
                currentMagazine.SetVisible(false); // Temporarily hide the magazine's renderer when inserted
                Reload();
            }
        }
    }
}
