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

    //lista per immagazzinare ogni punto del raggio laser
    List<Vector2> laserIndexes = new List<Vector2>();

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
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, laserRange);

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
        if (hit.collider.gameObject.tag == "Mirror")
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
        else
        {
            laserIndexes.Add(hit.point);
            UpdateLaser();
        }
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
