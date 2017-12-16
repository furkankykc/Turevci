using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication9;
using Matematik;

namespace ConsoleApplication9
{

    class Program
    {



        static void Main(string[] args)
        {
            string a, b, sonuc;

            LimitTürevIntegral lmt = new LimitTürevIntegral();
            //   int[] sayı = { 1, 2, 3 ,0,0} ;
            //  Console.Write("türev icin 1 e denklem çözümü icin 2 ye basınız");
            //   bool hangisi = false;
            ///son=Console.Read();
            /*  if (son == 49)
              {
            //       hangisi = false;                  hangisi = true;
              }*/


            /*a = "()()";//"(((())(()()())))";
            b = "((büşra)(parantezler)(aradı(mantıklarını geri istiyorlar))())";
            Console.WriteLine("geçerli parantei z sayısı :"+lmt.parantezSayısı(b) + "\n" + "parantez içleri:"+lmt.parantezIci(b) );
            Console.WriteLine(b);
            Console.WriteLine(lmt.pIndexYaz(lmt.pIndex(b)));
            matriSex.diziYazdır(lmt.parantezci(b));
            */
            //parantez içi alıcı yetersiz yenisinyaz. yazdım reiyz


            Console.ReadKey();


            while (true)//çalıştırmak için true
            {
                if (false)//denklem çözmek istersen false ; türev alcaksan true yap
                {
                    Console.Write("Türevini almak istediginiz denklemi giriniz : ");
                    a = Console.ReadLine();
                    Console.Write("Hangi bilinmeyene göre türev alınacagını giriniz : ");
                    b = Console.ReadLine();
                    sonuc = lmt.turev(a, b);
                    Console.WriteLine(sonuc);
                    Console.ReadLine();
                    if (b == "exit" || a == "exit") break;
                }
                else
                {

                    Console.Write("Çözümlemek istediginiz denklemi giriniz : ");
                    a = Console.ReadLine();
                    Console.WriteLine(lmt.uluDegiskenYazdır(lmt.uluSadelestirme(lmt.denkHazırlayıcı(lmt.uluDegiskenDiziAyırıcı(lmt.parantezIci(a)))))); //   lmt.uluSadelestirme
                    Console.WriteLine(lmt.uluDegiskenYazdır(lmt.uluDegiskenDiziAyırıcı(a)));
                    Console.WriteLine("x={0}", lmt.exDenkSln((lmt.uluDegiskenDiziAyırıcı(a))));
                    Console.ReadLine();
                }

            }
        }

        public void M()
        {
            throw new NotImplementedException();
        }
    }

}
