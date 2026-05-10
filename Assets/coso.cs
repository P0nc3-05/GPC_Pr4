using UnityEngine;

public class coso : MonoBehaviour
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

    // El punto que está pegado a la grúa y se mueve instantáneamente
    public Transform objetivoReferencia; 

    private Vector3 escalaOriginal;
    private float distanciaInicial;

    [Range(0.1f, 10f)]
    public float suavizado = 2.0f; // Menos valor = más retraso (más inercia)

    void Update()
    {
        // Vector3.Lerp calcula la posición intermedia entre el gancho y la grúa.
        // Al ejecutarse cada frame, el gancho nunca llega instantáneamente,
        // creando ese efecto de "atraso" de unos segundos.
        transform.position = Vector3.Lerp(
            transform.position, 
            objetivoReferencia.position, 
            suavizado * Time.deltaTime
        );
    }

    private void ActualizarCuerda()
    {
        bool todoOk = (cuerda != null && objeto != null && BaseGrua != null);

        if (todoOk)
        {
            // La dirección se calcula hacia el gancho que va retrasado.
            // Esto hace que la cuerda se incline "atrás" cuando la grúa corre.
            Vector3 direccion = objeto.position - BaseGrua.position;
            float distancia = direccion.magnitude;

            // Posicionamos la cuerda justo en el medio del trayecto
            cuerda.position = BaseGrua.position + direccion * 0.5f;

            // Rotamos para que mire siempre al gancho (línea recta)
            if (direccion != Vector3.zero) 
            {
                cuerda.rotation = Quaternion.LookRotation(direccion);
                cuerda.Rotate(90f, 0f, 0f);
            }

            // Ajustamos la escala según la distancia real en este frame
            float factorEscala = (distanciaInicial > 0) ? (distancia / distanciaInicial) : 1f;
            cuerda.localScale = new Vector3(escalaOriginal.x, escalaOriginal.y * factorEscala, escalaOriginal.z);
        }
    }
}
