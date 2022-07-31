using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using JogoForca;

namespace JogoForca
{
    class Game
    {
        private string[] boneco = { null, null, null, null, null, null }; //Protipo do boneco

        private string[] erros = { "( )", "|", "/", @"\", "/", @"\" }; //partes do boneco
        private int numAleatorio  =  new Random().Next(1, 4);

        #region TiposDeJogos
        private string[] Cidades =
        {

            "São Paulo",
            "Rio De Janeiro",
            "Porto Velho",
        };

        private string[] Times =
        {   "Corinthians",
            "Flamengo",
            "Palmeiras"
        };

        private string[] Frutas =
        {
            "Maça",
            "Morango",
            "Abacate"
        };
        #endregion
        public void Jogar()
        {   
         

            var frase = FraseAleatoria();
            var jogoGerado = TipoDeJogo();
            var frases = new string[frase.Length];
            var split = new char[frases.Length];
            var letra = ' ';
            int contErro = -1;

            for(int i = 0; i < frases.Length; i++)
            {
                if (frase[i] == ' '){ 

                    frases[i] = " ";// caso a frase tenha espaço 
                }
                else{

                    frases[i] = "_ "; // define os traços onde depois vão ser substituidos por letras que compoe uma frase
                }
                
                split[i] =Convert.ToChar(frase[i].ToString().ToLower()); // define todas as letras pra caixa baixa
            }
            
            while (true)
            {
                bool tentouDenovo = true; //variavel definida aqui, pra sempre que voltar aqui ela ser redefinida pra true
                Forca(ref jogoGerado, ref frases, ref contErro);

                Console.Write("\n\nDigite uma letra: ");
                 letra = char.Parse(Console.ReadLine());

                for (int i = 0; i < split.Length; i++) {

                    if (split[i].ToString().Contains(letra)){ // verifica se a letra digitada contem na frase

                        if (frases[i].Contains(letra)) { break; }// se já houver a letra e digitar ela novamente, volta perguntar uma letra, e não conta como erro ou acerto

                        frases[i] = letra.ToString(); // se contem a letra ele coloca ela no lugar dela
                        tentouDenovo = false; // define como false, pra contar erro apenas se não existe em nenhuma posicao a letra digitada
                    }
                    else if (i == split.Length - 1 && tentouDenovo == true)
                    { ++contErro; } // se não existe a letra digita em nenhuma posicao do array, conta um erro
                }

                if (VerificaSeGanhou(ref frases)){

                    ResetaBonecoEFrase(ref frases);
                    Console.WriteLine("\n\nGanhou");
                    Console.ReadKey();
                    JogoForca.Program.Menu();
                }
            }

        }
        private bool VerificaSeGanhou(ref string[] frase)
        {
            for (int i = 0; i < frase.Length; i++){

                if (frase[i].Contains("_ ") && i != frase.Length -1){
                    return false;
                }
            }

            return true;
        }

        private string TipoDeJogo()
        {
            if(numAleatorio is 1){
                return "Cidade";
            }
            else if(numAleatorio is 2){
                return "Time de futebol";
            }
            else{
                return "Fruta";
            }
        }
        private string FraseAleatoria()
        {       
            var fraseAleatoria = new Random().Next(0, 3);
            if (numAleatorio is 1){
                return Cidades[fraseAleatoria];
            }
            else if (numAleatorio is 2){
                return Times[fraseAleatoria];
            }
            else{
                return Frutas[fraseAleatoria];
            }
        }
        public void ResetaBonecoEFrase(ref string[] frases)
        {
            for (int i = 0; i < boneco.Length; i++) //resetar o boneco para o valor original
            {
                boneco.SetValue(null, i);
            }
            for (int i = 0; i < frases.Length; i++) { frases.SetValue("_ ", i); } // reseta o array  frase para o valor original, removendo as letras que foram inseridas durante o processo de tentativas
        }
        private void Forca(ref string jogoGerado, ref string[] frases, ref int contErro)
        {
            const int limiteMaxErro = 5;

            Console.Clear();
            Console.WriteLine("===========================");
            Console.WriteLine($"  Dica: {jogoGerado}");
            Console.WriteLine("===========================\n\n");

            if (contErro == limiteMaxErro)// se o contador de erros for igual o limite do boneco então o jogador perde
            {
                Console.WriteLine("Game Over");
                //zera o array que define o boneco
                ResetaBonecoEFrase(ref  frases);
                Thread.Sleep(1000);
                JogoForca.Program.Menu();
            }
            else if(contErro >= 0 )
              boneco[contErro] = erros[contErro]; // cado erro é inserido no boneco uma parte do corpo

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" _____________");
            Console.WriteLine("  |          |");
            Console.WriteLine($"  |         {boneco[0]}");
            Console.WriteLine($"  |         {boneco[2]}{boneco[1]}{boneco[3]}");
            Console.WriteLine($"  |          {boneco[1]}");
            Console.WriteLine($"  |         {boneco[4]}{boneco[1]}{boneco[5]}");
            Console.WriteLine($"__|__\n\n\n");
            Console.ResetColor();

            for (int i = 0; i < frases.Length; i++){ 
                Console.Write(frases[i]);  // cada vez que passa aqui ele atuliza se houve acertos exibe a letra no lugar que contem, ou mantem os traços
            }
            
        }
    }
}
