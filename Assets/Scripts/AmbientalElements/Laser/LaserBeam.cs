using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam 
{
    Vector2 position;
    Vector2 direction;
    float laserRange;

    public GameObject laserObject;
    LineRenderer laserRenderer;
    //se il raggio deve collidere con qualcosa basta aggiungerlo ai layer
    int layer = LayerMask.GetMask("Mirror", "Player", "BlockLaser", "Refract");

    //lista per immagazzinare ogni punto del raggio laser
    List<Vector2> laserIndexes = new List<Vector2>();
    //dizionario per eventuali differenti materiali per avere differenti rifrazioni
    Dictionary<string, float> refractMaterials = new Dictionary<string, float>()
    {
        {"Air", 1f },
        {"Glass", 1.5f }
    };

    public LaserBeam(Vector2 pos, Vector2 dir, Material material, float range)
    {
        laserRenderer = new LineRenderer();
        laserObject = new GameObject();
        position = pos;
        direction = dir;
        laserRange = range;

        //quando crei un raggio ci aggiungi il LineRenderer
        laserRenderer = laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;

        //dimensione del raggio, materiale e colore
        laserRenderer.startWidth = 0.1f;
        laserRenderer.endWidth = 0.1f;
        laserRenderer.material = material;
        laserRenderer.startColor = Color.red;
        laserRenderer.endColor = Color.red;

        CastRay(pos, dir, laserRenderer);
    }

    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser)
    {
        laserIndexes.Add(pos);

        //effettivo laser gestito da raycast
        Ray ray = new Ray(pos, dir);
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, laserRange, layer);

        if(hit.collider != null)
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
        else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Refract"))
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

            //una volta presa la rifrazione d'ingresso vado a calcolare quella d'uscita
            Ray ray1 = new Ray(newPos1, refractedVector);
            Vector3 newRayPos = ray1.GetPoint(1.5f);

            Ray ray2 = new Ray(newRayPos, -refractedVector);
            RaycastHit2D hit2 = Physics2D.Raycast(newRayPos, -refractedVector, 1.6f);

            if(hit2)
            {
                laserIndexes.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point, refractedVector2, laser);
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
        int count = 0;
        laserRenderer.positionCount = laserIndexes.Count;

        foreach(Vector2 index in laserIndexes)
        {
            laserRenderer.SetPosition(count, index);
            count++;
        }
    }
}
