using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class EvolutionManager : MonoBehaviour
{
    [SerializeField] private int populationSize;
    [SerializeField] private Transform layout;
    [SerializeField] private GameObject artworkPresentationPrefab;
    [SerializeField] private bool beziersMode;
    private ArtworkPopulation currentPopulation;
    
    void Start()
    {
        currentPopulation = ArtworkPopulation.Random(populationSize);
        RenderPopulation();
    }
    
    private void RenderArtwork(ArtworkDNA dna, int index)
    {
        GameObject instance = Instantiate(artworkPresentationPrefab, layout);
        instance.GetComponentInChildren<Marker>().index = index;
        int w = 400;
        int h = 400;
        Texture2D tex = new Texture2D(w,h);
        for(int x = 0; x < w; x++)
        {
            for(int y = 0; y < h; y++)
            {
                tex.SetPixel(x, y, Color.white);
            }
        }
        foreach (PolygonalLine l in dna.lines)
        {
            
            RenderPolygonalLine(l, tex, dna.intensityField);
        }
        
        
        tex.Apply(true);
        instance.transform.GetChild(0).GetComponent<RawImage>().texture = tex;
        

    } 
    private void RenderPolygonalLine(PolygonalLine pline, Texture2D tex, float[,] intensityField)
    {
        if(beziersMode)
        {
            List<Vector2> beziersCurve = Beziers.BeziersCurve(pline.points.ToArray(), 0.005f);
            foreach(Vector2 v in beziersCurve)
            {
                float x = Mathf.Clamp01(v.x);
                float y = Mathf.Clamp01(v.y);
                Draw((int)(x * (tex.width-1)), (int)(y*(tex.height-1)), tex, 5/*(int)intensityField[(int)(x * 99), (int)(y * 99)]*/, pline.color);
            }
        }
        else
        {
            for(int i = 0; i < pline.points.Count - 1; i++)
            {
                RenderLine(pline.points[i], pline.points[i+1], pline.color, tex, intensityField);
            }
        }
        
    }
    private void RenderLine(Vector2 start, Vector2 end, Color color, Texture2D tex, float[,] intensityField)
    {
        if(!beziersMode)
        {
            for(float t = 0; t <= 1; t+=0.01f)
            {
                Vector2 point = Vector2.Lerp(start, end, t);
                Draw((int)(point.x * (tex.width-1)), (int)(point.y*(tex.height-1)), tex, 5/*(int)intensityField[(int)(x * 99), (int)(y * 99)]*/, color);
            }
        }
        
        
        

    }
    private void Draw(int x, int y, Texture2D texture, int intensity, Color color)
    {
        
        for(int dx = -intensity; dx < intensity; dx++)
        {
            for(int dy = -intensity; dy < intensity; dy++)
            {
                
                if(x + dx < texture.width && x + dx > 0 && y + dy < texture.height && y + dy > 0)
                {
                    
                    texture.SetPixel(x+dx, y+dy, color);
                }
            }
        }
    }
    public void NextGeneration()
    {
        float[] fitnessArray = new float[populationSize];
        foreach(Marker c in GameObject.FindObjectsOfType<Marker>())
        {
            fitnessArray[c.index] = c.mark;
        }
        currentPopulation = currentPopulation.GenerateNewPopulation(fitnessArray);
        while(layout.childCount > 0)
        {
            DestroyImmediate(layout.GetChild(0).gameObject);
        }
        RenderPopulation();
    }

    private void RenderPopulation()
    {
        for(int i = 0; i < currentPopulation.artworks.Length; i++)
        {
            RenderArtwork(currentPopulation.artworks[i], i);
        }
    }
    
}
