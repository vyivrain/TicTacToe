using UnityEngine;
using System.Collections;
using Eppy;

public class CellScript {
	private float cell_width;
	private float cell_height;
	private string attr = "";
	private Vector2 top_point;
	private float topLeftX;
	private float topLeftY;
	private Rect rect;


	public CellScript () {
		cell_width = BoardScript.getBoard().width / 3;
		cell_height = BoardScript.getBoard().height / 3;
		topLeftX = BoardScript.getTopLeftX();
		topLeftY = BoardScript.getTopLeftY();
	}

	public CellScript( Rect rect, string attr )
	{
		this.rect = rect;
		this.attr = attr;

		cell_width = BoardScript.getBoard().width / 3;
		cell_height = BoardScript.getBoard().height / 3;
		topLeftX = BoardScript.getTopLeftX();
		topLeftY = BoardScript.getTopLeftY();
	}

	public Tuple<Rect,string> getCell()
	{	
		if( rect != null )
			return new Tuple<Rect,string>( rect, attr );
		else
			return null;
	}

	int GenerateOffset( int pos )
	{
		switch(pos)
		{
		case 0:
			return 0;
		case 1:
			return 3;
		case 2:
			return 10;
		}
		
		return -1;
	}
	// Ініціалізація комірки
	public void MakeTexture( int i, int j, string attribute )
	{
		attr = attribute;
		float offsetX = GenerateOffset(j);
		float offsetY = GenerateOffset(i);
		rect = new Rect( BoardScript.getTopLeftX() + j * cell_width + offsetX, BoardScript.getTopLeftY() + i * cell_height + offsetY, cell_width - 25, cell_height - 25 );
	}

	// Геттери
	public Rect getRect()
	{
		return rect;
	}

	public string getAttr()
	{
		return attr;
	}
}
