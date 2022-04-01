using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<int, int> { }

public class WeaponAssaultRifle : MonoBehaviour
{
    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //���⼳��

    private float lastAttackTime = 0;
    private bool isReload = false; // ������ ������ üũ

    [Header("�Ѿ�����")]
    [SerializeField]
    private GameObject GunBullet; //�Ѿ�

    [Header("�Ѿ˳�����ġ")]
    [SerializeField]
    private GameObject Bulletspot; //�Ѿ˳�������ġ


    private void Awake()
    {
        // ó�� ź ���� �ִ�� ����
        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
    }

    //�ܺο��� �ʿ��� ������ �����ϱ� ���� ������ Get Property's
    public WeaponName weaponName => weaponSetting.weaponName;

    private void OnEnable()
    {
        //���Ⱑ Ȱ��ȭ�� �� �ش� ������ ź �� ������ �����Ѵ�
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
    }

    public void StartWeaponAction(int type = 0)
    {
        // ������ ���� ���� ���� �׼��� �Ҽ�����
        if (isReload == true) return;

        //���콺 ���� Ŭ��(���� ����)
        if(type == 0)
        {
            //���� ����
            if(weaponSetting.isAutomaticAttack == true)
            {
                StartCoroutine("OnAttackLoop");
            }
            //�ܹ� ����
            else
            {
                OnAttack();
            }
        }
    }

    public void StartReload()
    {
        // ���� ������ ���̸� ������ �Ұ���
        if (isReload == true) return;

        // ���� �׼� ���߿� 'R'Ű�� ���� �������� �õ��ϸ� ���� �׼� ���� �� ������
        StopWeaponAction();

        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
        //StartCoroutine("OnReload");    �ϴ� �ִϸ��̼Ǿ�� �ּ�ó��
    }

    private IEnumerator OnReload()
    {
        isReload = true;

        // ������ �ִϸ��̼�, ���� ���

        while (true)
        {
            // ���� ������� �ƴϰ�, ���� �ִϸ��̼��� MoveMent�̸�
            // ������ �ִϸ��̼�(, ����) ����� ����Ǿ��ٴ� ��
            isReload = false;

            //���� ź���� �ִ�� �����ϰ�, �ٲ� ź �� ������ Text UI�� ������Ʈ
            weaponSetting.currentAmmo = weaponSetting.maxAmmo;
            onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

            yield return null;
        }

    }

    public void StopWeaponAction(int type=0)
    {
        // ���콺 ���� Ŭ��(���� ����)
        if(type == 0)
        {
            StopCoroutine("OnAttackLoop");
        }
    }

    private IEnumerator OnAttackLoop()
    {
        while(true)
        {
            OnAttack();

            yield return new WaitForSeconds(weaponSetting.attackRate);
        }
    }

    public void OnAttack()
    {
        if(Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            //�ٰ����� ���� ������ �� ����

            //�����ֱⰡ �Ǿ�� ������ �� �ֵ��� �ϱ� ���� ���� �ð� ����
            lastAttackTime = Time.time;

        }
        // ź ���� ������ ���� �Ұ���
        if(weaponSetting.currentAmmo <= 0)
        {
            return;
        }

        //�Ѿ� ��ġ
        GameObject bullets = Instantiate(GunBullet, Bulletspot.transform.position, Bulletspot.transform.rotation);


        // ���ݽ� currentAmmo 1 ����
        weaponSetting.currentAmmo--;
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
    }
}
