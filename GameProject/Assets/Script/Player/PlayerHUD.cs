using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private WeaponAssaultRifle weapon;      //���� ������ ��µǴ� ����

    [Header("Weapon Base")]
    [SerializeField]
    private Text textWeaponName; //���� �̸�
    [SerializeField]
    private Image imageWeaponIcon; //���� ������
    [SerializeField]
    private Sprite[] spriteWeaponIcons; //���� �����ܿ� ���Ǵ� sprite �迭

    [Header("Ammo")]
    [SerializeField]
    private Text textAmmo; //���� / �ִ� ź �� ��� text

    private void Awake()
    {
        SetupWeapon();

        // �޼ҵ尡 ��ϵǾ� �ִ� �̺�Ʈ Ŭ����(weapon.xx)��
        // Invoke() �޼ҵ尡 ȣ��� �� ��ϵ� �޼ҵ�(�Ű�����)�� ����ȴ�
        weapon.onAmmoEvent.AddListener(UpdateAmmoHUD);
    }

    private void SetupWeapon()
    {
        textWeaponName.text = weapon.weaponName.ToString();
        imageWeaponIcon.sprite = spriteWeaponIcons[(int)weapon.weaponName];
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
    }
}
