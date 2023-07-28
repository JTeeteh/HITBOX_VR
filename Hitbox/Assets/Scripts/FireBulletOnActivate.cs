using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public float ammunition = 10;
    public float fireRate = 0.3f;
    [SerializeField] AudioClip gunshot;
    private MagRefill currentMagazine;
    private bool isReloading = false;

    private bool canFire = true; // Flag to control semi-automatic firing

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (canFire && ammunition > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(gunshot, 0.5f);
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5);
            --ammunition;

            canFire = false; // Disable firing until the next shot is ready
            StartCoroutine(EnableFiring());
        }
    }

    private IEnumerator EnableFiring()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true; // Re-enable firing after the specified fire rate delay
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
    private IEnumerator EnableMagazineRenderer()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay time if needed
        currentMagazine.SetVisible(true); // Enable the magazine's renderer after the delay
        isReloading = false;
    }
    public void RefillAmmo(int amount)
    {
        ammunition += amount;
        ammunition = Mathf.Clamp(ammunition, 0, 30);
        Debug.Log("Ammo refilled. Current ammo: " + ammunition);
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
