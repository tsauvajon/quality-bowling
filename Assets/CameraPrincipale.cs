using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D'après une idée originale de Alexandre Journet
// (caméra qui suit programmatiquement la boule)
public class CameraPrincipale : MonoBehaviour
{
    public GameObject boule;
    private float offsetZ;

    void Start()
    {
        // on stocke le décalage initial entre la boule et la caméra pour le
        // garder tout au long du jeu
        offsetZ = transform.position.z - boule.transform.position.z;
    }

    // Utilisation de LateUpdate pour bouger la caméra toujours après la boule
    void LateUpdate()
    {
        // on fait suivre la boule par la caméra
        // (sur l'axe Z soit la longueur de la piste)
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            boule.transform.position.z + offsetZ
        );
    }
}
