using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research
{
	int researchPoints;
	Workforce workforce;
	Production production;

	int[] researchLevels = new int[]{0,0,0,0};
	int[] baseCosts;

	public Research (int rp, int mc, int cry, int bc, int cam, Workforce w, Production p)
	{
		researchPoints = rp;
		baseCosts = new int[]{mc, cry, bc, cam};
		workforce = w;
		production = p;
	}

	public void EndofTurn ()
	{
		researchPoints += workforce.GetResearchers ();
	}

	public bool ResearchLevel (int i)
	{
		switch (i) 
		{
		case 0:
			if (GetResearchCost(i) > researchPoints) {
				ErrorTooltip.OpenTooltip ("Not enough rp");
				return false;
			}
			researchPoints -= GetResearchCost(i);
			researchLevels[i]++;
			production.UpdateProductionRate (researchLevels[i]);
			return true;
		case 1:
			if (GetResearchCost (i) > researchPoints) {
				ErrorTooltip.OpenTooltip ("Not enough rp");
				return false;
			}
			researchPoints -= GetResearchCost (i);
			researchLevels[i]++;
			production.UpdateCapacity (researchLevels[i]);
			return true;
		case 2:
			if (GetResearchCost (i) > researchPoints) {
				ErrorTooltip.OpenTooltip ("Not enough rp");
				return false;
			}
			researchPoints -= GetResearchCost (i);
			researchLevels [i]++;
			workforce.UpdatePlanningStrength (researchLevels [i]);
			return true;
		case 3:
			if (GetResearchCost (i) > researchPoints) {
				ErrorTooltip.OpenTooltip ("Not enough rp");
				return false;
			}
			researchPoints -= GetResearchCost (i);
			researchLevels [i]++;
			workforce.UpdateDonationStrength (researchLevels [i]);
			return true;
		default:
			Debug.LogError ("Not a valid research");
			return false;
		}
	}

	public int GetResearchPoints ()
	{
		return researchPoints;
	}

	public int GetResearchCost (int i)
	{
		return baseCosts[i] * (int)Mathf.Pow (2, researchLevels[i]);
	}

	public int GetResearchLevel (int i)
	{
		return researchLevels [i];
	}

	public void UnlockShip (int i)
	{
		researchPoints -= i;
	}
}
