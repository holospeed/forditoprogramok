
using System;
using System.Text.RegularExpressions;


namespace FordítóProgramok_I
{

    public class SourceHandler
    {


        //========================
        // REGEX EXPRESSIONS
        //========================
        private readonly string replaceMultiComment = @"/\*(.|\n)*?\*/";
        private readonly string replaceSingleComment = @"//(.*?)\r?\n";
        private readonly string patternNuber = @"([0-9]+)";
        private readonly string patternVariable = @"([a-z-_]+)";
        private readonly string replaceNumber = " CONST[$1] ";
        private readonly string replaceVariable = " VAR[$1] ";


        /*
         * 
         * FROM FILE:
         * 
         * private Dictionary<string, string> CHARACTERS = new Dictionary<string, string>();
         * 
         * 
         * WHILE (SR.PEEK() > -1)
         * {
         *      STRING S = SR.READLINE();
         *      KEYVAL = S.SPLIT(",");
         *      THIS.CHARACTERS.ADD( KEYVAL[0] , KEYVAL[1] );
         * }
         * 
         */


        private Dictionary<string, string> characters = new Dictionary<string, string>
        {
            // WHITE SPACE
            {"\t", " "},
            {"\n", " "},
            {"\v", " "},
            {"\f", " "},
            {"\r", " "},
            {"  ", " "},

            // KEYWORDS
            {"IF",   "10"},
            {"(",    "20"},
            {")",    "30"},
            {"{",    "40"},
            {"}",    "50"},
            {"==",   "61"},
            {"=",    "60"},
            {">=",   "62"},
            {"<=",   "63"},
            {"ELSE", "70"},
            {"WHILE","80"},
            {"FOR",  "90"},
            {"TRY",  "100"},
            {"CATCH","110"},
        };

        private List<string> symbolTable;
        private int symbolTableIndex = 0;


        private string path1, path2 = "";

        public string Path1
        {
            get
            {
                return this.path1;
            }
            set
            {
                this.path1 = value;
            }
        }
        public string Path2
        {
            get
            {
                return this.path2;
            }
            set
            {
                this.path2 = value;
            }
        }

        private string content = "";
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public bool isReady; // instance is ready for use

        // @"PATH"
        public SourceHandler(string path1, string path2)
        {
            this.path1 = path1;
            this.path2 = path2;
            this.symbolTable = new List<string>();
        }


        string ChangeVarAndConst(string input)
        {
            this.symbolTable.Add(input);
            this.symbolTableIndex += 1;

             string result = "00" + this.symbolTableIndex.ToString();
             return result.Substring(result.Length - 3);

        }

        public void ReplaceContent()
        {
            // REMOVE multi line of comment(s)
            this.content = Regex.Replace(this.content, this.replaceMultiComment , " ");
            // REMOVE simple block of comment(s)
            this.content = Regex.Replace(this.content, this.replaceSingleComment, " ");

            // CHANGE CONSTANT AND SAVE
            this.content = Regex.Replace(this.content, this.patternNuber,    this.ChangeVarAndConst("$1"));
            // CHANGE VARIABLE AND SAVE
            this.content = Regex.Replace(this.content, this.patternVariable, this.ChangeVarAndConst("$1"));


            // REPLACE NUMBERS      6 => CONST[6]
            // this.content = Regex.Replace(this.content, this.patternNuber, this.replaceNumber);
            // REPLACE VARIABLES    a => VAR[a]
            // this.content = Regex.Replace(this.content, this.patternVariable, this.replaceVariable);

            // REMOVE double white space
            foreach (KeyValuePair <string, string> item in this.characters)
            {
                this.ReplaceText(item.Key, item.Value);
            }

        }

        public void ReplaceText(string from, string to)
        {
            while (this.content.Contains(from))
            {
                this.content = this.content.Replace(from, to) ;
            }
        }

        public void replaceContent(string sth)
        {
            this.content = sth;
        }

        public void OpenFileToRead()
        {
            try
            {

              StreamReader SR = new(File.OpenRead(this.path1));
              this.content = SR.ReadToEnd();
              SR.Close();

            } catch (IOException IOE)
            {
                Console.WriteLine(IOE.Message);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            
        }
        public void OpenFiletoWrite()
        {
            try
            {
                StreamWriter SW = new(File.Open(this.path2, FileMode.Create));
                SW.WriteLine(this.content);
                SW.Flush();
                SW.Close();

            }
            catch (IOException IOE)
            {
                Console.WriteLine(IOE.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }



    }

}

