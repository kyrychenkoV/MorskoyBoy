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
			'*', //SHOT,
			'#', //STRIKE,
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

		public void DrowField(DrowDisplay[,] player, Point point, DrowDisplay[,] fieldTargetplayer)
		{
			int i, j;
			//Drow 2 first strings Display
			for (i = 0; i < 2; i++)
			{
				for (j = 0; j < 28; j++)
				{
					Console.Write(display[i, j]);

				}
				Console.WriteLine();
			}
			//Drow 3-12 strings Display
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
						Console.Write(drowSymbol[(int) fieldTargetplayer[i, j]]);
					}
				}
				Console.WriteLine(display[i + 2, 27]);
			}

			//Drow 13 string Display
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

						one = StateGameEnum.DRAW;
						break;
					}
					case StateGameEnum.DRAW:
					{
						Console.ForegroundColor = ConsoleColor.Green;
						if (numberPlayer == 0)
						{
							DrowField(player1, point, fieldTargetPlayer1);
						}
						else
						{
							DrowField(player2, point, fieldTargetPlayer2);
						}
						// при нажатии ентер (выстрел)
						if (TargetPosition(point))
						{
							if (numberPlayer == 0)
							{
								if (DrowFieldTarget(player2, fieldTargetPlayer1))
								{
									one = StateGameEnum.PROCESING;
								}
								else
								{
									Console.WriteLine("Продолжаем стрелять");
								}
							}
							else
							{
								if (DrowFieldTarget(player1, fieldTargetPlayer2))
								{
									one = StateGameEnum.PROCESING;
								}
								else
								{
									Console.WriteLine("Продолжаем стрелять");
								}
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
			player[0, 4] = DrowDisplay.SHIP;
			player[0, 0] = DrowDisplay.SHIP;
			player[0, 9] = DrowDisplay.SHIP;
			player[1, 9] = DrowDisplay.SHIP;


			player[5, 9] = DrowDisplay.SHIP;
			player[9, 9] = DrowDisplay.SHIP;
			player[9, 5] = DrowDisplay.SHIP;
			player[9, 0] = DrowDisplay.SHIP;
			player[5, 0] = DrowDisplay.SHIP;

			player[7, 3] = DrowDisplay.SHIP;

			player[2, 3] = DrowDisplay.SHIP;
			player[2, 4] = DrowDisplay.SHIP;
			player[2, 5] = DrowDisplay.SHIP;
			player[2, 6] = DrowDisplay.SHIP;
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
			player[0, 4] = DrowDisplay.SHIP;
			player[0, 0] = DrowDisplay.SHIP;
			player[0, 9] = DrowDisplay.SHIP;
			player[5, 9] = DrowDisplay.SHIP;
			player[9, 9] = DrowDisplay.SHIP;
			player[9, 5] = DrowDisplay.SHIP;
			player[9, 0] = DrowDisplay.SHIP;
			player[5, 0] = DrowDisplay.SHIP;


			player[7, 3] = DrowDisplay.SHIP;


			player[3, 5] = DrowDisplay.SHIP;
			player[4, 5] = DrowDisplay.SHIP;
			player[5, 5] = DrowDisplay.SHIP;
			player[6, 5] = DrowDisplay.SHIP;
		}

		public bool DrowFieldTarget(DrowDisplay[,] player, DrowDisplay[,] fieldTarget)
		{
			switch (player[point.y, point.x])
			{
				case DrowDisplay.EMPTY:
					fieldTarget[point.y, point.x] = DrowDisplay.SHOT;
					return true;
				case DrowDisplay.SHIP:
					fieldTarget[point.y, point.x] = DrowDisplay.STRIKE;
					//ОДНОПАЛУБНИК
					//проверка на отсутствие в соседних полях корабля(центр екрана) 
					if ((point.y - 1 > 0) && (point.y + 1 < 9) && (point.x - 1 > 0) && (point.x + 1 < 9))
					{
						if ((player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						    (player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						    (player[point.y, point.x - 1] != DrowDisplay.SHIP) &&
						    (player[point.y, point.x + 1] != DrowDisplay.SHIP))
						{
							for (int i = -1; i < 2; i++)
							{
								for (int j = -1; j < 2; j++)
								{
									fieldTarget[point.y + i, point.x + j] = DrowDisplay.SHOT;
								}
							}
						}

						fieldTarget[point.y, point.x] = DrowDisplay.KILL; // -1 однопалубник
					}
					else

					{

						// проверка на отсутствие корабля и отрисовка углов экрана
						//левый верхний
						if ((point.y == 0) && (point.x == 0) && ((player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						                                         (player[point.y, point.x + 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);

							break;
						} //правый верхний
						if ((point.y == 0) && (point.x == 9) && ((player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						                                         (player[point.y, point.x - 1] != DrowDisplay.SHIP)))   /*FindShipArraund(player))*/
						{

							DrowArraundShip(fieldTarget);
							break;
						}
						//правый нижний
						if ((point.y == 9) && (point.x == 9) && ((player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						                                         (player[point.y, point.x - 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);
							break;
						}
						//левый нижний
						if ((point.y == 9) && (point.x == 0) && ((player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						                                         (player[point.y, point.x + 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);
							break;
						}
						// проверка на отсутствие в соседних полях корабля(верху внизу справа слева экрана)
						if ((point.y - 1 < 0) && ((player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x - 1] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x + 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);

						}
						if ((point.y + 1 > 9) && ((player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x - 1] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x + 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);

						}
						if ((point.x - 1 < 0) && ((player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x + 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);
						}

						if ((point.x + 1 > 9) && ((player[point.y + 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y - 1, point.x] != DrowDisplay.SHIP) &&
						                          (player[point.y, point.x - 1] != DrowDisplay.SHIP)))
						{
							DrowArraundShip(fieldTarget);
						}
						fieldTarget[point.y, point.x] = DrowDisplay.KILL; // -1 однопалубник
					}
					break;
			}
			return false;
		}

		private void DrowArraundShip(DrowDisplay[,] fieldTarget)
		{
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{

					if ((point.y + i >= 0) && (point.y + i <= 9) && (point.x + j >= 0) && (point.x + j <= 9))
					{

						fieldTarget[point.y + i, point.x + j] = DrowDisplay.SHOT;

					}
					else
					{
						continue;
					}
				}
			}
			fieldTarget[point.y, point.x] = DrowDisplay.KILL;
		}

		private bool FindShipArraund(DrowDisplay[,] player)
		{
			for (int i = -1; i < 2; i += 2)
			{
				for (int j = -1; j < 2; j += 2)
				{
					if ((point.y + i >= 0) && (point.y + i <= 9) && (point.x + j >= 0) && (point.x + j <= 9))
					{
						if (player[point.y + i, point.x + j] != DrowDisplay.SHIP)
						{
							return true;
						}
						else
					{
						continue;
					}
				}
			}
		}
			return false;
		}

public bool TargetPosition(Point point)
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
			{
				Console.WriteLine("Enter");
				return true;
			}
			else
			{
				return false;
			}

		}
	}
}