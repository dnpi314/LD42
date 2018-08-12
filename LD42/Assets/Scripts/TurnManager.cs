using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
	public GameObject[] launchPrefabs;
	public Transform launchPanel;
	public GameObject[] unlockButtons;
	public GameObject[] buildButtons;

	Display display;
	WorldPopulation worldPopulation;
	Finances finances;
	Workforce workforce;
	Production production;
	Research research;

	int turn = 2200;

	void Start () 
	{
		display = GetComponent<Display> ();

		worldPopulation = new WorldPopulation (1000000, 0.03f, 0.002f, 1500000, 500000);
		finances = new Finances (1000, 0.3f, 0.03f, worldPopulation);
		workforce = new Workforce (50, 10, 0, 0, finances, worldPopulation);
		production = new Production (10, workforce, finances, this);
		research = new Research (0, 40, 60, 60, 80, workforce, production);

		display.SetObjects (worldPopulation, finances, workforce, production, research);
	}

	void Update () 
	{
		
	}

	public void EndofTurn ()
	{
		turn++;
		display.UpdateTurn (turn);

		finances.EndofTurn ();
		worldPopulation.EndofTurn ();
		production.EndofTurn ();
		research.EndofTurn ();

		StartofTurn ();
	}

	void StartofTurn ()
	{
		finances.StartofTurn ();
		worldPopulation.StartofTurn ();
		workforce.UpdatePlanning (0);
	}

	public void LaborPurchace (int n)
	{
		if (n < 0 && workforce.GetLaborers() == 0)
		{
			ErrorTooltip.OpenTooltip ("None Left");
			return;
		}
		if (n < 0 && workforce.GetLaborers() < 10)
		{
			n = -workforce.GetLaborers ();
		}
		if (!workforce.UpdateLabor (n))
		{
			//TODO Display error tooltip
			ErrorTooltip.OpenTooltip ("Not enough funds");
		}
	}

	public void ResearchPurchace (int n)
	{
		if (n < 0 && workforce.GetResearchers() == 0)
		{
			ErrorTooltip.OpenTooltip ("None Left");
			return;
		}
		if (n < 0 && workforce.GetResearchers() < 5)
		{
			n = -workforce.GetResearchers ();
		}
		if (!workforce.UpdateResearch (n))
		{
			//TODO Display error tooltip
			ErrorTooltip.OpenTooltip ("Not enough funds");
		}
	}

	public void PlanningPurchace (int n)
	{
		if (n < 0 && workforce.GetPlanning() == 0)
		{
			ErrorTooltip.OpenTooltip ("None Left");
			return;
		}
		if (!workforce.UpdatePlanning (n))
		{
			//TODO Display error tooltip
			ErrorTooltip.OpenTooltip ("Not enough funds");
		}
	}

	public void DonationPurchace (int n)
	{
		if (n < 0 && workforce.GetDonations() == 0)
		{
			ErrorTooltip.OpenTooltip ("None Left");
			return;
		}
		if (!workforce.UpdateDonation (n))
		{
			//TODO Display error tooltip
			ErrorTooltip.OpenTooltip ("Not enough funds");
		}
	}

	public void ShipPurchase (int t)
	{
		production.StartProduction (t);
	}

	public void ShipConstruction (int t)
	{
		var newShip = Instantiate (launchPrefabs [t], launchPanel);
		newShip.GetComponent<Launch> ().SetValues (production.GetRocketType (t).GetCapacity (), production.GetRocketType (t).GetLaunchCost ());
	}

	public void ShipLaunch (GameObject go)
	{
		var launch = go.GetComponent<Launch> ();

		if (launch.GetLaunchCost() > finances.DisplayFunds())
		{
			ErrorTooltip.OpenTooltip ("Not enough funds");
			return;
		}

		finances.UpdateFunds (-launch.GetLaunchCost ());
		worldPopulation.UpdatePopulation (-launch.GetCapacity ());
		finances.UpdateIncome (0);
		Destroy (go);
	}

	public void ResearchLevel (int i)
	{
		research.ResearchLevel (i);
	}

	public void UnlockShip (int i)
	{
		if(!production.UnlockShip(i, research))
		{
			ErrorTooltip.OpenTooltip ("Not enough rp");
			return;
		}
			
		unlockButtons [i - 1].SetActive (false);
		buildButtons [i - 1].SetActive (true);
	}
}
