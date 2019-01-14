using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Inicializador;
    public GameObject I;
    public GameObject J;
    public GameObject L;
    public GameObject O;
    public GameObject S;
    public GameObject T;
    public GameObject Z;

    private GameObject piezaActual;
    private GameObject contenedor;

    private void Start()
    {
        contenedor = new GameObject("Contenedor");
    }

    void Update ()
    {
		if (!piezaActual)
        {
            IniciaPieza();
            EliminaLineas();
        }
	}

    private void EliminaLineas()
    {
        // por cada coordenada Y (float) guardamos cuantas partes (int) coinciden
        Dictionary<float, int> filas = new Dictionary<float, int>();

        List<float> filasABorrar = new List<float>();

        foreach (Transform t in contenedor.transform)
        {
            if (filas.ContainsKey(t.position.y))
            {
                filas[t.position.y]++;
            }
            else
            {
                filas.Add(t.position.y, 1);
            }
        }

        foreach (KeyValuePair<float, int> fila in filas)
        {
            if (fila.Value == 10)
            {
                filasABorrar.Add(fila.Key);
            }
        }

        if (filasABorrar.Count > 0)
        {
            foreach (Transform t in contenedor.transform)
            {
                int numFilasADesplazar = 0;

                if (filasABorrar.Contains(t.position.y))
                {
                    Destroy(t.gameObject);
                }
                else
                {
                    foreach (float fila in filasABorrar)
                    {
                        if (t.position.y > fila)
                        {
                            numFilasADesplazar++;
                        }
                    }

                    t.position = new Vector2(t.position.x, t.position.y - 1 * numFilasADesplazar);
                }
            }

            /*foreach (Transform t in contenedor.transform)
            {
                int numFilasADesplazar = 0;

                foreach(float fila in filasABorrar)
                {
                    if (t.position.y > fila)
                    {
                        numFilasADesplazar++;
                    }
                }

                t.position = new Vector2(t.position.x, t.position.y - 1 * numFilasADesplazar);
            }*/
        }
    }

    public void IniciaPieza()
    {
        int rand = Random.Range(0, 7);

        switch (rand)
        {
            case 0:
                piezaActual = Instantiate(I, Inicializador.transform);
                break;
            case 1:
                piezaActual = Instantiate(J, Inicializador.transform);
                break;
            case 2:
                piezaActual = Instantiate(L, Inicializador.transform);
                break;
            case 3:
                piezaActual = Instantiate(O, Inicializador.transform);
                break;
            case 4:
                piezaActual = Instantiate(S, Inicializador.transform);
                break;
            case 5:
                piezaActual = Instantiate(T, Inicializador.transform);
                break;
            case 6:
                piezaActual = Instantiate(Z, Inicializador.transform);
                break;
            default:
                break;
        }

        piezaActual.transform.parent = null;
        piezaActual.GetComponent<PieceController>().contenedor = contenedor;
    }
}
