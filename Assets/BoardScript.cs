using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardScript : MonoBehaviour {

	public Texture2D backGround;
	public Texture2D textureX;
	public Texture2D textureO;

	private static Rect r = new Rect (Screen.width / 2 - 200, Screen.height/2 - 200, 400, 400);
	private static List<CellScript> cells = new List<CellScript>();
	private List<CellScript> winnerComb = new List<CellScript>();
	private bool playerMove = true;
	private bool gameOver = false;
	private bool playerWin = false;
	private bool draw = false;
	private GUIStyle style1 = new GUIStyle();
	private Computer pc = new Computer();

	void Start()
	{
		Random.seed = (int) System.DateTime.Now.Ticks;
		for( int i = 0; i < 9; i++ )
		{
			cells.Add( new CellScript() );
		}
		style1.fontSize = 35;
	}

	void OnGUI()
	{
		GUILayout.BeginArea( r, backGround );
		GUILayout.EndArea();
		if( Input.GetMouseButtonUp(0) && !gameOver )
		{
			Vector2 point = Event.current.mousePosition;
			float X = point.x;
			float Y = point.y;
			int i = GetColumnOrRow( Y, r.height, r.position.y );
			int j = GetColumnOrRow( X, r.width, r.position.x );

			print( "i = " + i );
			print( "j = " + j );

			if( i < 0 || j < 0 || cells[i*3 + j].getCell().Item2 != "" )
				return;

			if( playerMove )
			{
				print(cells[i*3 + j].getAttr());
				cells[i*3 + j].MakeTexture( i, j, "X" );
				playerMove = false;
				if( checkWinner("X") )
				{
					print("Player");
					gameOver = true;
					playerWin = true;
					return;
				}
				if( checkDraw() )
				{
					draw = true;
					gameOver = true;
					return;
				}
				print( cells[i*3 + j].getAttr() );
				pc.MakeMove();
				print( cells[i*3 + j].getAttr() );
				playerMove = true;
				if( checkWinner("O") )
				{
					print("Computer");
					gameOver = true;
					playerWin = false;
					return;
				}
				if( checkDraw() )
				{
					draw = true;
					gameOver = true;
					return;
				}
			}
		}
		foreach( CellScript cell in cells )
		{
			if( cell.getCell().Item2 == "X" )
				GUI.Label( cell.getCell().Item1, textureX );
			else if( cell.getCell().Item2 == "O" )
				GUI.Label( cell.getCell().Item1, textureO ); 
		}
		if(gameOver)
		{
			if( draw )
			{
				GUILayout.BeginArea( new Rect( 10, Screen.height / 2 - 50, 300, 100 ) );
				GUILayout.Label( "NO ONE WINS", style1 );
			}
			else
				Drawing.DrawLine( winnerComb[0].getCell().Item1.center, winnerComb[1].getCell().Item1.center, Color.red, 9.0f, true );

			if( playerWin && !draw )
			{
				GUILayout.BeginArea( new Rect( 10, Screen.height / 2 - 50, 300, 100 ) );
				GUILayout.Label( "PLAYER WINS!", style1 );
			}
			else if( !draw )
			{
				GUILayout.BeginArea( new Rect( Screen.width - 340, Screen.height / 2 - 50, 310, 100 ) );
				GUILayout.Label( "COMPUTER WINS!", style1 );
			}
			if( GUILayout.Button( "Play again?" ) )
			{
				gameOver = false;
				playerMove = true;
				draw = false;
				pc.setFirstMove();
				cells.Clear();
				winnerComb.Clear();
				Start();
			}
			GUILayout.EndArea();
		}
			
	}
	
	// compareParam -> порівняльний параметр по горизонталі чи вертикалі в залежності від кліку мишки
	// edge -> довжина матриці по вертикалі чи горизонталі
	// position -> глобальна позиція початку матриці позиція х або у
	int GetColumnOrRow( float compareParam, float edge, float position )
	{
		if( compareParam > position && compareParam < position + edge / 3 )
			return 0;
		else if( compareParam > position + edge / 3 && compareParam < position + edge * 2.0f/3 )
			return 1;
		else if( compareParam > position + edge * 2.0f/3 && compareParam < position + edge )
			return 2;

		return -1;
	}

	// Геттери
	public static Rect getBoard()  
	{
		return r;
	}
	
	public static float getTopLeftX()
	{
		return r.position.x;
	}
	public static float getTopLeftY()
	{
		return r.position.y;
	}
	public static List<CellScript> getCells()
	{
		return cells;
	}
	//Cеттери
	public static void SetCell( int i, int j )
	{
		cells[i*3 + j].MakeTexture( i, j, "O" );
	}


	public static void printValue( int value )
	{
		print( value );
	}

	// Провірка на перемогу
	bool checkWinner( string mark )
	{
		bool winner = false;
		for( int i = 0; i < 3; i++ )
		{
			// Порядкова провірка
			for( int j = 0; j < 3; j++ )
			{
				if( cells[i*3 + j].getCell().Item2 == mark )
					winner = true;
				else
				{
					winner = false;
					break;
				}
			}
			if( winner )
			{
				winnerComb.Add( cells[i*3] );
				winnerComb.Add( cells[i*3 + 2] );
				return winner;
			}
				
			// Стовпчикова провірка
			for( int j = 0; j < 3; j++ )
			{
				if( cells[j*3 + i].getCell().Item2 == mark )
					winner = true;
				else
				{
					winner = false;
					break;
				}
			}
			if( winner )
			{
				winnerComb.Add( cells[i] );
				winnerComb.Add( cells[6 + i] );
				return winner;
			}
				
		}
		// Основна та зворотня діагональ
		if( cells[0].getCell().Item2 == mark && cells[4].getCell().Item2 == mark && cells[8].getCell().Item2 == mark )
		{
			winnerComb.Add( cells[0] );
			winnerComb.Add( cells[8] );
			return true;
		}
		if( cells[2].getCell().Item2 == mark && cells[4].getCell().Item2 == mark && cells[6].getCell().Item2 == mark )
		{
			winnerComb.Add( cells[2] );
			winnerComb.Add( cells[6] );
			return true;
		}
			
		return winner;
	}

	bool checkDraw()
	{
		if( cells.FindAll( cell => cell.getAttr() == "" ).Count == 0 )
			return true;

		return false;
	}

}
