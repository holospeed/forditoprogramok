namespace FordítóProgramok_I // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static void Main(string[] args)
        {


            SourceHandler s = new(
                @"/Users/agcocorp/Documents/EKE/Fordítóprogramok/file1.txt",
                @"/Users/agcocorp/Documents/EKE/Fordítóprogramok/file2.txt"
            );

            s.OpenFileToRead();
            s.ReplaceContent();
            s.OpenFiletoWrite();
            Console.ReadKey();





        }
    }
}