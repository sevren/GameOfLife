using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    
    public partial class GOL : Form
    {
        private bool[,] board;
        private Label[,] cells;

        private bool[,] initIntBoard(bool[,] b,int r, int c)
        {
	        for (int i=0; i<r;i++)
	        {
		        for (int j=0;j<c;j++)
		        {
			        b[i,j]=false;
		        }
	        }
	
	        return b;
        }

        private void translateToGui(bool[,] newboard2)
        {
            //get # of rows
            for (int i = 0; i < newboard2.GetLength(0); i++)
            {
                //get # of cols
                for (int j = 0; j < newboard2.GetLength(1); j++)
                {
                    if (!newboard2[i,j])
                    {
                        cells[i,j].BackColor=Color.Gray;
                    }
                    else
                    {
                        cells[i,j].BackColor=Color.Yellow;
                    }
                }
            }
        }

    public bool[,] simulate(bool[,] board)
    {
	    //neighbours counter for each square
	    int n=0;
	    //complete waste of memory for now
	    bool[,] newboard=new bool[board.GetLength(0),board.GetLength(1)];

	
	    //get # of rows
	    for (int i=0; i<board.GetLength(0);i++)
	    {
		    //get # of cols
		    for (int j=0;j<board.GetLength(1);j++)
		    {
			    n=0;
			
			    //
			     // Abuse of the Exceptions for now  :( a better method would be calculating the edges
			     //
			
			    //top-left check (r-1,c-1)
			    try
			    {
				    if (board[i-1,j-1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //top check (r-1,c)
			    try
			    {
				    if (board[i-1,j])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //top-right check (r-1,c+1)
			    try
			    {
				    if (board[i-1,j+1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //right check (r,c+1)
			    try
			    {
				    if (board[i,j+1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //bottom-right check (r+1,c+1)
			    try
			    {
				    if (board[i+1,j+1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //bottom check (r+1,c)
			    try
			    {
				    if (board[i+1,j])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //bottom-Left check (r+1,c-1)
			    try
			    {
				    if (board[i+1,j-1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    //left check (r,c-1)
			    try
			    {
				    if (board[i,j-1])
					    n++;
			    }
			    catch(Exception e)
			    {
				
			    }
			
			    // now apply rules
			
			    //loney
			    if ((board[i,j]) && (n <=1))
			    {
				    newboard[i,j]=false;
			    }
			
			    //Overcrowded
			    if ((board[i,j]) && (n >=4))
			    {
				    newboard[i,j]=false;
			    }
			
			    //Continue Living
			    if ((board[i,j]) && (n ==2 || n == 3))
			    {
				    newboard[i,j]=true;
			    }
			
			    //Birth
			    if ((!board[i,j]) && (n ==3))
			    {
				    newboard[i,j]=true;
			    }
			
			    //Barren
			    if ((!board[i,j]) && (n !=3))
			    {
				    newboard[i,j]=false;
			    }
			
		
		    }
	    }
	
	    return newboard;
	
    }


        private void GenerateTable(int rowCount, int columnCount)
        {
            //Clear out the existing controls, we are generating a new table layout
            tableLayoutPanel1.Controls.Clear();

            //Clear out the existing row and column styles
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            //Now we will generate the table, setting up the row and column counts first
            tableLayoutPanel1.ColumnCount = columnCount;
            tableLayoutPanel1.RowCount = rowCount;

            for (int x = 0; x < rowCount; x++)
            {
                //First add a column
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                for (int y = 0; y < columnCount; y++)
                {
                    //Next, add a row.  Only do this when once, when creating the first column
                    if (x == 0)
                    {
                        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    }

                    //Create the control, in this case we will add a label
                    Label lbl = new Label();
                    
                    lbl.BackColor = Color.Gray;
                    lbl.BorderStyle = BorderStyle.Fixed3D;
                    lbl.Size = new System.Drawing.Size(20, 20);
                    lbl.Click += new System.EventHandler(this.label_onClick);
                    lbl.Name = x + "," + y;
                    lbl.BackColorChanged += new System.EventHandler(this.label_colorChanged);
                    cells[x, y] = lbl;

                    //Finally, add the control to the correct location in the table
                    tableLayoutPanel1.Controls.Add(cells[x,y], y, x);
                }
            }
        }

        void lbl_BackColorChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public GOL()
        {
            int r=15;
            int c=25;
            board = new bool[r, c];
            cells = new Label[r, c];
            InitializeComponent();
            GenerateTable(r, c);
            
            board=initIntBoard(board, r, c);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_onClick(object sender, EventArgs e)
        {
            //cast the object as a Label;
            Label lbl = (Label)sender;
            //split on the ',' in the name of the label =>tokens[0] is row and tokens[1] is column
            string[] tokens = lbl.Name.Split(new string[] { "," }, StringSplitOptions.None);
           

            if (lbl.BackColor == Color.Gray)
            {
                lbl.BackColor = Color.Yellow;
                board[Convert.ToInt32(tokens[0]),Convert.ToInt32(tokens[1])] = true;

            }
            else
            {
                lbl.BackColor = Color.Gray;
                board[Convert.ToInt32(tokens[0]),Convert.ToInt32(tokens[1])] = false;

            }
            

        }

        private void label_colorChanged(object sender, EventArgs e)
        {
            //cast the object as a Label
           // Label lbl = (Label)sender;
            //split on the ',' in the name of the label =>tokens[0] is row and tokens[1] is column
            //string[] tokens = lbl.Name.Split(new string[] { "," }, StringSplitOptions.None);
            //Console.WriteLine("Label "+ lbl.Name+" has changed color!");
            //Console.WriteLine("Tokens are " + tokens[0] +" and "+tokens[1]);
            
            //board[Convert.ToInt32(tokens[0]),Convert.ToInt32( tokens[1])] = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            board = simulate(board);
            translateToGui(board);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

       
    }
}
