using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject boule;
    private float offsetZ;

    void Start()
    {
        // on stocke le décalage initial entre la boule et la caméra pour le garder tout au long du jeu
        offsetZ = transform.position.z - boule.transform.position.z;
    }

    void Update()
    {
        // on fait suivre la boule par la caméra (sur l'axe Z soit la longueur de la piste)
        transform.position = new Vector3(transform.position.x, transform.position.y, boule.transform.position.z + offsetZ);
    }
}
