using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production
{
	Workforce workforce;
	Finances finances;
	TurnManager turnManager;

	int productionRate;
	int baseProductionRate;
	int productionOverflow = 0;

	Rocket[] rocketTypes;

	struct CurrentProduction
	{
		public int productionLeft;
		public int rocketType;

		public CurrentProduction(int p, int t)
		{
			productionLeft = p;
			rocketType = t;
		}
	}

	CurrentProduction currentProduction;

	public Production (int pr, Workforce w, Finances f, TurnManager tm)
	{
		productionRate = pr;
		baseProductionRate = pr;
		workforce = w;
		finances = f;
		turnManager = tm;

		rocketTypes = new Rocket[4];
		rocketTypes [0] = new Rocket (2000, 250, 100, 50, 0, true);
		rocketTypes [1] = new Rocket (3000, 400, 200, 100, 50, false);
		rocketTypes [2] = new Rocket (5000, 800, 300, 200, 100, false);
		rocketTypes [3] = new Rocket (10000, 1500, 500, 500, 200, false);

		currentProduction = new CurrentProduction (0, -1);
	}

	public void EndofTurn ()
	{
		currentProduction.productionLeft -= productionRate * workforce.GetLaborers ();

		if (currentProduction.productionLeft <= 0)
		{
			if (currentProduction.rocketType == -1)
			{
				finances.UpdateFunds ((int)(productionRate * workforce.GetLaborers () * 0.2f));
				currentProduction.productionLeft = 0;
			}
			else
			{
				turnManager.ShipConstruction (currentProduction.rocketType);
				productionOverflow = currentProduction.productionLeft;
				currentProduction = new CurrentProduction (0, -1);
			}
		}
	}

	public void StartProduction (int t)
	{
		if (!rocketTypes[t].Unlocked) 
		{
			Debug.LogError ("Rocket not Unlocked");
			return;
		}

		if (currentProduction.rocketType != -1)
		{
			ErrorTooltip.OpenTooltip ("Rocket already under construction");
			return;
		}

		if (rocketTypes[t].GetBuildCost() > finances.DisplayFunds())
		{
			ErrorTooltip.OpenTooltip ("Not enough funds");
			return;
		}

		finances.UpdateFunds (-rocketTypes [t].GetBuildCost ());
		currentProduction = new CurrentProduction (rocketTypes [t].GetProductionCost (), t);
		currentProduction.productionLeft += productionOverflow;
		productionOverflow = 0;
	}

	public int GetProductionType ()
	{
		return currentProduction.rocketType;
	}

	public int GetProductionLeft ()
	{
		return currentProduction.productionLeft;
	}

	public int GetWealthProduction ()
	{
		return (int)(productionRate * workforce.GetLaborers () * 0.2f);
	}

	public int GetProductionRate ()
	{
		return productionRate * workforce.GetLaborers ();
	}

	public Rocket GetRocketType (int i)
	{
		return rocketTypes [i];
	}

	public void UpdateProductionRate (int l)
	{
		productionRate = (int)(baseProductionRate * (1 + l * 0.4f));
	}

	public void UpdateCapacity (int i)
	{
		foreach (var rocket in rocketTypes) 
		{
			rocket.UpdateCapacity (i);
		}
	}

	public bool UnlockShip (int i, Research r)
	{
		return rocketTypes [i].Unlock (r);
	}
}
