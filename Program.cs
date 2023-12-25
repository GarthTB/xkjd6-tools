using System.Text;

namespace 空码检查器
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

            //将词库载入为每行一码的list
            List<string> Malist = new();
            StreamReader MaStream = new(@".\cizu.yaml", Encoding.Default);
            string? Line;
            while ((Line = MaStream.ReadLine()) != null)
            {
                Malist.Add(Line.Split('\t')[1]);
            }
            MaStream.Dispose();

            //检查空码
            List<string> Errlist = new();
            string avoiddupli = "";
            foreach (string temp in Malist)
            {
                if (temp.Length == 4 && !Malist.Contains(temp[..^1]) && temp[..^1] != avoiddupli && new[] { "a", "i", "o", "u", "v" }.Contains(temp[^1..]))
                {
                    Errlist.Add(temp[..^1] + "\t" + temp);
                    Console.WriteLine(temp[..^1] + "\t" + temp);
                    avoiddupli = temp[..^1];
                }
                else if (temp.Length > 4 && !Malist.Contains(temp[..^1]) && temp[..^1] != avoiddupli)
                {
                    Errlist.Add(temp[..^1] + "\t" + temp);
                    Console.WriteLine(temp[..^1] + "\t" + temp);
                    avoiddupli = temp[..^1];
                }
            }

            //保存文件
            StreamWriter KongMaStream = new(@".\空码.yaml");
            for (int n = 0; n < Errlist.Count; n++)
            {
                KongMaStream.WriteLine(Errlist[n]);
            }
            KongMaStream.Dispose();
            Console.WriteLine("空码文件已生成。空码在左，现有码在右，请将现有码改短。若未按a-z排列，可能会有多个现有码抢占一个空码，造成重码。");
            Console.ReadKey();
        }
    }
}
