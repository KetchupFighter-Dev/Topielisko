using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour

    //weapons[1].found = true;  // odkryj broñ nr 2 (topór)
{
    public Weapon[] weapons;
    private Weapon currentWeapon;

    [Header("UI")]
    public Image[] weaponSlots;
    public Color normalColor = Color.gray;
    public Color selectedColor = Color.green;

    // Broñ podstawowa (np. piêœæ)
    public Weapon defaultWeapon;

    void Start()
    {
        SwitchWeapon(0);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwitchWeapon(0);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwitchWeapon(1);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SwitchWeapon(2);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (currentWeapon != null)
                currentWeapon.Attack();
        }
    }

    void SwitchWeapon(int index)
    {
        // Ukryj poprzedni¹ broñ (jeœli by³a)
        if (currentWeapon != null)
            currentWeapon.SetVisible(false);

        Weapon selected = weapons[index];

        if (selected.found)
        {
            // Broñ odkryta - pokaz j¹
            selected.SetVisible(true);
            currentWeapon = selected;
        }
        else
        {
            // Broñ nieodkryta - pokaz broñ podstawow¹
            if (defaultWeapon != null)
                defaultWeapon.SetVisible(true);
            currentWeapon = defaultWeapon;
        }

        UpdateUI(index);
        Debug.Log("Switched to: " + selected.weaponName + " | Found: " + selected.found);
    }

    void UpdateUI(int selectedIndex)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
            weaponSlots[i].color = (i == selectedIndex) ? selectedColor : normalColor;
    }
}