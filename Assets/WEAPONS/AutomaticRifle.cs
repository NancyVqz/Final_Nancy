using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AutomaticRifle : Weapon
    {
        public override void Shoot()
        {
            Debug.Log("TATATATATA");
        }

        public override void Reload()
        {

        }

        public void GranadeLaunch()
        {
            Debug.Log("Se lanzó granada");
        }
    }
}

