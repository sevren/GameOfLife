/**Game of life Java console version
 * Kiril Tzvetanov Goguev
 * Sept 9th, 2014
 * Game of Life rules:
 * 1.Check all 8 surround neighbors from a cell
 * 2. The cell will live iff neighbors = 2 or 3
 * 3. The cell will die iff neighbours <=1 or >=4
 * 4. The cell will become alive iff neighbours==3
 * 5. The cell will stay dead iff neighbours does not ==3
 *
 * TODO: Input file for creating configurations easily
 */

import java.io.*;
import java.util.*;
public class Life {
	private static int[][] board;
	private static int[][] newboard;
	private char[][] txt_board;
	
	
	
	
	private int[][] initIntBoard(int[][] b,int r, int c)
	{
		for (int i=0; i<r;i++)
		{
			for (int j=0;j<c;j++)
			{
				b[i][j]=0;
			}
		}
		
		return b;
	}
	
	private char[][] initBoard(char[][] b,int r, int c)
	{
		for (int i=0; i<r;i++)
		{
			for (int j=0;j<c;j++)
			{
				b[i][j]='.';
			}
		}
		
		return b;
	}
	
	private void testGlider()
	{
		board[1][1]=1;
		board[2][2]=1;
		board[3][2]=1;
		board[3][1]=1;
		board[3][0]=1;
		
	}
	
	private void testBlinker()
	{
		board[6][1]=1;
		board[7][1]=1;
		board[8][1]=1;
		
		
	}
	
	private char[][] dataBoard2CharBoard(int [][] dataBoard)
	{
		//get # of rows
		for (int i=0; i<dataBoard.length;i++)
		{
			//get # of cols
			for (int j=0;j<dataBoard[0].length;j++)
			{
				if (dataBoard[i][j]==0)
				{
					txt_board[i][j]='.';
				}
				else
				{
					txt_board[i][j]='P';
				}
			}
		}
		
		return txt_board;
	}
	
	//print board out
	public void print()
	{
		txt_board=dataBoard2CharBoard(newboard);
		boolean printOnce=false;
		
		for (int j=0;j<txt_board[0].length;j++)
		{
			if (!printOnce)
				System.out.print("   ");
			printOnce=true;
			System.out.print(" "+j);
		}
		
		System.out.println();
		printOnce=false;
		for (int i=0; i<txt_board.length;i++)
		{
			if (i<10)
			{
				System.out.print(i+"   ");
			}
			else
			{
				System.out.print(i+"  ");
			}
			for (int j=0;j<txt_board[0].length;j++)
			{
				System.out.print(txt_board[i][j]+" ");
			}
			printOnce=true;
			System.out.println();
		}
		System.out.println();
		
	}
	
	public int[][] simulate(int[][] board)
	{
		//neighbours counter for each square
		int n=0;
		int[][] newboard=new int[board.length][board[0].length];

		//get # of rows
		for (int i=0; i<board.length;i++)
		{
			//get # of cols
			for (int j=0;j<board[0].length;j++)
			{
				n=0;
				//top-left check (r-1,c-1)
				try
				{
					if (board[i-1][j-1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//top check (r-1,c)
				try
				{
					if (board[i-1][j]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//top-right check (r-1,c+1)
				try
				{
					if (board[i-1][j+1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//right check (r,c+1)
				try
				{
					if (board[i][j+1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//bottom-right check (r+1,c+1)
				try
				{
					if (board[i+1][j+1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//bottom check (r+1,c)
				try
				{
					if (board[i+1][j]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//bottom-Left check (r+1,c-1)
				try
				{
					if (board[i+1][j-1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				//left check (r,c-1)
				try
				{
					if (board[i][j-1]==1)
						n++;
				}
				catch(Exception e)
				{
					
				}
				
				// now apply rules
				
				//loney
				if ((board[i][j]==1) && (n <=1))
				{
					newboard[i][j]=0;
				}
				
				//Overcrowded
				if ((board[i][j]==1) && (n >=4))
				{
					newboard[i][j]=0;
				}
				
				//Continue Living
				if ((board[i][j]==1) && (n ==2 || n == 3))
				{
					newboard[i][j]=1;
				}
				
				//Birth
				if ((board[i][j]==0) && (n ==3))
				{
					newboard[i][j]=1;
				}
				
				//Barren
				if ((board[i][j]==0) && (n !=3))
				{
					newboard[i][j]=0;
				}
				
			
			}
		}
		
		return newboard;
		
	}
	
	public Life(int r, int c)
	{
		board= new int[r][c];
		newboard= new int[r][c];
		txt_board=new char[r][c];
		
		//initalize the data board
		board=initIntBoard(board,r,c);
		newboard=initIntBoard(newboard,r,c);
		//initalize the txt board
		txt_board=initBoard(txt_board,r,c);
		//places a glider;
		testGlider();
		testBlinker();
		
		
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		int n_rows, n_cols, n_iter;
		Scanner reader = new Scanner(System.in);
		System.out.println("Enter the first number");
		//get user input for a
		n_rows=reader.nextInt();
		System.out.println("Enter the second number");
		n_cols=reader.nextInt();
		System.out.println("Enter the # of iterations for the game");
		n_iter=reader.nextInt();
		
		Life L = new Life(n_rows,n_cols);
		newboard=board;
		for(int i=0;i<n_iter;i++)
		{
			System.out.println("iteration: "+i);
			L.print();
			newboard=L.simulate(newboard);
			
			
		}
		
	

	}

}
