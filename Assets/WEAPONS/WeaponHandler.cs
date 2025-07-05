using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // Este script debe de:
    //            TAREA
    // 1.- Cambiar de arma, por lo que debe de haber minimo 2 armas. (El cambio de arma se debe de realizar usando la rueda del mouse.
    //           EJERCICIO
    // 2.- Segun el arma equipada debe de disparar segun su funcion.

    // 3.- Segun el arma equipada debe de recargar de la manera correspondiente.
    // 4.- Al cambiar de arma, debe de aparecer en pantalla la municion de esa area.
    //una variable tipo var puede transformarse a cualquier tipo (string, int, bool, float) pero solo se puede usar dentro de un metodo

    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;
        [SerializeField] private Weapon actualWeapon;

        [SerializeField] private int currentWeaponIndex = 0;

        //Action es una variable donde se pueden guardar metodos
        private Action Shoot;
        private Action SpecialAction;

        private void Start()
        {
            //Se guarda el metodo en el action shoot
            Shoot = AutomaticShoot;

            currentWeaponIndex = 0;
            actualWeapon = weapons[currentWeaponIndex];
            TypeShoot();
        }

        private void Update()
        {
            Shoot(); //el switch le va a dar el metodo correcpondiente al tipo de arma
            ActualWeaponSwitch();
            TriggerSpecialAction();
        }

        private void TypeShoot()
        {
            switch (actualWeapon)
            {
                //lo que el case entiende es que es un weapon, no automatic rifle 
                case AutomaticRifle automaticRifle: //poniendole una nueva variable te saca las propiedades de la AutomaticRifle
                    {
                        Shoot = AutomaticShoot;
                        SpecialAction = automaticRifle.GranadeLaunch;
                        break;
                    }
                case Handgun handgun:
                    {
                        Shoot = SemiAutomaticShoot;
                        SpecialAction = handgun.TurnLight;
                        break;
                    }
                case Shotgun shotgun:
                    {
                        Shoot = SemiAutomaticShoot;
                        SpecialAction = shotgun.FlameThrower;
                        break;
                    }
            }
        }

        private void ActualWeaponSwitch()
        {
            if (Scroll() > 0) 
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
                actualWeapon = weapons[currentWeaponIndex];
                TypeShoot();

                //currentWeaponIndex++;
                //currentWeaponIndex = currentWeaponIndex >= weapons.Length ? 0 : currentWeaponIndex;
                //actualWeapon = weapons[currentWeaponIndex];
                //TypeShoot();

            }
            else if (Scroll() < 0)
            {
                currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
                actualWeapon = weapons[currentWeaponIndex];
                TypeShoot();

                //El maximo espacio al que puedes acceder a un arreglo se define por su tamaño - 1, (si el arreglo es de 5, solo puedo acceder a 4 porque se cuenta desde 0)
                //currentWeaponIndex--;
                //currentWeaponIndex = currentWeaponIndex <= 0 ? weapons.Length - 1 : currentWeaponIndex;
                //actualWeapon = weapons[currentWeaponIndex];
                //TypeShoot();
            }
        }

        private void AutomaticShoot()
        {
            if(actualWeapon.CheckAmmo() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Arma tipo Automatica");
                actualWeapon.Shoot();
            }
        }

        private void SemiAutomaticShoot()
        {
            if (actualWeapon.CheckAmmo() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Arma tipo Semi-Automatica");
                actualWeapon.Shoot();
            }
        }

        private int Scroll()
        {
            float input = Input.GetAxis("Mouse ScrollWheel");
            return input == 0 ? 0 : input > 0 ? 1 : -1; //hacer un if para regresar en numeros enteros un 1 o -1
        }

        private void TriggerSpecialAction()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SpecialAction();
            }
        }
    }
}

