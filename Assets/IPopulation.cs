using System.Collections.Generic;
public interface IPopulation
{
    IDNA[] GetIndividuals();
    IPopulation GenerateNewPopulation();
}
