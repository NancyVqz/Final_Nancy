using System.Collections;
using UnityEngine;

namespace Player
{
    public class Handgun : Weapon
    {
        [SerializeField]
        private GameObject bulletImpactPrefab;
        [SerializeField]
        private GameObject shootEffectPrefab;
        [SerializeField]
        private Transform shootEffectPos;

        private void Update()
        {
            Debug.DrawRay(bulletOrigin[0].transform.position, bulletOrigin[0].transform.forward * range, Color.red);
        }

        public override void Shoot()
        {
            Debug.Log("PIUM");

            RaycastHit hit;

            if (Physics.Raycast(bulletOrigin[0].transform.position, bulletOrigin[0].transform.forward, out hit, range, layers))
            {
                //if (hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth target))
                //{
                //    target.Dano();
                //}
                AudioManager.instance.Play("Shoot");
                CreateBulletImpact(hit);
                EffectShoot();
                canShoot = false;
            }
            StartCoroutine(FireRateCooldown());
        }

        public override void Reload()
        {

        }

        public IEnumerator FireRateCooldown()
        {
            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }

        public void TurnLight()
        {
            Debug.Log("Prendiendo luz");
        }

        public void EffectShoot()
        {
            GameObject copia = Instantiate(shootEffectPrefab.gameObject, shootEffectPos.transform.position, transform.rotation);
            Destroy(copia, 1.5f);
        }

        public void CreateBulletImpact(RaycastHit hit)
        {
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 position = hit.point + hit.normal * 0.01f;

            GameObject impact = Instantiate(bulletImpactPrefab, position, rotation);

            Destroy(impact, 5f);
        }
    }
}

