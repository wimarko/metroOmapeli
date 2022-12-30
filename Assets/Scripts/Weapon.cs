using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class Weapon : ScriptableObject, IWeapon
{
    //[SerializeField] AnimatorOverrideController weaponAnimatorOverride = null; //kun on omat animaatiot
    [SerializeField] GameObject equippedWeaponPrefab = null;
    //[SerializeField] float weaponDamage = 5f;
    [SerializeField] Projectile projectile = null;
    [SerializeField] float fireDelay = 1f;
    private float timeToReady;

    public GameObject launchpos = null;
 

    const string weaponName = "Weapon";

    public void Spawn(Transform holdingpos)
       
        //public void SpawnWeapon(Transform handTransform, Animator animator) //kun on animaatiot.. eri aseille omat
    {
        DestroyOldWeapon(holdingpos);

        Debug.Log("Spawnataan asetta");
        if(equippedWeaponPrefab != null)
        {

            GameObject weapon = Instantiate(equippedWeaponPrefab, holdingpos);
            FindObjectOfType<PlayerController>().SetRateOfFire(fireDelay);
            weapon.name = weaponName;
        }
        

        //animator.runtimeController = weaponAnimatorOverride; //jos/kun aseille on omat hyokkianimaatiot
    }



    private void DestroyOldWeapon(Transform weaponpos)
    {
        

        Transform oldWeapon = weaponpos.Find(weaponName);
        if(oldWeapon == null)
        {
            return;
        }

        oldWeapon.name = "DESTROO";  //jotain toimivuuden kannalta
        Destroy(oldWeapon.gameObject);

    }

    public void LaunchProjectile (Transform pos, float damageMultiplier)
    {
            Projectile ammoInstance = Instantiate(projectile, pos.position, pos.rotation);
   
    }
}