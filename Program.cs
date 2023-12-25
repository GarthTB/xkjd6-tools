using System.Text;

namespace 冗余检查器
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //检查文件
            if (!File.Exists(@".\cizu.yaml"))
            {
                Console.WriteLine("请先将键道6的词库文件重命名为cizu.yaml（不含文件头，已按a-z排列），并放在当前程序相同目录下。");
                return;
            }

            //将词库载入为list
            List<string> Malist = new();
            StreamReader MaStream = new(@".\cizu.yaml", Encoding.Default);
            string? Line;
            while ((Line = MaStream.ReadLine()) != null)
            {
                Malist.Add(Line);
            }
            MaStream.Dispose();

            //检查冗余
            List<string> Errlist = new();
            int n = 0;
            while (n < Malist.Count)
            {
                if (Malist.Contains(Malist[n][..^1]))
                {
                    Errlist.Add(Malist[n][..^1] + "\t" + Malist[n].Split('\t')[1]);
                    Console.WriteLine(Malist[n][..^1] + "\t" + Malist[n].Split('\t')[1]);
                }
                else if (Malist.Contains(Malist[n][..^2]))
                {
                    Errlist.Add(Malist[n][..^2] + "\t" + Malist[n].Split('\t')[1]);
                    Console.WriteLine(Malist[n][..^2] + "\t" + Malist[n].Split('\t')[1]);
                }
                else if (Malist.Contains(Malist[n][..^3]))
                {
                    Errlist.Add(Malist[n][..^3] + "\t" + Malist[n].Split('\t')[1]);
                    Console.WriteLine(Malist[n][..^3] + "\t" + Malist[n].Split('\t')[1]);
                }
                n++;
            }

            //保存文件
            StreamWriter RYMaStream = new(@".\冗余码.yaml");
            for (n = 0; n < Errlist.Count; n++)
            {
                RYMaStream.WriteLine(Errlist[n]);
            }
            RYMaStream.Dispose();
            Console.WriteLine("冗余码文件已生成。短码在左，长码在右。");
            Console.ReadKey();
        }
    }
}
