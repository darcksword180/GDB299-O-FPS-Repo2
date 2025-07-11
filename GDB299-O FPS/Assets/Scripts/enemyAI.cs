using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour, IDamage
{
    //Reference model
    [SerializeField] Renderer model;

    //Enemy health variable
    [SerializeField] int HP;

    //color refrence
    Color colorOriginal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //link color to model default
        colorOriginal = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IDamage reference
    public void takeDamage(int amount)
    {
        //Damages health by damage amount
        HP -= amount;

        //check if model is destroyed if no HP or flashes red if they do have any remaining
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        //flashes model red then reverts to original
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOriginal;
    }
}
