using UnityEngine;
using System.Collections;

public class Rocket
{
	int productionCost;
	int buildCost;
	int launchCost;
	int capacity;
	int baseCapacity;
	int researchCost;

	public bool Unlocked { get; protected set;}

	public Rocket (int pc, int bc, int lc, int c, int rc, bool u)
	{
		productionCost = pc;
		buildCost = bc;
		launchCost = lc;
		capacity = c;
		baseCapacity = c;
		researchCost = rc;
		Unlocked = u;
	}

	public int GetProductionCost ()
	{
		return productionCost;
	}

	public int GetBuildCost ()
	{
		return buildCost;
	}

	public int GetCapacity ()
	{
		return capacity;
	}

	public int GetLaunchCost ()
	{
		return launchCost;
	}

	public int GetResearchCost ()
	{
		return researchCost;
	}

	public void UpdateCapacity (int i)
	{
		capacity = (int)(baseCapacity * (1 + i * 0.4f)); 
	}

	public bool Unlock (Research r)
	{
		if (researchCost > r.GetResearchPoints())
		{
			return false;
		}
		r.UnlockShip (researchCost);
		Unlocked = true;
		return true;
	}
}

