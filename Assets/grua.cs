using UnityEngine;

public class grua : MonoBehaviour
{
    public Material materialCuerda;
    public Transform cuerda;
    public Transform objeto;
    public Transform pivoteGrua;
    public Transform BaseGrua;

    public float velGrua = 10f;
    public float vel = 10f;
    public float minZ = 0.75f;
    public float maxZ = 7.25f;
    private float posZ = 0f;

    

    private Vector3 escalaOriginal;
    private float distanciaInicial;

   void Start()
    {
        posZ = transform.position.z;

        if (cuerda != null)
        {
            escalaOriginal = cuerda.localScale;
        }

        if (cuerda != null && BaseGrua != null && objeto != null)
        {
            Vector3 dirInicial = objeto.position - BaseGrua.position;
            distanciaInicial = dirInicial.magnitude;
        }
    }

    void Update()
    {

        // Movimiento de la grua
        float movimientoZ = vel * Time.deltaTime;
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.O) && pos.z + movimientoZ <= maxZ)
        {
            transform.Translate(Vector3.forward * movimientoZ, Space.World);
            posZ += movimientoZ;
        }
        else 
        {
            if (Input.GetKey(KeyCode.L) && pos.z - movimientoZ >= minZ)
            {
                transform.Translate(Vector3.back * movimientoZ, Space.World);
                posZ -= movimientoZ;
            }
                
        }

        // subida y bajada
        if (pivoteGrua != null)
        {
            Vector3 posPivote = pivoteGrua.localPosition;
            float movimiento = velGrua * Time.deltaTime;
            float nuevaY = posPivote.y;

            // Subir
            if (Input.GetKey(KeyCode.I))
            {
                nuevaY -= movimiento;
            }

            // Bajar
            if (Input.GetKey(KeyCode.K))
            {
                nuevaY += movimiento;
            }

            // Limitar
            nuevaY = Mathf.Clamp(nuevaY, -80f, 0f);
            pivoteGrua.localPosition = new Vector3(posPivote.x, nuevaY, posPivote.z);
        }

        // posiciones
        if (objeto != null && pivoteGrua != null)
        {
            objeto.position = pivoteGrua.position;
        }

        ActualizarCuerda();
    }

    private void ActualizarCuerda()
    {
        bool todoOk = (cuerda != null && objeto != null && BaseGrua != null);
        float factorEscala;
        Vector3 direccion = objeto.position - BaseGrua.position;
        float distancia = direccion.magnitude;

        if (todoOk)
        {

            cuerda.position = BaseGrua.position + direccion * 0.5f;

            
            cuerda.rotation = Quaternion.LookRotation(direccion);
            cuerda.Rotate(90f, 0f, 0f);



            if (distanciaInicial > 0)
            {
                factorEscala = distancia / distanciaInicial;
            }
            else
            {
                factorEscala = 1f;
            }

            cuerda.localScale = new Vector3(
                escalaOriginal.x,
                escalaOriginal.y * factorEscala,
                escalaOriginal.z
            );

 
            if (materialCuerda != null)
            {
                materialCuerda.mainTextureScale = new Vector2(1, factorEscala);
            }
        }
      
    }
}