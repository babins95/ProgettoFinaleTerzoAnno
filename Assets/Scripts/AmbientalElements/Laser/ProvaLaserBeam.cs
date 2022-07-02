using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaLaserBeam : MonoBehaviour
{
    //Vector2 position;
    public float laserRange = 10;
    private Vector2 direction;
    LineRenderer laserRenderer;
    //se il raggio deve collidere con qualcosa basta aggiungerlo ai layer
    public int layer;

    //lista per immagazzinare ogni punto del raggio laser
    List<Vector3> laserIndexes = new List<Vector3>();
    //dizionario per eventuali differenti materiali per avere differenti rifrazioni
    Dictionary<string, float> refractMaterials = new Dictionary<string, float>()
    {
        //più grande il float più grande l'angolo di rifrazione
        {"Air", 1f },
        {"Glass", 1.5f },
        {"Crystal", 2.1f }
    };

    //public ProvaLaserBeam(Vector2 pos, Vector2 dir, Material material, float range)
    //{
    //    laserRenderer = new LineRenderer();
    //    laserObject = new GameObject();
    //    position = pos;
    //    direction = dir;
    //    laserRange = range;

    //    //quando crei un raggio ci aggiungi il LineRenderer
    //    laserRenderer = laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;

    //    //dimensione del raggio, materiale e colore
    //    laserRenderer.startWidth = 0.1f;
    //    laserRenderer.endWidth = 0.1f;
    //    laserRenderer.material = material;
    //    laserRenderer.startColor = Color.red;
    //    laserRenderer.endColor = Color.red;
    //    laserRenderer.sortingOrder = 2;

    //    CastRay(pos, dir, laserRenderer);
    //}
    
    private void Start()
    {
        laserRenderer = GetComponent<LineRenderer>();
        direction = GetComponentInParent<Transform>().right;
        layer = LayerMask.GetMask("Mirror", "Player", "BlockLaser", "Refract");
    }
    private void Update()
    {
        CastRay(transform.position, direction, laserRenderer);
    }
    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser)
    {
        laserIndexes.Add(pos);

        //effettivo laser gestito da raycast
        Ray ray = new Ray(pos, dir);
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, laserRange, layer);

        if (hit.collider != null)
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndexes.Add(ray.GetPoint(laserRange));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit2D hit, Vector2 direction, LineRenderer laser)
    {
        //se colpisce uno specchio il raggio viene deviato
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Mirror"))
        {
            Vector2 pos = hit.point;
            Vector2 dir = Vector2.Reflect(direction, hit.normal);
            //questa newPos la calcolo per evitare casi nei quali il nuovo raggio parte da
            //dentro lo specchio, risultando quindi in una collisione con se stesso e un loop infinito
            Vector2 newPos = new Vector2(pos.x + (0.01f * dir.x), pos.y + (0.01f * dir.y));
            //la deviazione è di fatto la creazione di un nuovo laser
            CastRay(newPos, dir, laser);
            //la funzione è ricorsiva, se serve mettere un limite basta aggiungere una variabile counter
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Refract"))
        {
            //prendo il punto d'impatto
            Vector3 pos = hit.point;
            laserIndexes.Add(pos);

            Vector3 newPos1 = new Vector3(pos.x + (0.01f * direction.x), pos.y + (0.01f * direction.y));
            float n1 = refractMaterials["Air"];
            //questo funziona solo se il nome del gameObject è lo stesso interno al dictionary
            float n2 = refractMaterials[hit.collider.name];

            Vector3 norm = hit.normal;
            Vector3 incidentDir = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incidentDir);
            //da togliere se si mette la seconda rifrazione
            CastRay(newPos1, refractedVector, laser);
        }
        else
        {
            laserIndexes.Add(hit.point);
            UpdateLaser();
        }
    }

    //questo serve a calcolare la prima rifrazione
    Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        incident.Normalize();
        //bro questa è fisica oltre il mio livello, non ho idea dei calcoli, la legge di Snell non l'ho mai studiata
        Vector3 refractedVector = (n1 / n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident))
            - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1 / n2 * n1 / n2),
            Vector3.Cross(norm, incident)))).normalized;

        return refractedVector;
    }

    //updatelaser serve a far andare "in avanti" il laser
    void UpdateLaser()
    {
        laserRenderer.positionCount = laserIndexes.Count;
        for(int i = 0; i < laserIndexes.Count; i++)
        {
            if (laserRenderer.GetPosition(i) != laserIndexes[i])
            {
                laserRenderer.SetPosition(i, laserIndexes[i]);
            }
        }
        laserIndexes = new List<Vector3>();
    }
}
