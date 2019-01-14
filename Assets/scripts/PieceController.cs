using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour {

    public float intervaloCaida = 1f;
    public GameObject contenedor;

    private bool cae = false;
    private bool stop = false;

    private bool acelera = false;

	void Start ()
    {
        StartCoroutine(GravedadTetris());	
	}
	
	void Update ()
    {
        if (!stop)
        {

            Direccion direccion = Direccion.Ninguna;
            float rotacion = 0;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rotacion = 90;

                transform.Rotate(Vector3.forward, rotacion);

                foreach(PartController part in GetComponentsInChildren<PartController>())
                {
                    if (part.Colisiona())
                    {
                        transform.Rotate(Vector3.forward, -rotacion);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direccion = Direccion.Izquierda;

                foreach(PartController parte in GetComponentsInChildren<PartController>())
                {
                    if(!parte.PuedeMoverse(direccion))
                    {
                        direccion = Direccion.Ninguna;
                        break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direccion = Direccion.Derecha;

                foreach (PartController parte in GetComponentsInChildren<PartController>())
                {
                    if (!parte.PuedeMoverse(direccion))
                    {
                        direccion = Direccion.Ninguna;
                        break;
                    }
                }
            }

            if (direccion != Direccion.Ninguna)
            {
                float posicion = transform.position.x;

                if (direccion == Direccion.Derecha)
                {
                    posicion = posicion + 1;
                }
                else if (direccion == Direccion.Izquierda)
                {
                    posicion = posicion - 1;
                }

                transform.position = new Vector2(posicion, transform.position.y);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                acelera = true;
                StartCoroutine(AceleraCaida());
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                acelera = false;
            }

            if (cae)
            {
                bool puedeCaer = true;

                foreach (PartController parte in GetComponentsInChildren<PartController>())
                {
                    //¿puede caer la parte?
                    if (!parte.PuedeCaer())
                    {
                        puedeCaer = false;
                        //si no puede caer salimos del bucle
                        break;
                    }

                }

                //si puede caer
                if (puedeCaer)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                }
                else
                {
                    stop = true;
                }

                //movemos la pieza hacia abajo
                cae = false;
            }
        }else
        {
            foreach(PartController parte in GetComponentsInChildren<PartController>())
            {
                parte.gameObject.layer = 0;
                parte.gameObject.transform.parent = contenedor.transform;
            }

            Destroy(gameObject);
        }
	}

    private IEnumerator GravedadTetris()
    {
        while(!stop)
        {
            yield return new WaitForSeconds(intervaloCaida);
            cae = true;
        }
    }

    private IEnumerator AceleraCaida()
    {
        while(acelera)
        {
            yield return new WaitForSeconds(0.1f);
            cae = true;
        }
    }
}
