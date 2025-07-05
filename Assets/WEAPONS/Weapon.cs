using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public abstract class Weapon : MonoBehaviour
    {
        public string weapon;

        public int damage; // daño del arma
        public float range; // alcance del arma
        public float fireRate; // cadencia del arma (cooldown)
        public float accuracy; // punteria: que tanto se mueve el arma o dispara hacia donde apunta

        public int currentAmmo; // municion del cargador actual
        public int currentMaxAmmo; //capacidad maxima del cargador
        public int ammo; // municion disponible en la reserva
        public int maxAmmo; // capacidad maxima de mi reserva

        public Transform[] bulletOrigin; //La posicion 0 seria para armas de 1 solo impacto
        public LayerMask layers;
        public bool canShoot;

        public abstract void Shoot(); // estos son abstractos para despues hacerles el override en cada arma para que hagan algo en especifico

        public abstract void Reload();

        public bool CheckAmmo() //este es un metodo normal porque no cambia para las demas armas
        {
            return currentAmmo <= 0 && ammo <= 0;
        }
    }
}


