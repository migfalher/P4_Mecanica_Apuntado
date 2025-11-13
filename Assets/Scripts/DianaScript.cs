using UnityEngine;

public class DianaScript : MonoBehaviour
{
    // Public components
    public Material newMaterial;

    // Private attributes
    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method 
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bala")
        {
            count++;
            switch (count)
            {
                case 1:
                    if (newMaterial != null)
                    {
                        this.GetComponent<Renderer>().material.color = newMaterial.color;
                    }
                    else
                    {
                        this.GetComponent<Renderer>().material.color = Color.red;
                    }
                    break;
                case 2:
                    this.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }
}
