using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    //[SerializeField] AnimatorOverrideController weaponAnimatorOverride = null; //kun on omat animaatiot
    [SerializeField] GameObject equippedWeaponPrefab = null;
    [SerializeField] float weaponDamage = 5f;


    const string weaponName = "Weapon";
    public void Spawn(Transform holdingpos)
       
        //public void SpawnWeapon(Transform handTransform, Animator animator) //kun on animaatiot.. eri aseille omat
    {
        DestroyOldWeapon(holdingpos);

        Debug.Log("Spawnataan asetta");
        if(equippedWeaponPrefab != null)
        {
            GameObject weapon = Instantiate(equippedWeaponPrefab, holdingpos);
            weapon.name = weaponName;
        }
        

        //animator.runtimeController = weaponAnimatorOverride; //jos/kun aseille on omat hyokkianimaatiot
    }
    public float GetDamage()
    {
        return weaponDamage; 
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
}