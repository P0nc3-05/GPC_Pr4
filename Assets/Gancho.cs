using UnityEngine;
using UnityEngine.InputSystem;

public class Gancho : MonoBehaviour
{
    public Transform pbase;
    public Transform pbrazo;
    public Transform pcodo;
    public Transform ppinza1;
    public Transform ppinza2;
    public Transform pganchos;

    public float velocidadRotacion = 50f;
    public float velocidadVertical = 2f;

    private float rotacion = 0f;

    void Update()
    {
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
            rotacion += velocidadRotacion * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.F))
        {
            rotacion -= velocidadRotacion * Time.deltaTime;

        }
        rotacion = Mathf.Clamp(rotacion, -25f, 90f);
        ppinza1.localRotation = Quaternion.AngleAxis(rotacion, Vector3.forward);
        ppinza2.localRotation = Quaternion.AngleAxis(-rotacion, Vector3.forward);

        pganchos.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
    }

}
