using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finances
{
	WorldPopulation worldPopulation;
	float donationRate;
	float donationRateAcceleration;

	int funds;
	int income;
	int expenses;

	public Finances (int f, float dr, float dra, WorldPopulation wp)
	{
		funds = f;
		donationRate = dr;
		donationRateAcceleration = dra;
		worldPopulation = wp;

		income = (int)(worldPopulation.DisplayPopulation () * donationRate);
	}

	public void EndofTurn ()
	{
		funds += income - expenses;
	}

	public void StartofTurn ()
	{
		UpdateIncome (donationRateAcceleration);
	}

	public int DisplayFunds ()
	{
		return funds;
	}

	public string DisplayChange ()
	{
		int profit = income - expenses;

		if(profit > 0)
		{
			return "+" + profit;
		}
		else
		{
			return profit.ToString ();
		}
	}

	public int GetChange ()
	{
		return income - expenses;
	}

	public void UpdateExpenses (int change)
	{
		expenses += change;
	}

	public void UpdateIncome (float change)
	{
		donationRate += change;
		income = (int)(worldPopulation.DisplayPopulation () * donationRate);
	}

	public void UpdateFunds (int change)
	{
		funds += change;
	}
}
