using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour 
{
	public Text populationDisplay;
	WorldPopulation worldPopulation;

	public Text moneyDisplay;
	Finances finances;

	public Text laborerDisplay;
	public Text researcherDisplay;
	public Text planningDisplay;
	public Text donationDisplay;
	Workforce workforce;

	public Text productionTypeDisplay;
	public Text productionRemainingDisplay;
	Production production;

	public Text researchDisplay;
	public Text manCostDisplay;
	public Text manLevelDisplay;
	public Text cryCostDisplay;
	public Text cryLevelDisplay;
	public Text birthCostDisplay;
	public Text birthLevelDisplay;
	public Text campCostDisplay;
	public Text campLevelDisplay;
	Research research;

	public Text yearDisplay;
	int turn = 2200;

	void Start () 
	{
		
	}

	void Update () 
	{
		populationDisplay.text = string.Format("{0}B ({1})", worldPopulation.DisplayPopulation(), worldPopulation.DisplayChange());

		if (production.GetProductionType() == -1)
		{
			int profit = finances.GetChange ();
			profit += production.GetWealthProduction ();
			string displayChange = "";

			if (profit > 0)
			{
				displayChange = "+" + profit;
			}
			else
			{
				displayChange = profit.ToString ();
			}

			moneyDisplay.text = string.Format ("{0}M ({1})", finances.DisplayFunds (), displayChange);
		}
		else
		{
			moneyDisplay.text = string.Format ("{0}M ({1})", finances.DisplayFunds (), finances.DisplayChange ());
		}
			
		laborerDisplay.text = string.Format ("Laborers: {0}K\nCost: -{1}M p/t", workforce.GetLaborers (), workforce.GetLaborerCost ());
		researcherDisplay.text = string.Format ("Researchers: {0}K\nCost: -{1}M p/t", workforce.GetResearchers (), workforce.GetResearcherCost ());
		planningDisplay.text = string.Format ("Family Planning Centers: {0}K\nCost: -{1}M p/t", workforce.GetPlanning (), workforce.GetPlanningCost ());
		donationDisplay.text = string.Format ("Donation Centers: {0}K", workforce.GetDonations ());

		switch (production.GetProductionType()) 
		{
		case -1:
			productionTypeDisplay.text = "Consumer Goods";
			break;
		case 0:
			productionTypeDisplay.text = "Basic Colony Ship";
			break;
		case 1:
			productionTypeDisplay.text = "Advanced Colony Ship";
			break;
		case 2:
			productionTypeDisplay.text = "Mega Colony Ship";
			break;
		case 3:
			productionTypeDisplay.text = "Colony Ark";
			break;
		default:
			productionTypeDisplay.text = "None";
			break;
		}

		if (production.GetProductionType() == -1)
		{
			productionRemainingDisplay.text = string.Format ("+{0}M Funds", production.GetWealthProduction ());
		}
		else
		{
			productionRemainingDisplay.text = string.Format ("Remaining: {0}P (-{1})", production.GetProductionLeft (), production.GetProductionRate ());
		}

		researchDisplay.text = string.Format ("{0} (+{1})", research.GetResearchPoints (), workforce.GetResearchers());
		manCostDisplay.text = string.Format ("{0}", research.GetResearchCost (0));
		manLevelDisplay.text = research.GetResearchLevel (0).ToString ();
		cryCostDisplay.text = string.Format ("{0}", research.GetResearchCost (1));
		cryLevelDisplay.text = research.GetResearchLevel (1).ToString ();
		birthCostDisplay.text = string.Format ("{0}", research.GetResearchCost (2));
		birthLevelDisplay.text = research.GetResearchLevel (2).ToString ();
		campCostDisplay.text = string.Format ("{0}", research.GetResearchCost (3));
		campLevelDisplay.text = research.GetResearchLevel (3).ToString ();

		yearDisplay.text = string.Format ("{0} CE", turn);
	}

	public void SetObjects (WorldPopulation wp, Finances f, Workforce w, Production p, Research r)
	{
		worldPopulation = wp;
		finances = f;
		workforce = w;
		production = p;
		research = r;
	}
		
	public void UpdateTurn (int i)
	{
		turn = i;
	}
}
