
//
// G = ({S,E,E',T,T',F},
//      {+,*,(,),#}, P,S
//      )
//
// P =  S->E#
//      E->TE'
//      E'-> + TE'|e
//      T->FT'
//      T'-> *FT'|e
//      F ->(E)|i
//
//
// +12, -3, 2 ,0 ,000, 001
// D = {0,2, ..., 9}
// (+|-|e)D+
// delta(q0, +) -> q1,
// delta(q0, -) -> q1,
// delta(q0, N) -> q2,
// delta(q1, N) -> q2,
// delta(q1, N) -> q2,
//
//



// SEMANTIC ANALISER
namespace RegexAutamaton
{

    class Automata
    {
        string input;
        int i;
        string state;
        Dictionary<string, string> Dic  = new Dictionary<string, string>();

        public Automata(string input)
        {
            this.input = input;
            this.i = 0;
            this.state = "q0";

            this.Dic.Add("q0-", "q1");
            this.Dic.Add("q0+", "q1");
            this.Dic.Add("q0D", "q2");
            this.Dic.Add("q1D", "q2");
            this.Dic.Add("q2D", "q2");
              
        }

        public char Convert(char c)
        {
            return Char.IsDigit(c) ? 'D' : c;
        }

        public string Delta(string st, char elem)
        {
            string elemek = st + this.Convert(elem);

            return this.Dic[elemek] ?? "error";
          
        }

        public void Main()
        {
            while( this.i < this.input.Length && this.state != "error" )
            {
                this.state = this.Delta(state, this.input[this.i]);
                this.i++;
            }

            if (state == "error")
            {
                Console.WriteLine("{0} kifejezés helytelen, " +
                                  "a hiba pozíciója {1}, a hibás karakter {2}",
                                  this.input,
                                  this.i,
                                  this.input[i] );
            }
            else
            {
                Console.WriteLine("Az input kifejezés helyes: {0}", this.input);
            }
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
        } 
    }
}