using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
	/*enum StateGameEnum1
	{
		INIZIALIZATE = 0,
		DRAW,
		PROCESING,
		EXIT
	}*/
	class Program
	{
	
		
		static void Main(string[] args)
		{
			/*	ConsoleKeyInfo key = Console.ReadKey();
				Console.WriteLine(key);*/
			//var isUp = Console.ReadKey().Key == ConsoleKey.UpArrow;
			/*isUp = Console.ReadKey().Key == ConsoleKey.DownArrow;
			isUp = Console.ReadKey().Key == ConsoleKey.LeftArrow;
			isUp = Console.ReadKey().Key == ConsoleKey.RightArrow;*/
			//isUp = Console.ReadKey().Key == ConsoleKey.Enter;
			//isUp = Console.ReadKey().Key == ConsoleKey.Clear;

			/*while (isUp)
			{
				Console.WriteLine("ssss");
				isUp = false;

			}*/




			int x=0;
			int y = 0;
			int z = y;
			Screan screan=new Screan();
			//screan.StateGame();

			//Console.WriteLine(screan.DrowSymbol[(int)DrowDisplay.KILL]);
			/*int a = 10;
			screan.Sqr(ref a);*/
			screan.Position(x, y, out z);
			Console.WriteLine(x, y, z);










		}
	}
}
