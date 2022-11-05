using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    //[SerializeField] AnimatorOverrideController weaponAnimatorOverride = null; //kun on omat animaatiot
    [SerializeField] GameObject equippedWeaponPrefab = null;
    [SerializeField] float weaponDamage = 5f;

    public void Spawn(Transform holdingpos)
       
        //public void SpawnWeapon(Transform handTransform, Animator animator) //kun on animaatiot.. eri aseille omat
    {
        Debug.Log("Spawnataan asetta");
        if(equippedWeaponPrefab != null)
        {
            Instantiate(equippedWeaponPrefab, holdingpos);
        }
        

        //animator.runtimeController = weaponAnimatorOverride; //jos/kun aseille on omat hyokkianimaatiot
    }
    public float GetDamage()
    {
        return weaponDamage; 
    }
}