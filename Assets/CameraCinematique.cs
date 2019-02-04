using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Caméra "cinématique" qui s'active lorsque la boule atteint la fin de la piste
// afin d'avoir une vue dramatique
// Cette idée est de moi et tout le monde l'a copiée
public class CameraCinematique : MonoBehaviour
{
    public GameObject boule;

    void Start()
    {
        // désactive la caméra
        enabled = false;
    }

    void Update()
    {
        // la boule atteint environ les 3/4 de la piste
        if (boule.transform.position.z > 140) {
            // on active la caméra cinématique
            enabled = true;
        }
    }
}
