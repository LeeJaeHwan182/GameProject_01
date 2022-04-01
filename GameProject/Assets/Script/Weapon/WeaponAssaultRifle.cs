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
    private WeaponSetting weaponSetting; //무기설정

    private float lastAttackTime = 0;
    private bool isReload = false; // 재장전 중인지 체크

    [Header("총알종류")]
    [SerializeField]
    private GameObject GunBullet; //총알

    [Header("총알나올위치")]
    [SerializeField]
    private GameObject Bulletspot; //총알나오는위치


    private void Awake()
    {
        // 처음 탄 수는 최대로 설정
        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
    }

    //외부에서 필요한 정보를 열람하기 위해 정의한 Get Property's
    public WeaponName weaponName => weaponSetting.weaponName;

    private void OnEnable()
    {
        //무기가 활성화될 때 해당 무기의 탄 수 정보를 갱신한다
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
    }

    public void StartWeaponAction(int type = 0)
    {
        // 재장전 중일 때는 무기 액션을 할수없다
        if (isReload == true) return;

        //마우스 왼쪽 클릭(공격 시작)
        if(type == 0)
        {
            //연속 공격
            if(weaponSetting.isAutomaticAttack == true)
            {
                StartCoroutine("OnAttackLoop");
            }
            //단발 공격
            else
            {
                OnAttack();
            }
        }
    }

    public void StartReload()
    {
        // 현재 재장전 중이면 재장전 불가능
        if (isReload == true) return;

        // 무기 액션 도중에 'R'키를 눌러 재장전을 시도하면 무기 액션 종료 후 재장전
        StopWeaponAction();

        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
        //StartCoroutine("OnReload");    일단 애니메이션없어서 주석처리
    }

    private IEnumerator OnReload()
    {
        isReload = true;

        // 재장전 애니메이션, 사운드 재생

        while (true)
        {
            // 사운드 재생중이 아니고, 현재 애니메이션이 MoveMent이면
            // 재장전 애니메이션(, 사운드) 재생이 종료되었다는 뜻
            isReload = false;

            //현재 탄수를 최대로 설정하고, 바뀐 탄 수 정보를 Text UI에 업데이트
            weaponSetting.currentAmmo = weaponSetting.maxAmmo;
            onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

            yield return null;
        }

    }

    public void StopWeaponAction(int type=0)
    {
        // 마우스 왼쪽 클릭(공격 종료)
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
            //뛰고있을 때는 공격할 수 없다

            //공격주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

        }
        // 탄 수가 없으면 공격 불가능
        if(weaponSetting.currentAmmo <= 0)
        {
            return;
        }

        //총알 설치
        GameObject bullets = Instantiate(GunBullet, Bulletspot.transform.position, Bulletspot.transform.rotation);


        // 공격시 currentAmmo 1 감소
        weaponSetting.currentAmmo--;
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
    }
}
