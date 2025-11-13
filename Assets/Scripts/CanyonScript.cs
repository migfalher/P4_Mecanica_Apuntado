using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonScript : MonoBehaviour
{
    // Public Attributes
    public GameObject ammo;
    public float force;
    public List<Material> matList;

    // Private Components
    private GameObject spawn;
    private GameObject tube;
    private GameObject cross;
    private GameObject rotor;
    private GameObject buton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find components
        spawn = GameObject.Find("Canyon_Spawn");
        rotor = GameObject.Find("Canyon_Rotor");
        tube = GameObject.Find("Canyon_Tubo");
        cross = GameObject.Find("Canyon_Cruz");
        buton = GameObject.Find("Canyon_Boton_Cylinder");
        Debug.Log(buton);
        // if list is nt empty, apply fist material tu the tube
        if (matList.Count > 0) { tube.GetComponent<Renderer>().material = matList[0]; }
        // make the rootor aim at the cross
        rotor.transform.LookAt(cross.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rotor.transform.LookAt(cross.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            DetectClick();
        }
    }

    // Method detect mouse click
    private void DetectClick()
    {
        // Get click variables
        // -> 'hit' is a sentinel to detect if the ray has touched a collider
        // -> 'ray' is thrown from the camera core to the place where the mouse "would be" in the world
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // has the ray even touched any collider?
        if (Physics.Raycast(ray, out hit))
        {
            // if so:
            // activate event of clicked object
            GameObject coll = hit.transform.gameObject;
            string name = coll.transform.name;
            if (name.Equals(buton.name))
            {
                ShootAmmo();
            }
            else
            {
                Debug.Log("'Canyon' detected the click, but you didn't press the button.");
            }
        }
    }


    // Method shoot ammo
    private void ShootAmmo()
    {
        // Instantiate prefab
        GameObject bullet = Instantiate(
            ammo,
            spawn.transform.position,
            spawn.transform.rotation
        );

        // Get Rigidbody and local axis
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 localAxis = (cross.transform.position) - (spawn.transform.position);
        if (rb != null)
        {
            // Apply force to Rigidbody
            Vector3 bulletForce = localAxis * force * bullet.GetComponent<Rigidbody>().mass;
            rb.AddForce(bulletForce, ForceMode.Impulse);
        }

        // Change color
        StartCoroutine(this.ChangeColor());
    }

    // Method shoot ammo randomly
    private void ShootRandomly()
    {
        // Instantiate prefab
        GameObject bullet = Instantiate(
            ammo,
            spawn.transform.position,
            spawn.transform.rotation
        );

        // Apply random scale
        bullet.transform.localScale = new Vector3(
            UnityEngine.Random.Range(0.5f, 5.0f),
            UnityEngine.Random.Range(0.5f, 5.0f),
            UnityEngine.Random.Range(0.5f, 5.0f)
        );

        // If more than one available, apply random material
        if (matList.Count > 1)
        {
            int index = UnityEngine.Random.Range(0, matList.Count);
            bullet.GetComponent<Renderer>().material = matList[index];
        }
        else if (matList.Count > 0)
        {
            bullet.GetComponent<Renderer>().material = matList[0];
        }

        // Get Rigidbody and local Z axis
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 localAxis = (cross.transform.position) - (spawn.transform.position);
        if (rb != null)
        {
            // Apply force to Rigidbody
            Vector3 bulletForce = localAxis* UnityEngine.Random.Range(0.5f, 5.0f) * bullet.GetComponent<Rigidbody>().mass;
            rb.AddForce(bulletForce, ForceMode.Impulse);
        }

        // Change color
        StartCoroutine( this.ChangeColor() );
    }

    // Method change cannon color
    private IEnumerator ChangeColor()
    {
        tube.GetComponent<Renderer>().material = matList[matList.Count - 1];
        yield return new WaitForSeconds( 0.2f );
        tube.GetComponent<Renderer>().material = matList[0];
    }

}
