using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player //al hacer dos namespaces con variables iguales se le pone el namespace respectivo a los demas tambien 
{
    public class Shotgun : Weapon
    {
        public override void Shoot()
        {
            Debug.Log("SPIUSH");
        }

        public override void Reload()
        {

        }
        public void FlameThrower()
        {
            Debug.Log("Lanzando fuego");
        }
    }
}

