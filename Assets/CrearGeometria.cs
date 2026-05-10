using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CrearGeometria : MonoBehaviour
{
    public enum formas 
    {
        Triangulo,
        Tira,
        Plano,
        Cubo,
        Cilindro,
        Esfera
    };
    public formas forma;
    public int particiones = 20;

    //añadido
    public Vector3 tamanyo = Vector3.one;

    public float radio = 1;

    public float altura = 1;
    public float radio_base = 1;
    

    public List<Vector3> vertices;
    public List<Vector3> normals;
    public List<Vector2> textCoords;
    public List<int> triangles;
    
    // Función generica para crear la geometría, se invoca desde el CustomInspector
    public void crear() 
    {
        Debug.Log("Crear geometría: " + forma);
        switch (forma) {
            // Triangulo
            case formas.Triangulo: 
                crearTriangulo(); 
                break;

            case formas.Tira:
                crearTira();
                break;

            case formas.Plano:
                crearPlano();
                break;

            case formas.Esfera:
                crearEsfera();
                break;
            
            // ToDo: completar el resto de formas posibles
            case formas.Cubo:
                crearCubo();
                break;

            case formas.Cilindro:
                crearCilindro();
                break;
        }
    }

    void crearTriangulo() 
    {
        // Crear los vertices
        vertices = new List<Vector3>();
        vertices.Add(new Vector3(0, 0, 0));
        vertices.Add(new Vector3(0, 0, 1));
        vertices.Add(new Vector3(1, 0, 0));

        // Crear las normales de cada vertice
        normals = new List<Vector3>();
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));

        // Crear las coordenadas de textura de cada vertice
        textCoords = new List<Vector2>();
        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, 1));
        textCoords.Add(new Vector2(1, 0));

        // Crear los triángulos, a partir de los indices de los vertices en el vector de vertices
        // Cuidado! orientación mano izquierda, vertices en sentido horario
        triangles = new List<int>();
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    void crearCubo()
    {
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();
        
        //CARA FRONTAL

        // Vertice 0
        vertices.Add(new Vector3(0, 0, 0)); 
        normals.Add(new Vector3(0, 0, -1)); 
        textCoords.Add(new Vector2(1.0f/3.0f, 0.5f)); 

        // Vertice 1
        vertices.Add(new Vector3(0, tamanyo.y, 0));  
        normals.Add(new Vector3(0, 0, -1));
        textCoords.Add(new Vector2(1.0f/3.0f, 1)); 

        // Vertice 2
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, 0));   
        normals.Add(new Vector3(0, 0, -1)); 
        textCoords.Add(new Vector2(2.0f/3.0f, 1)); 

        // Vertice 3
        vertices.Add(new Vector3(tamanyo.x, 0, 0)); 
        normals.Add(new Vector3(0, 0, -1)); 
        textCoords.Add(new Vector2(2.0f/3.0f, 0.5f)); 

        //triangulo 1
        triangles.Add(0); 
        triangles.Add(1); 
        triangles.Add(3);

        //triangulo 2
        triangles.Add(1); 
        triangles.Add(2); 
        triangles.Add(3);

        //CARA DERECHA

        // Vertice 4
        vertices.Add(new Vector3(tamanyo.x, 0, 0)); 
        normals.Add(new Vector3(1, 0, 0));
        textCoords.Add(new Vector2(2.0f/3.0f, 0.5f));

        // Vertice 5
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, 0));
        normals.Add(new Vector3(1, 0, 0));
        textCoords.Add(new Vector2(2.0f/3.0f, 1));

        // Vertice 6
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(1, 0, 0));
        textCoords.Add(new Vector2(1, 1));

        // Vertice 7
        vertices.Add(new Vector3(tamanyo.x, 0, tamanyo.z));
        normals.Add(new Vector3(1, 0, 0));
        textCoords.Add(new Vector2(1, 0.5f));
        

        //triangulo 3
        triangles.Add(4); 
        triangles.Add(5); 
        triangles.Add(7);

        //triangulo 4
        triangles.Add(5); 
        triangles.Add(6); 
        triangles.Add(7);

        //CARA TRASERA

        // Vertice 8
        vertices.Add(new Vector3(tamanyo.x, 0, tamanyo.z));
        normals.Add(new Vector3(0, 0, 1));
        textCoords.Add(new Vector2(1.0f/3.0f, 0));

        // Vertice 9
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(0, 0, 1));
        textCoords.Add(new Vector2(1.0f/3.0f, 0.5f));

        // Vertice 10
        vertices.Add(new Vector3(0, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(0, 0, 1));
        textCoords.Add(new Vector2(2.0f/3.0f, 0.5f));

        // Vertice 11
        vertices.Add(new Vector3(0, 0, tamanyo.z));
        normals.Add(new Vector3(0, 0, 1));
        textCoords.Add(new Vector2(2.0f/3.0f, 0));

        //triangulo 5
        triangles.Add(8); 
        triangles.Add(9); 
        triangles.Add(11);

        //triangulo 6
        triangles.Add(9); 
        triangles.Add(10); 
        triangles.Add(11);

        //CARA IZQUIERDA    

        // Vertice 12
        vertices.Add(new Vector3(0, 0, tamanyo.z));
        normals.Add(new Vector3(-1, 0, 0));
        textCoords.Add(new Vector2(0, 0.5f));

        // Vertice 13
        vertices.Add(new Vector3(0, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(-1, 0, 0));
        textCoords.Add(new Vector2(0, 1));

        // Vertice 14
        vertices.Add(new Vector3(0, tamanyo.y, 0));
        normals.Add(new Vector3(-1, 0, 0));
        textCoords.Add(new Vector2(1.0f/3.0f, 1));

        // Vertice 15
        vertices.Add(new Vector3(0, 0, 0));
        normals.Add(new Vector3(-1, 0, 0));
        textCoords.Add(new Vector2(1.0f/3.0f, 0.5f));

        //triangulo 5
        triangles.Add(12); 
        triangles.Add(13); 
        triangles.Add(15);

        //triangulo 6
        triangles.Add(13); 
        triangles.Add(14); 
        triangles.Add(15);

        //CARA ABAJO

        // Vertice 16
        vertices.Add(new Vector3(tamanyo.x, 0, 0));
        normals.Add(new Vector3(0, -1, 0));
        textCoords.Add(new Vector2(1, 0));

        // Vertice 17
        vertices.Add(new Vector3(tamanyo.x, 0, tamanyo.z));
        normals.Add(new Vector3(0, -1, 0));
        textCoords.Add(new Vector2(2.0f/3.0f, 0));

        // Vertice 18
        vertices.Add(new Vector3(0, 0, tamanyo.z));
        normals.Add(new Vector3(0, -1, 0));
        textCoords.Add(new Vector2(2.0f/3.0f, 0.5f));

        // Vertice 19
        vertices.Add(new Vector3(0, 0, 0));
        normals.Add(new Vector3(0, -1, 0));
        textCoords.Add(new Vector2(1, 0.5f));

        //triangulo 5
        triangles.Add(16); 
        triangles.Add(17); 
        triangles.Add(19);

        //triangulo 6
        triangles.Add(17); 
        triangles.Add(18); 
        triangles.Add(19);

        //CARA ARRIBA

        // Vertice 20
        vertices.Add(new Vector3(0, tamanyo.y, 0));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(1.0f/3.0f, 0));

        // Vertice 21
        vertices.Add(new Vector3(0, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(0, 0));

        // Vertice 22
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(0, 0.5f));

        // Vertice 23
        vertices.Add(new Vector3(tamanyo.x, tamanyo.y, 0));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(1.0f/3.0f, 0.5f));

        //triangulo 5
        triangles.Add(20); 
        triangles.Add(21); 
        triangles.Add(23);

        //triangulo 6
        triangles.Add(21); 
        triangles.Add(22); 
        triangles.Add(23);

        crearMesh();
    }

    void crearCilindro()
    {
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        int tamTira = particiones + 1;
        float tamParticion = 1.0f/particiones;
        int currIndex;
        

        //CARA ABAJO

        // Vertice 0
        vertices.Add(new Vector3(-tamanyo.x, 0, -tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(0.5f, 0.5f));

        // Vertice 1
        vertices.Add(new Vector3(tamanyo.x, 0, -tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(0.5f, 1));

        // Vertice 2
        vertices.Add(new Vector3(tamanyo.x, 0, tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(1,1));

        // Vertice 3
        vertices.Add(new Vector3(-tamanyo.x, 0, tamanyo.z));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(1, 0.5f));

        //triangulo 1
        triangles.Add(2); 
        triangles.Add(1); 
        triangles.Add(3);

        //triangulo 2
        triangles.Add(3); 
        triangles.Add(1); 
        triangles.Add(0);
        

        //vertice 0
        vertices.Add(new Vector3(0, tamanyo.y, 0));
        normals.Add(new Vector3(0, 1, 0));
        textCoords.Add(new Vector2(0.25f, 0.75f));

        for(int i=4; i<=particiones+4; i++)
        {
            //cálculo de "circunferencia del circulo"
            float angulo = i * (2 * Mathf.PI / particiones);
            float x = Mathf.Cos(angulo)*radio;
            float z = Mathf.Sin(angulo)*radio;
            vertices.Add(new Vector3(x, tamanyo.y, z)); //centro del circulo
            normals.Add(new Vector3(0, 1, 0)); 
            textCoords.Add(new Vector2(Mathf.Cos(angulo)*0.25f + 0.25f, Mathf.Sin(angulo)*0.25f + 0.75f));

            if(i>4)
            {
                triangles.Add(4);
                triangles.Add(i+1);
                triangles.Add(i-1+1);
            }
        } 

        

        

        for (int i=0; i<=particiones; i++) 
        {
            float X = tamParticion * i;

            float angulo = i * (2 * Mathf.PI / particiones);
            float x = Mathf.Cos(angulo)*radio;
            float z = Mathf.Sin(angulo)*radio;
            float x_base = Mathf.Cos(angulo)*radio_base;
            float z_base = Mathf.Sin(angulo)*radio_base;
            

            //Hay que cambiar lasss normales entre -1/1
            // - Vertice inferior
            vertices.Add(new Vector3(x_base,0,z_base));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,0));

            // - Vertice superior
            vertices.Add(new Vector3(x, tamanyo.y, z));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,0.5f));
            
            currIndex = vertices.Count-1;

             

            if(i>0)
            {
                // - Triangulo 1
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-1);     //2
            triangles.Add(currIndex-3);     //0

            // - Triangulo 2
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-3);     //0
            triangles.Add(currIndex-2);
            }
        }

        
        
        

        crearMesh();
    }

    void crearTira() 
    {
        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        // Inicializar el contador de vertices
        int tamTira = particiones + 1;
        float tamParticion = 1.0f/particiones;

        /*
        Añadir los dos primeros vertices de la tira, después
        por cada iteración del bucle se añaden 2 vertices y
        se crean dos triangulos (T1 y T2) con los dos vertices 
        anteriores.

        1 - - 3 - - 5 - - 
        |T2 / |   / |
        |  /  |  /  |
        | / T1| /   |
        0 - - 2 - - 4 - - 

        */

        // - Vertice inferior
        vertices.Add(new Vector3(0,0,0));
        normals.Add(new Vector3(0,1,0));
        textCoords.Add(new Vector2(0,0));
        
        // - Vertice superior
        vertices.Add(new Vector3(0,0,tamParticion));
        normals.Add(new Vector3(0,1,0));
        textCoords.Add(new Vector2(0,1));

        for (int i=1; i<=particiones; i++) 
        {
            float X = tamParticion * i;
            
            // - Vertice inferior
            vertices.Add(new Vector3(X,0,0));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,0));

            // - Vertice superior
            vertices.Add(new Vector3(X,0,tamParticion));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,1));
            
            int currIndex = vertices.Count-1;

            // - Triangulo 1
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-1);     //2
            triangles.Add(currIndex-3);     //0

            // - Triangulo 2
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-3);     //0
            triangles.Add(currIndex-2);     //1
        }

        // Crear la mesh con los vectores calculados
        crearMesh();
    }

    void crearPlano() 
    {
        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        // Inicializar el contador de vertices
        int currIndex = 0;
        int tamTira = particiones + 1;

        // Calcular el tamaño de cada celda, 
        // al ser un cuadrado es igual en X y en Z
        float tamParticion = 1.0f/particiones;

        for (int i=0; i<=particiones; i++) {
            
            float Z = tamParticion * i;             // Calculo de la coordenada Z (filas)

            for (int j=0; j<=particiones; j++) {
                
                float X = tamParticion * j;          // Calculo de la coordenada X (columna)
                
                vertices.Add(new Vector3(X,0,Z));
                normals.Add(new Vector3(0,1,0));     // En el plano XZ el vector normal es el vector Y
                textCoords.Add(new Vector2(X,Z));

                if ((i>0) && (j>0)) {

                    // A partir de la primera fila y la primera columna, 
                    // cada vertice da lugar a 2 nuevos triángulos (sentido horario)
                    /*
                            currIndex - 1  -->              O --- X   <-- currIndex
                                                            | 1 / |
                                                            |  /  |
                                                            | / 2 |
                            currIndex - tamTira - 1  -->    O --- O   <-- currIndex - tamTira
                    
                    */

                    // Triángulo 1
                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira - 1);
                    triangles.Add(currIndex - 1);
                    
                    // Triángulo 2
                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira);
                    triangles.Add(currIndex - tamTira - 1);
                }
                
                // Incrementar el contador de vertices
                currIndex ++;
            }
        }

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    void crearEsfera() 
    {

        // Ecuación de la esfera por gajos
        // [U,V] en el rango [0,1]
        // x = radio * cos(2*PI*U) * sin(PI*V)
        // y = radio * cos(PI*V)
        // z = radio * sin(2*PI*U) * sin(PI*V)

        float radio = 1;

        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        float paso = 1.0f / particiones;

        int currIndex = 0;
        int tamTira = particiones + 1;

        for (int i=0; i<=particiones; i++) {
            float u = paso * i;
            for (int j=0; j<=particiones; j++) {
                float v = paso * j;

                Vector3 aux = new Vector3(
                    Mathf.Cos(2*Mathf.PI*u) * Mathf.Sin(Mathf.PI * v),
                    Mathf.Cos(Mathf.PI*v),
                    Mathf.Sin(2*Mathf.PI*u) * Mathf.Sin(Mathf.PI * v)
                );

                vertices.Add(radio * aux);
                normals.Add(aux.normalized);    // Para una esfera centrada en 0, la normal coincide con las coordenadas del vertice
                textCoords.Add(new Vector2(u,1.0f-v));

                if (i>0 && j>0) {
                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira);
                    triangles.Add(currIndex - tamTira - 1);

                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira - 1);
                    triangles.Add(currIndex - 1);
                }

                // Incrementar el contador de vertices
                currIndex ++;
            }
        }

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    // Función para crear la mesh a partir de los vectores calculados en las funciones anteriores
    public void crearMesh() {

        // Crear la mesh con toda la información
        Mesh m = new Mesh();
        m.vertices = vertices.ToArray();
        m.normals = normals.ToArray();
        m.uv = textCoords.ToArray();
        m.triangles = triangles.ToArray();

        // Llamada obligatoria para recalcular la información 
        // de la malla a partir de los vectores asignados
        m.RecalculateBounds();  
        m.RecalculateTangents();

        // Asignar la malla al componente MeshFilter del Gameobject
        GetComponent<MeshFilter>().sharedMesh = m;
    }
}
