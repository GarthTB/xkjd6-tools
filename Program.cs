using System.Text;

namespace 筛词器
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //检查文件
            if (!File.Exists(@".\shit.yaml") || !File.Exists(@".\cizu.yaml"))
            {
                Console.WriteLine("请先将词库文件重命名为cizu.yaml（不含文件头），想筛除的字或词重命名为shit.yaml（每行一个，不含编码），并放在当前程序相同目录下。");
                Console.ReadKey();
                return;
            }

            //将shit载入为list
            List<string> Shitlist = new();
            StreamReader ShitStream = new(@".\shit.yaml", Encoding.Default);
            string? Shit;
            while ((Shit = ShitStream.ReadLine()) != null)
            {
                Shitlist.Add(Shit);
            }
            ShitStream.Dispose();

            //将cizu载入为list
            List<string> Cilist = new();
            StreamReader CiStream = new(@".\cizu.yaml", Encoding.Default);
            string? Ci;
            while ((Ci = CiStream.ReadLine()) != null)
            {
                Cilist.Add(Ci);
            }
            CiStream.Dispose();

            //选择筛除模式
            Console.WriteLine("要筛除所有包含shit的cizu，请按1；只需筛除等于shit的cizu，请按2。");
            char input;
            while (true)
            {
                input = Console.ReadKey().KeyChar;
                if (input == '1' || input == '2')
                {
                    break;
                }
                Console.WriteLine("按错了，再按一遍。");
            }

            List<string> Discardlist = new();//用来装被剔除的词

            //筛除包含shit的项
            if (input == '1')
            {
                int n = 0;
                while (n < Cilist.Count)
                {
                checkagain1:
                    foreach (string shit in Shitlist)
                    {
                        if (Cilist[n].Split('\t')[0].Contains(shit))
                        {
                            Discardlist.Add(Cilist[n]);
                            Cilist.RemoveAt(n);
                            Console.WriteLine("+1");
                            goto checkagain1;
                        }
                    }
                    n++;
                }
            }

            //筛除等于shit的项
            else
            {
                int n = 0;
                while (n < Cilist.Count)
                {
                checkagain2:
                    foreach (string shit in Shitlist)
                    {
                        if (Cilist[n].Split('\t')[0] == shit)
                        {
                            Discardlist.Add(Cilist[n]);
                            Cilist.RemoveAt(n);
                            Console.WriteLine("+1");
                            goto checkagain2;
                        }
                    }
                    n++;
                }
            }

            //保存文件
            StreamWriter NoShitStream = new(@".\cizu (clean).yaml");
            for (int n = 0; n < Cilist.Count; n++)
            {
                NoShitStream.WriteLine(Cilist[n]);
            }
            NoShitStream.Dispose();

            StreamWriter DiscardStream = new(@".\cizu (full of shit).yaml");
            for (int n = 0; n < Discardlist.Count; n++)
            {
                DiscardStream.WriteLine(Discardlist[n]);
            }
            DiscardStream.Dispose();

            Console.WriteLine("已完成筛除。");
            Console.ReadKey();
        }
    }
}
