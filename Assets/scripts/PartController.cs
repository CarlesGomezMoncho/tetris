using UnityEngine;

public enum Direccion { Arriba, Abajo, Derecha, Izquierda, Ninguna };

public class PartController : MonoBehaviour {

    private int layerMask;

    private void Start()
    {
        //todas las capas excepto la capa Pieza
        layerMask = ~LayerMask.GetMask("Pieza");
    }

    public bool PuedeCaer()
    {
        bool puedeCaer = true;

        if (Physics2D.Raycast(transform.position, Vector2.down, 1, layerMask))
        {
            //si choca con algo que este a 1 de distancia y que no esté en la capa Pieza
            puedeCaer = false;
        }

        return puedeCaer;
    }

    public bool PuedeMoverse(Direccion direccion)
    {
        RaycastHit2D hit;
        bool puedeMoverse = true;

        if (direccion == Direccion.Derecha)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, 1, layerMask);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, 1, layerMask);
        }

        if (hit)
        {
            puedeMoverse = false;
        }

        return puedeMoverse;
    }

    public bool Colisiona()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 0, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
