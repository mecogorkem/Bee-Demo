using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    private List<Vector2> points;
    private List<Vector2> mobPoints;
    private List<Vector2> flowerPoints;
    private List<Vector2> beehivePoints;
    [SerializeField] private float gizmosRadius;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private float maxAngle = 30;
    [SerializeField] private float edgeGap =0.5f;
    [SerializeField] private int mobCount = 5;
    [SerializeField] private int flowerCount = 5;
    [SerializeField] private int beeCount = 8;
    [SerializeField] private int safeZone = 20;
    [SerializeField] private List<GameObject> mobs;
    [SerializeField] private Transform mobTransform;
    [SerializeField] private GameObject beehive;
    [SerializeField] private List<GameObject> flowers;
    [SerializeField] private Transform finalMobs;
    private List<Vector3> positions = new List<Vector3>
    {
        new Vector3(2.50f,2.713f,208.6f),
        new Vector3(1.486f,2.713f,208.165f),
        new Vector3(-0.11f,2.713f,208.28f),
        new Vector3(2.836f,2.713f,209.41f),
    };
    private float _radyan;
    private float maxX;
    private List<MobType> finalMobType;

    public void Initialize(LevelData levelData)
    {
        maxAngle = maxAngle >= 90 ? 89 : maxAngle;
        _radyan = (maxAngle / 180) * Mathf.PI;
        maxX = (boxSize.x / 2f) - edgeGap;
        mobCount = levelData.mobCount;
        flowerCount = levelData.flowerCount;
        beeCount = levelData.beeCount;
        finalMobType = levelData.endMob;
        points = CreatePath();
        mobPoints = FindMobPoint(points);
        DropMob(mobPoints);
        beehivePoints = FindBeehivePoints(points);
        DropBeehive(beehivePoints);
        flowerPoints = FindFlowerPoints(points, beehivePoints);
        DropFlower(flowerPoints);
        DropFinalMob();
    }

    private void DropFinalMob()
    {
        for (int i = 0; i < finalMobType.Count; i++)
        {
            var obj = Instantiate(mobs[(int)finalMobType[i]], finalMobs);
            obj.transform.position = positions[i];
            obj.GetComponent<Mob>().Initialize();
        }
    }
    

    private void DropFlower(List<Vector2> vector2s)
    {
        int i = 0;
        foreach (var point in vector2s)
        {
            var obj = Instantiate(flowers[i%4],mobTransform);
            obj.transform.position = new Vector3(point.x, 0.55f, point.y);
            obj.GetComponent<Flower>().Initialize(UnityEngine.Random.Range(15, 25));
            i++;
        }
        
    }

    private void DropBeehive(List<Vector2> vector2s)
    {
        foreach (var point in vector2s)
        {
            var obj = Instantiate(beehive,mobTransform);
            obj.transform.position = new Vector3(point.x, 0.55f, point.y); 
            obj.GetComponent<BeeHive>().Initialize(1);
        }    
    }

    private void DropMob(List<Vector2> vector2s)
    {
        int i = 0;
        foreach (var point in vector2s)
        {
            var obj = Instantiate(mobs[i%3],mobTransform);
            obj.transform.position = new Vector3(point.x, 0, point.y); 
            obj.GetComponent<Mob>().Initialize();
            i++;
        }
    }
    
    


    private void OnDrawGizmos()
    {
        if (points == null)
        {
            return;
        }
            
        foreach (var point in points)
        {
            Gizmos.DrawSphere(new Vector3(point.x,0,point.y),gizmosRadius);
        }

        Gizmos.DrawWireCube(new Vector3(0,boxSize.y/2f,boxSize.z/2f),boxSize);
        Gizmos.color = Color.red;

        if (mobPoints == null)
        {
            return;
        }
        foreach (var point in mobPoints)
        {
            Gizmos.DrawSphere(new Vector3(point.x,0,point.y),gizmosRadius);
        }
    }

    private List<Vector2> CreatePath()
    {
        
        var list = new List<Vector2>();
        list.Add(Vector2.zero);
        int index = 0;
        float maxX = (boxSize.x / 2f) - edgeGap;
        while (list[index].y<boxSize.z)
        {
            var newRadyan = UnityEngine.Random.Range(-_radyan, _radyan);
            var xoffset = Mathf.Sin(newRadyan) * gizmosRadius*2;
            if (Mathf.Abs(xoffset+list[index].x)>maxX)
            {
                continue;
            }

            list.Add(new Vector3(list[index].x+xoffset,list[index].y+Mathf.Cos(newRadyan)));
            index++;
            
        }
        return list; 

        

    }

    private List<Vector2> FindMobPoint(List<Vector2> vectors)
    {
        int zoneGap = (int)Mathf.Floor((vectors.Count - safeZone)/ mobCount);
        int starter = safeZone;
        int maxLoopCount = 30;
        int loopCount = 0;
        var list = new List<Vector2>();
        while (starter+zoneGap < vectors.Count)
        {
            var canDrawSphere = true;
            var miny = vectors[starter].y;
            var maxy = vectors[starter + zoneGap].y;
            var y = UnityEngine.Random.Range(miny, maxy);
            var x = UnityEngine.Random.Range(-maxX, maxX);
            var point = new Vector2(x, y);
            loopCount++;
            if (loopCount>maxLoopCount)
            {
                break;
            }
            for (int i = starter; i < starter+zoneGap; i++)
            {
              
                if (point != vectors[i] && Vector3.Distance(point, vectors[i]) < gizmosRadius*3)
                {
                    canDrawSphere = false;
                    break;
                }
            }

            if (!canDrawSphere)
            {
                continue;
            }
            list.Add(point);
            loopCount = 0;
            starter += zoneGap;

        }

        return list;
    }

    private List<Vector2> FindBeehivePoints(List<Vector2> vectors)
    {
        int zoneGap = (int)Mathf.Floor((vectors.Count - safeZone)/ beeCount);
        int starter = safeZone;
        var list = new List<Vector2>();
        while (starter+zoneGap < vectors.Count)
        {
            var pointIndex = UnityEngine.Random.Range(starter, starter + zoneGap);
            list.Add(points[pointIndex]);
            starter += zoneGap;
        }

        return list;
    }
    
    private List<Vector2> FindFlowerPoints(List<Vector2> vectors,List<Vector2> beehiveVector)
    {
        int zoneGap = (int)Mathf.Floor((vectors.Count - safeZone)/ flowerCount);
        int starter = safeZone;
        var list = new List<Vector2>();
        int maxLoopCount = 30;
        int loopCount = 0;
        while (starter+zoneGap < vectors.Count)
        {
            var canDrawSphere = true;
            var pointIndex = UnityEngine.Random.Range(starter, starter + zoneGap);
            var point = points[pointIndex];
            loopCount++;
            if (loopCount>maxLoopCount)
            {
                break;
            }
            foreach (var vector in beehiveVector)
            {
              
                if (Vector3.Distance(point, vector) < gizmosRadius*3)
                {
                    canDrawSphere = false;
                    break;
                }
            }
            if (!canDrawSphere)
            {
                continue;
            }

            loopCount = 0;
            list.Add(points[pointIndex]);
            starter += zoneGap;
        }

        return list;
    }
}
