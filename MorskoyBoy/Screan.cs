using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{

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

		public enum DrowDisplay
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


		StateGameEnum one = StateGameEnum.INIZIALIZATE;

		private DrowDisplay[,] player1 = new DrowDisplay[10, 10];
		private DrowDisplay[,] player2 = new DrowDisplay[10, 10];
		private DrowDisplay[,] fieldTargetPlayer1 = new DrowDisplay[10, 10];
		private DrowDisplay[,] fieldTargetPlayer2 = new DrowDisplay[10, 10];

		private int numberPlayer = 1;
		private char target = '+';

		public char[] DrowSymbol
		{
			get { return drowSymbol; }
		}

		Point point = new Point(); //create x y coordinate

		public void DrowField(DrowDisplay[,] player, Point point)
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
					Console.Write(drowSymbol[(int) player[i, j]]);
				}
				for (j = 12; j < 17; j++)
				{
					Console.Write(display[i + 2, j]);
				}
				for (j = 0; j < 10; j++)
				{
					// field 2
					if (i == point.getY() && j == point.getX())
					{
						Console.Write(target);
					}
					else
					{
						//Console.Write(" ");
						Console.Write(drowSymbol[(int) fieldTargetPlayer1[i, j]]);
					}
				}
				Console.WriteLine(display[i + 2, 27]);
			}

			for (j = 0; j < 28; j++)
			{
				Console.Write(display[12, j]);
			}
		}




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
						TmpDrowShip2Player(player2);
						player1[0, 0] = DrowDisplay.KILL;
						//fieldTargetPlayer1[0, 0] = DrowDisplay.KILL;

						one = StateGameEnum.DRAW;
						break;
					}
					case StateGameEnum.DRAW:
					{

							Console.ForegroundColor = ConsoleColor.Green;
							DrowField(numberPlayer == 0 ? player1 : player2, point);



							if (numberPlayer == 0)
						{
							fieldTargetPlayer1[1, 2] = DrowDisplay.SHIP;
						}
						else
						{
							fieldTargetPlayer1[1, 2] = DrowDisplay.STRIKE;
						}



						if (TargetPosition(point))
						{
							if (FieldTarget(player2, fieldTargetPlayer1))
							{
									one = StateGameEnum.PROCESING;
							}
							



							Console.ReadKey();
							
						}
						Console.Clear();
						break;
					}
					case StateGameEnum.PROCESING:
					{
						numberPlayer = numberPlayer == 0 ? 1 : 0;
						one = StateGameEnum.INIZIALIZATE;
						point.x = 0;
						point.y = 0;

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

		public void TmpDrowShip2Player(DrowDisplay[,] player)
		{

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					player[i, j] = DrowDisplay.EMPTY;
				}
			}




			player[0, 3] = DrowDisplay.SHIP;
			player[0, 1] = DrowDisplay.SHIP;
			player[0, 2] = DrowDisplay.SHIP;
			player[0, 4] = DrowDisplay.SHIP;

			player[3, 8] = DrowDisplay.SHIP;
			player[3, 9] = DrowDisplay.SHIP;

			player[1, 7] = DrowDisplay.SHIP;
			player[3, 4] = DrowDisplay.SHIP;

			player[8, 8] = DrowDisplay.SHIP;
			player[8, 9] = DrowDisplay.SHIP;

			player[3, 9] = DrowDisplay.SHIP;
			player[4, 9] = DrowDisplay.SHIP;
			player[5, 9] = DrowDisplay.SHIP;

			player[1, 6] = DrowDisplay.SHIP;
			player[9, 1] = DrowDisplay.SHIP;
			player[4, 6] = DrowDisplay.SHIP;
			player[5, 6] = DrowDisplay.SHIP;

			player[5, 2] = DrowDisplay.SHIP;

			player[7, 5] = DrowDisplay.SHIP;

		}

		public bool FieldTarget(DrowDisplay[,] player, DrowDisplay[,] fieldTarget)
		{
			if (player[point.y, point.x] == DrowDisplay.EMPTY)
			{
				fieldTarget[point.y, point.x] = DrowDisplay.SHOT;
				return true;
			}
			if (player[point.y, point.x] == DrowDisplay.SHIP)
			{
				fieldTarget[point.y, point.x] = DrowDisplay.STRIKE;
			}

			return false;
		}

		//public bool DrowField()
	

	//void int Position(x, y, out z);
		public bool TargetPosition( Point point)
		{
			var isUp = Console.ReadKey().Key;
			switch (isUp)
			{
				case ConsoleKey.UpArrow:
					if (point.y > 0)
					{
						point.y--;
					}
					break;
				case ConsoleKey.DownArrow:
					if (point.y < 9)
					{
						point.y++;
					}

					
					break;
				case ConsoleKey.RightArrow:
					if (point.x < 9)
					{
						point.x++;
					}
					break;
				case ConsoleKey.LeftArrow:
					if (point.x > 0)
					{
						point.x--;
					}
					break;
				default:
					Console.WriteLine("Default case");
					break;

				
			}
			
			if (isUp == ConsoleKey.Enter)
			{ Console.WriteLine("Enter");
				return true;
			}
			else
			{
				return false;
			}

		}
}
}