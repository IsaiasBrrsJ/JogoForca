using System;

namespace JogoForca
{
    class Program
    {
        static void Main(string[] args)
        {   
          
            Menu();
            Console.ReadKey();
        }
        public static void  Menu()
        {
            int opcao;
            bool opcDigitada;
            do
            {
                Console.Title = "JOGO DA FORCA";
                Console.Clear();
                Console.WriteLine("--------------------------");
                Console.WriteLine("  Jogo de Forca   ");
                Console.WriteLine("--------------------------");
                Console.WriteLine("[ 1 ] Jogar\n[ 2 ] Sair\n");
                Console.Write("Opção: ");

                opcDigitada = int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        var game = new Game();
                        game.Jogar();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Menu();
                        break;
                }
            } while (!opcDigitada);
        }
    }
}
