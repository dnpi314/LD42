using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPopulation
{
	int population;
	float growthRate;
	float baseGrowthRate;
	float growthRateAcceleration;
	int maxPopulation;
	int minPopulation;

	public WorldPopulation (int p, float gr, float gra, int maxp, int minp)
	{
		population = p;
		growthRate = gr;
		baseGrowthRate = gr;
		growthRateAcceleration = gra;
		maxPopulation = maxp;
		minPopulation = minp;
	}

	public void EndofTurn ()
	{
		if (population <= minPopulation)
		{
			SceneManagement.Victory ();
		}

		population = (int)(population * (1 + growthRate));

		if (population >= maxPopulation)
		{
			SceneManagement.GameOver ();
		}
	}

	public void StartofTurn ()
	{
		baseGrowthRate += growthRateAcceleration;
	}

	public int DisplayPopulation ()
	{
		return population/1000;
	}

	public string DisplayChange()
	{
		int change = (int)(population * growthRate) / 1000;
		return "+" + change;
	}

	public void UpdatePopulation (int change)
	{
		population += change * 1000;
	}

	public void UpdateGrowthRate (float change)
	{
		growthRate = baseGrowthRate * change;
	}
}
