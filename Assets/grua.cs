using UnityEngine;

public class grua : MonoBehaviour
{
   
    public float speed = 5f;
    public float minZ = 0.75f;
    public float maxZ = 7.25f;
    private float posicionZ = 0f;

   
    public float velocidadGancho = 10f;

    public Transform cuerda;
    public Transform objetoCuelga;
    public Transform pivoteGancho;
    public Transform puntoAnclajeSuperior;
    public Material materialCuerda;

    private Vector3 escalaOriginal;
    private float distanciaInicial;

    void Start()
    {
        posicionZ = transform.position.z;

        if (cuerda != null)
        {
            escalaOriginal = cuerda.localScale;
        }

        if (puntoAnclajeSuperior == null)
        {
            puntoAnclajeSuperior = this.transform;
        }

        if (cuerda != null && puntoAnclajeSuperior != null && objetoCuelga != null)
        {
            Vector3 dirInicial = objetoCuelga.position - puntoAnclajeSuperior.position;
            distanciaInicial = dirInicial.magnitude;
        }
    }

    void Update()
    {
        // Movimiento de la grua
        float movimientoZ = speed * Time.deltaTime;
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.O) && pos.z + movimientoZ <= maxZ)
        {
            transform.Translate(Vector3.forward * movimientoZ, Space.World);
            posicionZ += movimientoZ;
        }
        else if (Input.GetKey(KeyCode.P) && pos.z - movimientoZ >= minZ)
        {
            transform.Translate(Vector3.back * movimientoZ, Space.World);
            posicionZ -= movimientoZ;
        }

        // subida y bajada
        if (pivoteGancho != null)
        {
            Vector3 posPivote = pivoteGancho.localPosition;
            float movimiento = velocidadGancho * Time.deltaTime;
            float nuevaY = posPivote.y;

            // Subir
            if (Input.GetKey(KeyCode.K))
            {
                nuevaY -= movimiento;
            }

            // Bajar
            if (Input.GetKey(KeyCode.L))
            {
                nuevaY += movimiento;
            }

            // Limitar
            nuevaY = Mathf.Clamp(nuevaY, -80f, 0f);
            pivoteGancho.localPosition = new Vector3(posPivote.x, nuevaY, posPivote.z);
        }

        // posiciones
        if (objetoCuelga != null && pivoteGancho != null)
        {
            objetoCuelga.position = pivoteGancho.position;
        }

        ActualizarCuerda();
    }

    private void ActualizarCuerda()
    {
        if (cuerda == null || objetoCuelga == null || puntoAnclajeSuperior == null) return;

        Vector3 direccion = objetoCuelga.position - puntoAnclajeSuperior.position;
        float distancia = direccion.magnitude;

        cuerda.position = puntoAnclajeSuperior.position + direccion * 0.5f;

        cuerda.rotation = Quaternion.LookRotation(direccion);
        cuerda.Rotate(90f, 0f, 0f);

        float factorEscala = (distanciaInicial > 0) ? (distancia / distanciaInicial) : 1f;

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