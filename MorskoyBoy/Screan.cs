using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
	public enum DrowDisplay //: short
	{
		EMPTY = 0,
		SHOT,
		STRIKE,
		KILL,
		SHIP,
	}

	enum StateGameEnum
	{
		INIZIALIZATE = 0,
		DRAW,
		PROCESING,
		EXIT
	}


	class Screan
	{
		private char[,] display =
		{
			{
				' ', ' ', 'A', 'B', 'C', 'D', 'F', 'G', 'H', 'K', 'L', 'M', ' ', ' ', ' ', ' ', ' ', 'A', 'B', 'C', 'D', 'F', 'G',
				'H', 'K', 'L', 'M', ' '
			},
			{
				' ', '|', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '|', ' ', ' ', ' ', '|', '-', '-', '-', '-', '-', '-',
				'-', '-', '-', '-', '|',
			},
			{
				'0', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '0', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'1', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '1', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'2', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '2', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'3', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '3', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'4', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '4', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'5', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '5', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'6', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '6', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'7', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '7', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'8', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '8', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				'9', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', ' ', '9', '|', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', '|'
			},
			{
				' ', '|', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '|', ' ', ' ', ' ', '|', '-', '-', '-', '-', '-', '-',
				'-', '-', '-', '-', '|'
			}
		};

		private char[] drowSymbol =
		{
			' ', //EMPTY
			'.', //SHOT,
			'*', //STRIKE,
			'X', //KILL,
			'$' //SHIP,
		};

		StateGameEnum one = StateGameEnum.INIZIALIZATE;

		private DrowDisplay[,] player1 = new DrowDisplay[10, 10];
		private DrowDisplay[,] player2 = new DrowDisplay[10, 10];

		private int numberPlayer = 1;
		private char target = '+';

		public int x=0;
		public int y=0;

		public char[] DrowSymbol
		{
			get { return drowSymbol; }
		}

		public void DrowField(DrowDisplay[,] player, int x,int y)
		{
			int i, j, k;
			for (i = 0; i < 2; i++)
			{
				for (j = 0; j < 28; j++)
				{
					Console.Write(display[i, j]);

				}
				Console.WriteLine();
			}

			for (i = 0; i < 10; i++)
			{
				Console.Write(display[i + 2, 0]);
				Console.Write(display[i + 2, 1]);
				for (j = 0; j < 10; j++)
				{
					Console.Write(drowSymbol[(int)player[i, j]]);
					}
				for (j = 12; j < 17; j++)
				{
					Console.Write(display[i + 2, j]);
				}
				for (j = 0; j < 10; j++)
				{
					 // field 2
					if (i == y && j == x)
					{
						Console.Write(target);
					}
					else
					{
						Console.Write(" ");
					}
				}
				Console.WriteLine(display[i + 2, 27]);
		}

			for (j = 0; j < 28; j++)
			{
				Console.Write(display[12, j]);
			}
			Console.Write(drowSymbol[(int) DrowDisplay.STRIKE]);
		}


		private int p;

		public void StateGame()
		{
			bool flag = true;
			while (flag)
			{
				switch (one)
				{
					case StateGameEnum.INIZIALIZATE:
					{
						  DrowShip(player1);
							DrowShip(player2);
							player1[0,0]= DrowDisplay.KILL;
							one = StateGameEnum.DRAW;
						break;
					}
					case StateGameEnum.DRAW:
					{
						DrowField(numberPlayer == 0 ? player1 : player2,x,y);
						Console.ReadKey();
						one = StateGameEnum.PROCESING;
						Console.Clear();
					break;
					}
					case StateGameEnum.PROCESING:
				{
							numberPlayer = numberPlayer == 0 ? 1 : 0;
							one = StateGameEnum.INIZIALIZATE;
						break;
					}
					case StateGameEnum.EXIT:
					{
						break;
					}
				}
			}

		}


		public void DrowShip(DrowDisplay[,] player)
		{
			/*  12345678910 
			 * 1$ $       
			 * 2        $  
			 * 3        S   
			 * 4        S
			 * 5 $      S
			 * 6
			 * 7    &
			 * 8
			 * 9
			 * 10
			 */

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					player[i, j] = DrowDisplay.EMPTY;
			}
		}




	    player[0, 2] = DrowDisplay.SHIP;
			player[0, 0] = DrowDisplay.SHIP;

			player[2, 9] = DrowDisplay.SHIP;
			player[3, 9] = DrowDisplay.SHIP;
			player[4, 9] = DrowDisplay.SHIP;
			player[5, 9] = DrowDisplay.SHIP;

			player[2, 6] = DrowDisplay.SHIP;
			player[3, 6] = DrowDisplay.SHIP;
			player[4, 6] = DrowDisplay.SHIP;
			player[5, 6] = DrowDisplay.SHIP;

			player[5, 2] = DrowDisplay.SHIP;

			player[7, 5] = DrowDisplay.SHIP;

		}



		void int Position(x, y, out z);
		public void Position( int x, int y, out int z)
		{
			x = 36;
			z=y=25;

			//return z;
		}
		
		public void Sqr(ref int i)
		{
			i = i * i;
		}

	}
}