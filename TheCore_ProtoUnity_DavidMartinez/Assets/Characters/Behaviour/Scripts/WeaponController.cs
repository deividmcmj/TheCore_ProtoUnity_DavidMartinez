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

    WeaponBase[] weapons;
    int selectedWeaponIndex;

    public UnityEvent onShoot, onSwing;
    public UnityEvent<WeaponBase> onChangeWeapon;

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
        weapons = GetComponentsInChildren<WeaponBase>();
        foreach (WeaponBase w in weapons)
        {
            w.gameObject.SetActive(false);
        }
        SelectWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        ((FireWeaponBase)weapons[selectedWeaponIndex]).Shoot();
        onShoot.Invoke();
    }

    public void StartShooting()
    {
        ((FireWeaponBase)weapons[selectedWeaponIndex]).StartShooting();
    }

    public void StopShooting()
    {
        ((FireWeaponBase)weapons[selectedWeaponIndex]).StopShooting();
    }

    public void Swing()
    {
        ((MeleeWeaponBase)weapons[selectedWeaponIndex]).Swing();
        onSwing.Invoke();
    }

    public void OnSwingDamageStart()
    {
        ((MeleeWeaponBase)weapons[selectedWeaponIndex]).SwingDamageStart();
    }

    public void OnSwingDamageEnd()
    {
        ((MeleeWeaponBase)weapons[selectedWeaponIndex]).SwingDamageEnd();
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
        weapons[selectedWeaponIndex].gameObject.SetActive(false);

        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weapons.Length)
        {
            selectedWeaponIndex = 0;
        }
        if (selectedWeaponIndex < 0)
        {
            selectedWeaponIndex = weapons.Length - 1;
        }

        weapons[selectedWeaponIndex].gameObject.SetActive(true);

        onChangeWeapon.Invoke(weapons[selectedWeaponIndex]);
    }

    public WeaponBase.UseMode GetCurrentWeaponUseMode()
    {
        return weapons[selectedWeaponIndex].useMode;
    }
}
