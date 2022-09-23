using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    //Vector2 position;
    public float laserRange = 10;
    private Vector2 direction;
    LineRenderer laserRenderer;
    //se il raggio deve collidere con qualcosa basta aggiungerlo ai layer
    private int layer;

    //lista per immagazzinare ogni punto del raggio laser
    List<Vector3> laserIndexes = new List<Vector3>();

    private void Start()
    {
        laserRenderer = GetComponent<LineRenderer>();
        direction = GetComponentInParent<Transform>().right;
        layer = LayerMask.GetMask("Mirror", "Player", "BlockLaser");
    }
    private void Update()
    {
        CastRay(transform.position, direction, laserRenderer);
        if(GetComponentInParent<ShootLaser>().laserOn == false)
        {
            Destroy(this.gameObject);
        }
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
        else
        {
            laserIndexes.Add(hit.point);
            UpdateLaser();
        }
    }
    //updatelaser serve a far andare "in avanti" il laser
    void UpdateLaser()
    {
        laserRenderer.positionCount = laserIndexes.Count;
        for (int i = 0; i < laserIndexes.Count; i++)
        {
            if (laserRenderer.GetPosition(i) != laserIndexes[i])
            {
                laserRenderer.SetPosition(i, laserIndexes[i]);
            }
        }
        laserIndexes = new List<Vector3>();
    }
    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[laserRenderer.positionCount];
        laserRenderer.GetPositions(positions);
        return positions;
    }
    public float GetWidth()
    {
        return laserRenderer.startWidth;
    }
}
