using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShoot;
    [SerializeField] bool debugNextWeapon;
    [SerializeField] bool debugPreviousWeapon;

    [SerializeField] Rig pistolRig;

    FireWeaponBase[] fireWeapons;
    MeleeWeaponBase meleeWeapon;
    int selectedWeaponIndex;

    public UnityEvent onShoot, onSwing;
    public UnityEvent<FireWeaponBase> onChangeWeapon;

    void OnValidate()
    {
        if (debugShoot)
        {
            debugShoot = false;
            Shoot();
        }
        if (debugNextWeapon)
        {
            debugNextWeapon = false;
            SelectNextWeapon();
        }
        if (debugPreviousWeapon)
        {
            debugPreviousWeapon = false;
            SelectPreviousWeapon();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fireWeapons = GetComponentsInChildren<FireWeaponBase>();
        foreach (FireWeaponBase w in fireWeapons)
        {
            w.gameObject.SetActive(false);
            w.weaponCanvas.SetActive(false);
        }
        SelectWeapon(0);

        meleeWeapon = GetComponentInChildren<MeleeWeaponBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - fireWeapons[selectedWeaponIndex].lastShotTime > fireWeapons[selectedWeaponIndex].coolDownBetweenShots)
        {
            fireWeapons[selectedWeaponIndex].canShoot.text = "Yes";
            fireWeapons[selectedWeaponIndex].canShoot.color = Color.green;
        }
    }

    public void Shoot()
    {
        if (Time.time - fireWeapons[selectedWeaponIndex].lastShotTime > fireWeapons[selectedWeaponIndex].coolDownBetweenShots)
        {
            fireWeapons[selectedWeaponIndex].Shoot();
            onShoot.Invoke();
            pistolRig.weight = 1.0f;
        }
    }

    public void StartShooting()
    {
        fireWeapons[selectedWeaponIndex].StartShooting();
    }

    public void StopShooting()
    {
        fireWeapons[selectedWeaponIndex].StopShooting();
    }

    public void Swing()
    {
        meleeWeapon.Swing();
        onSwing.Invoke();
        pistolRig.weight = 0.0f;
    }

    public void OnSwingDamageStart()
    {
        meleeWeapon.SwingDamageStart();
        pistolRig.weight = 0.0f;
    }

    public void OnSwingDamageEnd()
    {
        meleeWeapon.SwingDamageEnd();
        pistolRig.weight = 1.0f;
    }

    public void SelectNextWeapon()
    {
        SelectWeapon(selectedWeaponIndex + 1);
    }

    public void SelectPreviousWeapon()
    {
        SelectWeapon(selectedWeaponIndex - 1);
    }

    public void SelectWeapon(int index)
    {
        fireWeapons[selectedWeaponIndex].gameObject.SetActive(false);
        fireWeapons[selectedWeaponIndex].weaponCanvas.SetActive(false);

        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= fireWeapons.Length)
        {
            selectedWeaponIndex = 0;
        }
        if (selectedWeaponIndex < 0)
        {
            selectedWeaponIndex = fireWeapons.Length - 1;
        }

        fireWeapons[selectedWeaponIndex].gameObject.SetActive(true);
        fireWeapons[selectedWeaponIndex].weaponCanvas.SetActive(true);

        onChangeWeapon.Invoke(fireWeapons[selectedWeaponIndex]);
    }

    public FireWeaponBase.UseMode GetCurrentWeaponUseMode()
    {
        return fireWeapons[selectedWeaponIndex].useMode;
    }
}
