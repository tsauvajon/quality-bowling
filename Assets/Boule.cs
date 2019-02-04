using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boule : MonoBehaviour
{
    // Structure interne pour stocker plus proprement une position (position + rotation)
    private struct SauvegardePos {
        public Vector3 pos;
        public Quaternion rot;
    }

    // Caméra principale, qui suit la boule
    public GameObject mainCam;
    // Caméra centrée sur les quilles, pour montrer l'impact
    public GameObject quillesCam;

    public GameObject canvas;

    // Position initiale de la boule
    private Vector3 initialPosition;

    // Position initiale des quilles
    private List<SauvegardePos> initialPositionQuilles = new List<SauvegardePos>();
    private GameObject[] quilles;

    // Est-ce que la boule est déjà lancée ?
    // мяч катится = est-ce que la boule roule (en Russe)
    private bool мячкатится它很漂亮不是吗 = false;

    void Start()
    {
        quilles = GameObject.FindGameObjectsWithTag("Quille");
        initialPosition = transform.position;

        GetComponent<Rigidbody>().maxAngularVelocity = 100;

        // on stocke la position initiale de chaque quille, pour pouvoir les
        // ré-initialiser plus tard
        foreach (var q in quilles)
        {
            var body = q.GetComponent<Rigidbody>().transform;
            initialPositionQuilles.Add(new SauvegardePos {
                pos = body.position,
                rot = body.rotation
            });
        }

        Init();
    }

    void Update()
    {
        // Si la boule n'est pas encore lancée
        if (!мячкатится它很漂亮不是吗) {
            // Appui sur Espace
            if (Input.GetKey(KeyCode.Space)) {
                Lancer();
            }
            
            // Appui sur > et < : décaler la boule sur la droite ou la gauche
            if (Input.GetKey(KeyCode.RightArrow)) {
                Translater(true);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Translater(false);
            }

            // Appui sur page up / page down : donner de l'effet à la boule
            if (Input.GetKey(KeyCode.PageDown)) {
                Effet(true);
            }
            if (Input.GetKey(KeyCode.PageUp)) {
                Effet(false);
            }

            // Appui sur flèche haut / flèche bas : incliner le lancer
            if (Input.GetKey(KeyCode.UpArrow)) {
                Tourner(true);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Tourner(false);
            }
        }

        // active le canvas des consignes uniquement quand la boule n'est pas
        // encore lancée
        canvas.GetComponent<Canvas>().enabled = !мячкатится它很漂亮不是吗;

        // Quand on est aux 3/4 de la piste
        if (transform.position.z > 140) {
            quillesCam.SetActive(true);
            mainCam.SetActive(false);
        }

        var v = GetComponent<Rigidbody>().velocity;

        // Quand le lancer est terminé (la boule est tombée ou ne bouge presque plus)
        if (transform.position.y < -15 || (v.x < .1f && v.y < .1f && v.z < .1f)) {
            Init();
        }
    }

    // Lance la boule vers l'avant : dans le cas où un angle a été donné à la boule,
    // on veut également ajouter une force sur l'axe Z
    void Lancer() {
        var fw = mainCam.transform.forward;

        // on réactive la gravité sur la boule une fois qu'elle est lancée
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(fw.x * 20000f , 800f, fw.z * 20000f));
        мячкатится它很漂亮不是吗 = true;
    }

    // Décale à la fois la boule et la caméra sur l'axe x (sur la largeur de la piste)
    void Translater(bool droite) {
        // boule
        transform.position = new Vector3(
            transform.position.x + (droite ? 0.1f : -0.1f),
            transform.position.y,
            transform.position.z
        );

        // caméra
        mainCam.transform.position = new Vector3(
            mainCam.transform.position.x + (droite ? 0.1f : -0.1f),
            mainCam.transform.position.y,
            mainCam.transform.position.z
        );
    }

    // Donne un angle au lancer (droite vers gauche ou gauche vers droite)
    // Pour le moment, on ne modifie que la caméra : on la décale vers la droite
    // ou la gauche, et on fait en sorte qu'elle regarde toujours vers la boule
    void Tourner(bool droite) {
        mainCam.transform.position = new Vector3(
            mainCam.transform.position.x + (droite ? .01f : -.01f),
            mainCam.transform.position.y,
            mainCam.transform.position.z
        );

        mainCam.transform.LookAt(transform.position);
    }

    // Donne à la boule une vitesse angulaire pour lui donner de l'effet
    void Effet(bool droite) {
        GetComponent<Rigidbody>().AddTorque(0, GetComponent<Rigidbody>().angularVelocity.y + (droite ? 5 : -5), 0);
    }

    // Met le jeu en place pour un lancer
    void Init() {
        мячкатится它很漂亮不是吗 = false;

        ResetQuilles();
        ResetBoule();
        ResetCameras();
    }

    // Remet chacune des quilles dans leur position initiale (position, rotation, vitesse, vitesse angulaire)
    void ResetQuilles() {
        for (int i = 0; i < quilles.Length; i++) {
            var body = quilles[i].GetComponent<Rigidbody>();
            body.position = initialPositionQuilles[i].pos;
            body.rotation = initialPositionQuilles[i].rot;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }

    // Remet à 0 la position, la vitesse, la rotation et la vitesse de rotation
    void ResetBoule() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;

        // on désactive la gravité avant de lancer la boule, pour ne pas qu'elle
        // tombe sur la piste avant le lancement
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Remet les 2 caméras dans leur position et état d'activation initiaux
    void ResetCameras() {
        mainCam.SetActive(true);
        quillesCam.SetActive(false);

        mainCam.transform.position = new Vector3(
            initialPosition.x,
            mainCam.transform.position.y,
            -3.5f
        );
        mainCam.transform.LookAt(transform.position);
    }

    bool LaQuilleEstElleDebout() {

    }
}
