using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunLogic[] guns;
    private int CurrentGunIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGunChange();
    }

    void ChangeCurrentGun() 
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        guns[CurrentGunIndex].gameObject.SetActive(true);
    }

    void CheckGunChange()
    {
        float MouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if(MouseWheel > 0f)
        {
            SelectPreviousGun();
        } 
        else if (MouseWheel < 0f)
        {
            SelectNextGun();
        }
        guns[CurrentGunIndex].reloading = false;
        guns[CurrentGunIndex].noShootTime = false;
    }

    void SelectPreviousGun()
    {
        if(CurrentGunIndex == 0)
        {
            CurrentGunIndex = guns.Length - 1;
        }
        else
        {
            CurrentGunIndex--;
        }
        ChangeCurrentGun();
    }

    void SelectNextGun()
    {
        if (CurrentGunIndex >= (guns.Length - 1))
        {
            CurrentGunIndex = 0;
        }
        else
        {
            CurrentGunIndex++;
        }
        ChangeCurrentGun();
    }
}
