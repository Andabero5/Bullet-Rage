using UnityEngine;

namespace Nokobot.Assets.Crossbow
{
    public class CrossbowShoot : MonoBehaviour
    {
        public GameObject arrowPrefab;
        public Transform arrowLocation;
        public GameObject gun;
        private GunLogic gunLogic;

        public float shotPower = 50000f;

        void Start()
        {
            if (arrowLocation == null)
                arrowLocation = transform;

            gunLogic = gun.GetComponent<GunLogic>();
        }

        void Update()
        {
            if(Input.GetButtonDown("Fire1") && gunLogic.bulletsInMag > 0)
            {
                Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotPower);
            }
        }
    }
}
