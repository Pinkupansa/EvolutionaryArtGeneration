
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ArtworkDNA 
{
    public List<PolygonalLine> lines{get; private set;}
    public float[,] intensityField{get; private set;}
    public ArtworkDNA(List<PolygonalLine> _lines, float[,] _intensityField)
    {
        lines = _lines;
        intensityField = _intensityField;
        //SmoothIntensityField(3);
    }
    public static ArtworkDNA Reproduce(ArtworkDNA dna1, ArtworkDNA dna2)
    {
        List<PolygonalLine> lines = new List<PolygonalLine>();
        lines.AddRange(dna1.lines.Where(x => UnityEngine.Random.Range(0f,1f) < 0.5f));
        lines.AddRange(dna2.lines.Where(x => UnityEngine.Random.Range(0f,1f) < 0.5f));
        float[,] intensityField = new float[100,100];
        for(int y = 0; y < 100; y++)
        {
            for(int x = 0; x < 100; x++)
            {
                intensityField[x, y] = (dna1.intensityField[x,y] + dna2.intensityField[x, y])/2f;
            }
        }
        ArtworkDNA child = new ArtworkDNA(lines, intensityField);
        child.Mutate();
        return child;
    }
    public void Mutate()
    {
        float mergingProbability = 0.13f;
        float movingProbability = 0.01f;
        float destroyProbability = 0.02f;
        float recolorProbability = 0.05f;
        List<PolygonalLine> newLines = new List<PolygonalLine>();
        foreach(PolygonalLine pline in lines)
        {
            float destroyToss = UnityEngine.Random.Range(0, 1f);
            if(destroyToss > destroyProbability)
            {
                PolygonalLine newPline = pline;
                float mergeToss = UnityEngine.Random.Range(0, 1f);
                if(mergeToss < mergingProbability)
                {
                    newPline = PolygonalLine.Merge(pline, lines[UnityEngine.Random.Range(0, lines.Count)]);
                }
                for(int i = 0; i < pline.points.Count; i++)
                {
                    float moveToss = UnityEngine.Random.Range(0f, 1f);
                    if(moveToss < movingProbability)
                    {
                        pline.points[i] += new Vector2(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));
                        pline.points[i] = new Vector2(Mathf.Clamp01(pline.points[i].x), Mathf.Clamp01(pline.points[i].y));
                    }
                }
                float recolorToss = UnityEngine.Random.Range(0f, 1f);
                if(recolorToss < recolorProbability)
                {
                    pline.color = new Color(pline.color.r + UnityEngine.Random.Range(-0.05f, -0.05f), pline.color.g + UnityEngine.Random.Range(-0.05f, -0.05f), pline.color.b + UnityEngine.Random.Range(-0.05f, -0.05f));
                }
                newLines.Add(newPline);
            }

        }
        lines = newLines;
        float fieldModificationProbability = 0.05f;
        for(int y = 0; y < 100; y++)
        {
            for(int x = 0; x < 100; x++)
            {
                float fieldModificationToss = UnityEngine.Random.Range(0f,1f);
                if(fieldModificationToss < fieldModificationProbability)
                {
                    intensityField[x, y] += UnityEngine.Random.Range(-0.5f,0.5f);
                }
                
            }
        }
        //SmoothIntensityField(3);
    }
    private void SmoothIntensityField(int iterations)
    {
        for(int i = 0; i < iterations; i++)
        {
            float[,] newIntensityField = (float[,])intensityField.Clone();
            for(int x = 1; x < 99; x++)
            {
                for(int y = 1; y < 99; y++)
                {
                    float sum = 0;
                    for(int dx = -1; dx <= 1; dx ++)
                    {
                        for(int dy = -1; dy <= 1; dy ++)
                        {
                            
                            sum += intensityField[x, y];
                            
                        }
                    }
                    newIntensityField[x, y] = sum/9f;
                }
            }
            intensityField = newIntensityField;
        }
        
    }
    public static ArtworkDNA Random()
    {
        List<PolygonalLine> brushStrokes = new List<PolygonalLine>();
        int numberOfLines = UnityEngine.Random.Range(2,10);
        for(int i = 0; i < numberOfLines; i++)
        {
            brushStrokes.Add(PolygonalLine.Random());
        }
        float[,] intensityField = new float[100,100];
        for(int y = 0; y < 100; y++)
        {
            for(int x = 0; x < 100; x++)
            {
                intensityField[x, y] = UnityEngine.Random.Range(1.5f,10f);
            }
        }
        ArtworkDNA dna = new ArtworkDNA(brushStrokes, intensityField);
       
        return dna ;
    }

}
