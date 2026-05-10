using UnityEngine;

public class coso : MonoBehaviour
{

    public Material materialCuerda;
    public Transform cuerda;
    public Transform objeto;
    public Transform pivoteGrua;
    public Transform BaseGrua;

    // El punto que está pegado a la grúa y se mueve instantáneamente
    public Transform objetivoReferencia; 

    private Vector3 escalaOriginal;
    private float distanciaInicial;

    public float suavizado = 10f; // Menos valor = más retraso (más inercia)

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
        

        
            // La dirección se calcula hacia el gancho que va retrasado.
            // Esto hace que la cuerda se incline "atrás" cuando la grúa corre.
            Vector3 direccion = objeto.position - BaseGrua.position;
            float distancia = direccion.magnitude;

            // Posicionamos la cuerda justo en el medio del trayecto
            cuerda.position = BaseGrua.position + direccion * 0.5f;

            // Rotamos para que mire siempre al gancho (línea recta)
                cuerda.rotation = Quaternion.LookRotation(direccion);
                cuerda.Rotate(90f, 0f, 0f);

            // C) ESCALA: Estirar la cuerda
            float factorEscala = distancia / distanciaInicial;
            cuerda.localScale = new Vector3(
                escalaOriginal.x, 
                escalaOriginal.y * factorEscala, 
                escalaOriginal.z
            );
        
    }
}
