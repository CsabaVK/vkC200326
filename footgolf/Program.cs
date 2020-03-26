using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace footgolf
{
    class Program
    {
        static List<versenyzo> verseny;
        struct versenyzo
        {
            public string nev;
            public string kategoria;
            public string egyesulet;
            public List<int> pontok;
            public int osszpont;
        }
        static void Main(string[] args)
        {
            Beolvasas();
            F03();
            F04();
            F06();
            F07();
            //F08();
            Console.ReadKey();
        }

        private static void F08()
        {
            var list = verseny.GroupBy(x => x.kategoria).Select(x => x.ToList()).ToList();

        }

        private static void F07()
        {
            StreamWriter sw = new StreamWriter("osszpontFF.txt", false);
            List<versenyzo> ferfiak = verseny.FindAll(x => x.kategoria.Contains("Felnott ferfi"));
            foreach (versenyzo f in ferfiak)
            {
                sw.WriteLine(f.nev + ";" + f.osszpont);
            }
            sw.Close();
        }

        private static void F06()
        {
            versenyzo legjobbNoi = verseny.FindAll(x => x.kategoria.Contains("Noi")).OrderByDescending(x => x.osszpont).First();
            Console.WriteLine($"6. feladat: A bajok női versenyzpő \n Név: {legjobbNoi.nev} \n Egyesület: {legjobbNoi.egyesulet} \n Összpont: {legjobbNoi.osszpont}");
        }

        private static void F04()
        {
            int no = verseny.FindAll(x => x.kategoria.Contains("Noi")).Count;
            double noiarany = (Convert.ToDouble(no) / verseny.Count) * 100;
            Console.WriteLine($"4. feladat: A női versenyzők aránya: {noiarany.ToString("#.##")}%");
        }

        private static void F03()
        {
            Console.WriteLine($"3. feladat: Versenyzők száma: {verseny.Count}");
        }

        private static void Beolvasas()
        {
            verseny = new List<versenyzo>();
            StreamReader sr = new StreamReader(@"fob2016.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(';');
                versenyzo vz = new versenyzo();
                vz.nev = line[0];
                vz.kategoria = line[1];
                vz.egyesulet = line[2];
                vz.pontok = new List<int>();
                for (int i = 3; i < line.Length; i++)
                {
                    vz.pontok.Add(Convert.ToInt32(line[i]));
                }
                vz.osszpont = calculateOsszpont(vz.pontok);
                verseny.Add(vz);
            }
            sr.Close();
        }

        // 5. feladat
        private static int calculateOsszpont(List<int> pontok)
        {
            int ossz = 0;
            pontok.Sort();
            for (int j = 2; j < pontok.Count; j++)
            {
                ossz += pontok[j];
            }
            if (pontok[0] != 0)
            {
                ossz += 10;
            }
            if (pontok[1] != 0)
            {
                ossz += 10;
            }
            return ossz;
        }
    }
}
