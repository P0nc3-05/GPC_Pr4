using UnityEngine;
using UnityEngine.InputSystem;

public class Gancho : MonoBehaviour
{
    public Transform pbase;
    public Transform pbrazo;
    public Transform pcodo;
    public Transform ppinza1;
    public Transform ppinza2;

    public float velocidadRotacion = 50f;
    public float velocidadVertical = 2f;

    void Update()
    {
        //PBASE
        if (Input.GetKey(KeyCode.Q))
        {

        }
        if (Input.GetKey(KeyCode.E))
        {

        }
        //PBRAZO
        if (Input.GetKey(KeyCode.Y))
        {
            pbrazo.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
            pcodo.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.H))
        {
            pbrazo.Rotate(0, 0, -(velocidadRotacion * Time.deltaTime));
            pcodo.Rotate(0, 0, -(velocidadRotacion * Time.deltaTime));
        }
        //PGANCHO
        if (Input.GetKey(KeyCode.T))
        {
            transform.Translate(0, 0, -velocidadVertical * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.G))
        {
            transform.Translate(0, 0, velocidadVertical * Time.deltaTime);
        }
        //PPINZAS
        if (Input.GetKey(KeyCode.R))
        {
            ppinza1.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
            ppinza2.Rotate(0, 0, -(velocidadRotacion * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.F))
        {
            ppinza2.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
            ppinza1.Rotate(0, 0, -(velocidadRotacion * Time.deltaTime));

        }
    }

}
