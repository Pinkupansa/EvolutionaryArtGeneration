using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtworkPopulation
{
    
    
    public ArtworkDNA[] artworks {get; private set;}
    public ArtworkPopulation(ArtworkDNA[] _artworks)
    {
        artworks = _artworks;
    }
    public ArtworkPopulation GenerateNewPopulation(float[] fitnessArray)
    {
        ArtworkDNA[] newPop = new ArtworkDNA[artworks.Length];
        float[] pickingProbabilities = GenerateProbabilities(fitnessArray);
        for(int i = 0; i < artworks.Length; i++)
        {
            int parent1 = RandomIndex(pickingProbabilities);
            int parent2 = RandomIndex(pickingProbabilities);
            //Debug.Log(parent1 + " " + parent2);
            newPop[i] = ArtworkDNA.Reproduce(artworks[parent1], artworks[parent2]);
            newPop[i].Mutate();
        }
        return new ArtworkPopulation(newPop);
    }
    private int RandomIndex(float[] probabilities)
    {
        int index = UnityEngine.Random.Range(0, probabilities.Length);
        float toss = UnityEngine.Random.Range(0f, 1f);
        if(toss > probabilities[index])
        {
            
            return RandomIndex(probabilities);
        }
        return index;
    }
    private float[] GenerateProbabilities(float[] fitnessValues)
    {
        float sum = 0;
        
        for(int i = 0; i < fitnessValues.Length; i++)
        {
            sum += fitnessValues[i];
           
        }
        float[] probs = new float[fitnessValues.Length];
        
        for(int i = 0; i < fitnessValues.Length; i++)
        {
            probs[i] = fitnessValues[i]/sum;
            
        }
        return probs;
    }

    public static ArtworkPopulation Random(int size)
    {
        
        ArtworkDNA[] artworks = new ArtworkDNA[size];
        for(int i = 0; i < size; i++)
        {
            artworks[i] = ArtworkDNA.Random();
        }
        return new ArtworkPopulation(artworks);
    }

    
}
