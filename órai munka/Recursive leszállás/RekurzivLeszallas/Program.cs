using System.Text.RegularExpressions;

internal class Program
{

    class Automata
    {
        string input;
        int i = 0;

        public string Simple(string st)
        {
            return Regex.Replace(st, "([0-9]+)+", "i");
        }

        public Automata(string input)
        {
            Console.WriteLine("Eredeti input: {0}", input);
            this.input = $"{Simple(input)}#";
            Console.WriteLine("Az egyszerűsített input: {0}", this.input);
        }


        void accept(char symbol)
        {
            if (input[i] != symbol)
            {
                Console.WriteLine("HIBÁS KIFEJEZÉS {0}. Helytelen karakter: {1}", input, input[i]);
            }
            i++;

        }

        void T()
        {
            F();
            Tv();
        }

        void Ev()
        {
            if (input[i] == '+')
            {
                accept('+');
                T();
                Ev();
            }
        }

        void Tv()
        {
            if (input[i] == '*')
            {
                accept('*');
                F();
                Tv();
            }
        }

        void F()
        {
            if (input[i] == '(')
            {
                accept('(');
                E();
                accept(')');
            }
            else
            {
                accept('i');
            }
        }
 
        void E()
        {
            T();
            Ev();
        }

        public void S()
        {
            E();
            accept('#');
            Console.WriteLine(" az elemzés lefutott"); ;
        }      
    } 

    static void Main(string[] args)
    {
        Automata a = new("(31+d)*t");
        a.S();
        Console.ReadLine();
    }
}