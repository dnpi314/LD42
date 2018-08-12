using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workforce
{
	Finances finances;
	WorldPopulation worldPopulation;

	int laborers;
	int laborerCost = 1;
	int laborerHireCost = 10;

	int researchers;
	int researcherCost = 10;
	int researcherHireCost = 60;

	int planningCenters;
	int planningCenterCost = 10;
	int planningCenterBuildCost = 200;
	float planningCenterStrength = 0.03f;
	float basePlanningStrength = 0.03f;

	int donationCenters;
	int donationCenterBuildCost = 300;
	float donationCenterStrength = 0.03f;
	float baseDonationStrength = 0.03f;

	public Workforce (int l, int r, int pc, int dc, Finances f, WorldPopulation wp)
	{
		laborers = l;
		researchers = r;
		planningCenters = pc;
		donationCenters = dc;
		finances = f;
		worldPopulation = wp;

		int expenses = (l * laborerCost) + (r * researcherCost) + (pc * planningCenterCost);
		finances.UpdateExpenses (expenses);
	}

	public bool UpdateLabor (int change)
	{
		if (finances.DisplayFunds () < change * laborerHireCost && change > 0)
		{
			return false;
		}

		laborers += change;

		if(change > 0)
		{
			finances.UpdateFunds (-change * laborerHireCost);
		}
		if(change < 0)
		{
			finances.UpdateFunds (-change * laborerHireCost / 2);
		}

		finances.UpdateExpenses (change * laborerCost);

		return true;
	}

	public bool UpdateResearch (int change)
	{
		if (finances.DisplayFunds () < change * researcherHireCost && change > 0)
		{
			return false;
		}

		researchers += change;

		if(change > 0)
		{
			finances.UpdateFunds (-change * researcherHireCost);
		}
		if(change < 0)
		{
			finances.UpdateFunds (-change * researcherHireCost / 2);
		}

		finances.UpdateExpenses (change * researcherCost);

		return true;
	}

	public bool UpdatePlanning (int change)
	{
		if (finances.DisplayFunds () < change * planningCenterBuildCost && change > 0)
		{
			return false;
		}

		planningCenters += change;

		if(change > 0)
		{
			finances.UpdateFunds (-change * planningCenterBuildCost);
		}
		if(change < 0)
		{
			finances.UpdateFunds (-change * planningCenterBuildCost / 2);
		}

		finances.UpdateExpenses (change * planningCenterCost);

		worldPopulation.UpdateGrowthRate (Mathf.Pow(1-planningCenterStrength, planningCenters));

		return true;
	}

	public bool UpdateDonation (int change)
	{
		if (finances.DisplayFunds () < change * donationCenterBuildCost && change > 0)
		{
			return false;
		}

		donationCenters += change;

		if(change > 0)
		{
			finances.UpdateFunds (-change * donationCenterBuildCost);
		}
		if(change < 0)
		{
			finances.UpdateFunds (-change * donationCenterBuildCost / 2);
		}

		finances.UpdateIncome (change * donationCenterStrength);

		return true;
	}

	public int GetLaborers ()
	{
		return laborers;
	}

	public int GetLaborerCost ()
	{
		return laborers * laborerCost;
	}

	public int GetResearchers ()
	{
		return researchers;
	}

	public int GetResearcherCost ()
	{
		return researchers * researcherCost;
	}

	public int GetPlanning ()
	{
		return planningCenters;
	}

	public int GetPlanningCost ()
	{
		return planningCenters * planningCenterCost;
	}

	public int GetDonations ()
	{
		return donationCenters;
	}

	public void UpdatePlanningStrength (int i)
	{
		planningCenterStrength = basePlanningStrength * (1 + i * 0.4f);
		UpdatePlanning (0);
	}

	public void UpdateDonationStrength (int i)
	{
		donationCenterStrength = baseDonationStrength * (1 + i * 0.4f);
		finances.UpdateIncome (baseDonationStrength * 0.4f * donationCenters);
	}
}
