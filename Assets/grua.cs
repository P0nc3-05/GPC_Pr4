using UnityEngine;

public class grua : MonoBehaviour
{
    [Header("Movimiento en Z (Grúa)")]
    public float speed = 5f;
    public float minZ = 0.75f;
    public float maxZ = 7.25f;
    private float posicionZ = 0f;

    [Header("Movimiento en Y (Gancho)")]
    public float velocidadGancho = 2f;
    public float alturaMinima = -1f;
    public float alturaMaxima = 0f;

    [Header("Referencias Visuales")]
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
        // --- 1. MOVIMIENTO HORIZONTAL (Eje Z) ---
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

        // --- 2. CONTROL DE RECOGIDA/DESCENSO (Manual) ---
        if (pivoteGancho != null)
        {
            Vector3 posPivote = pivoteGancho.localPosition;
            float movimiento = velocidadGancho * Time.deltaTime;
            float nuevaY = posPivote.y;

            // Recoger/Subir si se presiona K (resta Y)
            if (Input.GetKey(KeyCode.K))
            {
                nuevaY -= movimiento;
            }

            // Bajar si se presiona L (suma Y)
            if (Input.GetKey(KeyCode.L))
            {
                nuevaY += movimiento;
            }

            // Limitar la altura dentro del rango
            nuevaY = Mathf.Clamp(nuevaY, alturaMinima, alturaMaxima);
            pivoteGancho.localPosition = new Vector3(posPivote.x, nuevaY, posPivote.z);
        }

        // --- 3. ACTUALIZACIÓN DE LA CUERDA Y EL GANCHO ---
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