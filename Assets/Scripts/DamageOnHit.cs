using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] float dmg = 1.0f;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        var character = collision.gameObject.GetComponent<BasicCharacter>();
<<<<<<< HEAD
<<<<<<< HEAD
        if (character) 
        {
            character.Health -= dmg;
            character.CheckHealth();

        }
=======
        if (character) character.Health -= dmg;
>>>>>>> parent of 518d20d (Fixed some stuff and changed a few others)
=======
        if (character) { 
            character.Health -= dmg;
            character.CheckHP();
        }
        
>>>>>>> parent of 4354a6c (Revert "Fixed some stuff and changed a few others")
        //GameObject.Destroy(this);
    }
}
