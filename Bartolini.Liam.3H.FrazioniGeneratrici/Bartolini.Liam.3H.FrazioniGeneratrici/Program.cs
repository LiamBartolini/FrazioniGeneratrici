using System;

namespace Bartolini.Liam._3H.FrazioniGeneratrici
{
    class Program
    {
        static void Main(string[] args)
        {
            Frazione Fraz = new Frazione();
            string ris;

            //richiedo il risultato e lo converto in double
            Console.WriteLine("Inserisci il risultato della frazione: ");
            string strN = Console.ReadLine();
            //chiedo se è periodica o no
            Console.WriteLine("Il numero è periodico: (si/no) ");
            string strP = Console.ReadLine();

            double risultato = Convert.ToDouble(strN);
            if (strP == "si")   //se è periodica uso il metodo per la frazione periodica
                ris = Fraz.FrazioneDaPeriodici(risultato);
            else                //altrimenti uso il metodo per la frazione da double
                ris = Fraz.CostruisciFrazioneDaDouble(risultato);

            Console.WriteLine("Frazione generatrice è {0}", ris);
        }
    }

    class Frazione
    {
        public string CostruisciFrazioneDaDouble(double n)
        {
            string strN = n.ToString();
            strN.ToCharArray();

            string denominatore = "1";

            //numeri dopo la virgola
            int cont = 0, j = strN.Length;
            while (strN[j - 1] != ',')
            {
                cont++;
                j--;
            }

            string numeratore = strN.Replace(",", "");

            //metto tanti zeri quanti sono i numeri dopo la virgola
            for (int i = 0; i < cont; i++)
                denominatore += "0";

            //Converto numeratore e denominatore ad interi
            int numInt = Convert.ToInt32(numeratore);
            int denInt = Convert.ToInt32(denominatore);

            //richiamo la funziona e gli passo i due valori
            int semplificatore = SemplificaFrazione(numInt, denInt);
            //divido i valori per il semplificatore
            numInt /= semplificatore;
            denInt /= semplificatore;

            return numeratore + "/" + denominatore;
        }

        public string FrazioneDaPeriodici(double n)
        {
            string strN = n.ToString();
            strN.ToCharArray();

            int i = 0;
            string num = "";
            int numeratore;
            string denominatore = "";

            //numeri prima della virgola
            while (strN[i] != ',')
            {
                num += strN[i];
                i++;
            }

            //numeri dopo la virgola (ipotetico periodo)
            int cont = 0, j = strN.Length;
            while (strN[j - 1] != ',')
            {
                cont++;
                j--;
            }

            //tolgo la virgola dal numero e la uso come numero intero
            string nV = strN.Replace(",", "");
            numeratore = Convert.ToInt32(nV) - Convert.ToInt32(num);

            for (i = 0; i < cont; i++)
                denominatore += "9";

            //converto il denominatore ad intero lo uso come parametro nella funzione SemplificaFrazione
            int denInt = Convert.ToInt32(denominatore);
            int semplificatore = SemplificaFrazione(numeratore, denInt);
            numeratore /= semplificatore;
            denInt /= semplificatore;

            return "\n" + Convert.ToString(numeratore) + "/" + Convert.ToString(denInt);
        }

        //calcolo il fattore di semplificazione con l'MCD ricorsivo
        public int SemplificaFrazione(int n, int m)
        {
            if (n == m)      //controllo il caso base
                return n;

            //faccio tutti i calcoli necessari per riportami al caso base
            else if (n > m)
                return SemplificaFrazione(n - m, m);
            else
                return SemplificaFrazione(m, m - n);
        }
    }
}
