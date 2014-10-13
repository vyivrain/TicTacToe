using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Computer {
	private bool first_move_made = false;

	public void MakeMove()
	{
		if( !first_move_made )
		{
			FirstMove();
			first_move_made = true;
		}
		else if( Attack() ){}
		else if( Defend() ){}
		else
		{
			bool madeMove = false;
			while(!madeMove)
			{
				int num = Random.Range( 0, 9 );
				if( BoardScript.getCells()[num].getAttr() == "" )
				{
					BoardScript.SetCell( num / 3, num % 3 );
					madeMove = true;
				}
			}
		}
			

	}
	bool FirstMove()
	{
		if( BoardScript.getCells()[4].getAttr() == "" )
		{
			BoardScript.SetCell( 1, 1 );
			return true;
		}
		else
		{
		   int[] ar = { 1, 3, 5, 7 };
		   int v = Random.Range( 0, 4 );
		   BoardScript.printValue(v);
		   BoardScript.SetCell( ar[v] / 3, ar[v] % 3 );
		   return true;
		}
		return false;
	}

	bool Defend()
	{
		return Attack_or_Defend("X");
	}

	bool Attack()
	{
		return Attack_or_Defend("O");
	}


	bool Attack_or_Defend( string mark )
	{
		int value = 0;
		if( ( value = CheckDefend( BoardScript.getCells()[0].getAttr(), BoardScript.getCells()[1].getAttr(), BoardScript.getCells()[2].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(1);
			BoardScript.printValue(value);
			BoardScript.SetCell( 0, value );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[3].getAttr(), BoardScript.getCells()[4].getAttr(), BoardScript.getCells()[5].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(2);
			BoardScript.printValue(value);
			BoardScript.SetCell( 1, value );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[6].getAttr(), BoardScript.getCells()[7].getAttr(), BoardScript.getCells()[8].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(3);
			BoardScript.printValue(value);
			BoardScript.SetCell( 2, value % 3 );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[0].getAttr(), BoardScript.getCells()[3].getAttr(), BoardScript.getCells()[6].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(4);
			BoardScript.printValue(value);
			BoardScript.SetCell( value, 0 );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[1].getAttr(), BoardScript.getCells()[4].getAttr(), BoardScript.getCells()[7].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(5);
			BoardScript.printValue(value);
			BoardScript.SetCell( value, 1 );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[2].getAttr(), BoardScript.getCells()[5].getAttr(), BoardScript.getCells()[8].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(6);
			BoardScript.printValue(value);
			BoardScript.SetCell( value, 2 );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[0].getAttr(), BoardScript.getCells()[4].getAttr(), BoardScript.getCells()[8].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(7);
			BoardScript.printValue(value);
			BoardScript.SetCell( value, value );
			return true;
		}
		else if( ( value = CheckDefend( BoardScript.getCells()[2].getAttr(), BoardScript.getCells()[4].getAttr(), BoardScript.getCells()[6].getAttr(), mark ) ) != -1 )
		{
			BoardScript.printValue(8);
			BoardScript.printValue(value);
			BoardScript.SetCell( value, 2 - value );
			return true;
		}
		return false;
	}



	// Провірка окремих ситуацій, при яких необхідно захищатись
	int CheckDefend( string item1, string item2, string item3, string mark )
	{
		List<string> check = new List<string>();
		check.Add ( item1 );
		check.Add ( item2 );
		check.Add ( item3 );
		if( check.FindAll( s => s.Equals(mark) ).Count == 2 && check.Contains( "" ) )
			return check.IndexOf( "" );
			
		return -1;
	}

	//Сеттери
	public void setFirstMove()
	{
		first_move_made = false;
	}
}
