using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class LimitTürevIntegral
    {
        #region Global Tanımlılar
        private char[] isaretler = { '+', '-', '*', '/' };// { '+', '-', '/', '*' };
        private char[] uluIsaretler = { '*' };//{'/', '*' };
        private char[] isaretleriBul(string a)
        {
            char[] araIsaretler = new char[kacTaneVar(a, isaretler)];
            int i = 0;
            foreach (char index in a)
            {
                foreach (char aranan in isaretler)
                {
                    if (index == aranan)
                    {
                        araIsaretler[i] = index;
                        i++;
                    }
                }
            }
            return araIsaretler;
        }
        #endregion
        #region ULU DEGİŞKEN
        public UluDegisken[] uluDegiskenToplamTurev(UluDegisken[] dx, UluDegisken[] dt)//kafasınca denklemin türevini alır
        {
            UluDegisken[] uluSonuc = new UluDegisken[dx.Length + kacTaneVar(uluDegiskenYazdır(dx), '*') * 2 + kacTaneVar(uluDegiskenYazdır(dx), '/') * 4];
            for (int i = 0; i < uluSonuc.Length; i++)
            {
                uluSonuc[i] = new UluDegisken();
            }
            for (int i = 0, a = 0; i < dx.Length; i++)
            {
                //     uluSonuc[i]= new UluDegisken();
                for (int j = 0; j < dt.Length; j++)
                {
                    if (dx[i].isaretim == '+' || dx[i].isaretim == '-')
                    {
                        uluSonuc[a] = uluDegiskenTekliToplamTurev(dx[i], dt[j]);
                    }
                    else if (dx[i].isaretim == '*')
                    {

                        uluSonuc[a - 1] = uluDegiskenTekliToplamTurev(dx[i], dt[j]) * dx[i - 1];
                        uluSonuc[a] = uluDegiskenTekliToplamTurev(dx[i - 1], dt[j]) * dx[i];
                        uluSonuc[++a] = uluDegiskenTekliToplamTurev(dx[i], dt[j]) * dx[i - 1];
                        uluSonuc[++a] = uluDegiskenTekliToplamTurev(dx[i - 1], dt[j]) * dx[i];
                    }
                    else if (dx[i].isaretim == '/')
                    {
                        uluSonuc[a - 1] = uluDegiskenTekliToplamTurev(dx[i], dt[j]) * dx[i - 1];
                        uluSonuc[a] = dx[i] ^ 2;
                        uluSonuc[++a] = uluDegiskenTekliToplamTurev(dx[i - 1], dt[j]) * dx[i];
                        uluSonuc[++a] = dx[i] ^ 2;
                        uluSonuc[++a] = uluDegiskenTekliToplamTurev(dx[i], dt[j]) * dx[i - 1];
                        uluSonuc[++a] = dx[i] ^ 2;
                        uluSonuc[++a] = uluDegiskenTekliToplamTurev(dx[i - 1], dt[j]) * dx[i];
                        uluSonuc[++a] = dx[i] ^ 2;
                    }
                }
                a++;
            }
            return uluSonuc;
        }
        public UluDegisken[] uluSadelestirme(UluDegisken[] a)//buda sadelestirir bug vardı düzelttim bu sapasaglam
        {

            bool bidahamı = false;

            int kacAzaldık = 0;
            for (int i = 0; i < a.Length; i++)
            {

                if (bidahamı) i--; bidahamı = false;
                for (int j = 0; j < a.Length; j++)
                {


                    if (i == j) continue;
                    if ((a[i].tamKısım == a[j].tamKısım) && (a[i].uluKısım == a[j].uluKısım) && a[j].uluKısım != 0)
                    {

                        if (a[i].isaretim == a[j].isaretim)
                        {
                            kacAzaldık++;
                            a[i] += a[j];
                            a[j] = uluSıfırlayıcı();
                            bidahamı = true;
                        }
                        else if ((a[i].isaretim == '+' && a[j].isaretim == '-') || (a[i].isaretim == '-' && a[j].isaretim == '+'))
                        {
                            kacAzaldık++;
                            a[i] -= a[j];
                            a[j] = uluSıfırlayıcı();
                            bidahamı = true;
                        }
                    }

                }
            }

            UluDegisken[] uluSade = new UluDegisken[a.Length - kacAzaldık];
            for (int i = 0, p = 0; i < a.Length; i++)
            {


                if (!uluSıfırmı(a[i]))
                {
                    uluSade[p] = new UluDegisken();
                    uluSade[p] = a[i]; p++;
                }

            }


            if (kacAzaldık == 0)
            {
                return uluSade;
            }
            return uluSadelestirme(uluSade);

        }
        public UluDegisken[] denkHazırlayıcı(UluDegisken[] a)//bunu kullanma ne oldugu bilinmiyor kullancaksan test et
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].sonucmu)
                {
                    a[i] = uluIllalahEttirici(a[i]);
                }
            }
            return a;
        }
        public UluDegisken[] uluDegiskenDiziAyırıcı(string a)// en iyisi bu ,basit token parser
        {
            a = bosluklarıYalarun(a);//boşlukları bi silmek gerekir
            UluDegisken[] ayırdım = new UluDegisken[kacTaneVar(a, isaretler)+1];

            bool ustlumusunSen = false;
            int baslat = 0;
            bool ilkdenklemmi = true;
            bool esitliksonu = false;
            for (int i = 0; i < ayırdım.Length; i++)
            {
                int[] katSayı = new int[a.Length];
                int[] uluSayı = new int[a.Length];
                int ta = katSayı.Length - 1;
                int t = uluSayı.Length - 1;
                bool isaretAldımmı = false;


                ayırdım[i] = new UluDegisken();

                for (int j = baslat; j < a.Length; j++)
                {



                    if (isInteger(a[j]))//sayı ise katlıkısım yada ulukısım
                    {
                        if (ustlumusunSen)
                        {
                            uluSayı[t] = a[j] - 48;
                            t--;
                            ilkdenklemmi = false;
                            isaretAldımmı = true;
                        }
                        else if(a[j]!=' ')//demekki katlıkısımmıs
                        {
                            katSayı[ta] = a[j] - 48;
                            ta--;
                            ilkdenklemmi = false;
                            isaretAldımmı = true;
                        }

                    }
                    else //sayı degilse işaretdir yada üst işaretim unutmadan bide degisken olma ihtimali var bura daha karısık önce burdan yazmak lazım
                    {
                        if (isaretmi(a[j])) //eger temel bir isaretimse alalım
                        {
                            ustlumusunSen = false;
                            if (ilkdenklemmi)
                            {
                                ilkdenklemmi = false;
                                ayırdım[i].isaretim = a[j];
                                baslat = j;
                                isaretAldımmı = true;
                                break;

                            }
                            else
                            {
                                if (isaretAldımmı)
                                {
                                    baslat = j;
                                    break;

                                }
                                else
                                {
                                    ayırdım[i].isaretim = a[j];
                                    baslat = j;
                                    isaretAldımmı = true;

                                }


                            }

                        }
                        else if (a[j] == '^')//benim isaretim degilse üstlü sayi isareti olmasın ?
                        {
                            ustlumusunSen = true;

                        }
                        else if (a[j] == '=')
                        {
                            ustlumusunSen = false;
                            baslat = j + 1;
                            ilkdenklemmi = false;
                            esitliksonu = true;
                            break;
                        }
                        else//oda degilse bu tamkısım olsa gerek
                        {
                            ayırdım[i].tamKısım = a[j];
                        }




                    }
                    if (esitliksonu)
                    {
                        ayırdım[i].sonucmu = true;


                    }

                }

                ayırdım[i].katlıKısım = katSayıTransformer(katSayı, katSayı.Length - 1 - ta);
                ayırdım[i].uluKısım = katSayıTransformer(uluSayı, uluSayı.Length - 1 - t);
                if (ayırdım[i].katlıKısım == 0 && ayırdım[i].tamKısım != ' ') ayırdım[i].katlıKısım = 1;
                if (ayırdım[i].uluKısım == 0) ayırdım[i].uluKısım = 1;
                if (ayırdım[i].sonucmu)
                {
                    // ayırdım[i] = uluIllalahEttirici(ayırdım[i]);
                }
                // Console.WriteLine("katlıkısım = {0}\ttamkısım = {1} \t ulukısım = {2}", ayırdım[i].katlıKısım, ayırdım[i].tamKısım, ayırdım[i].uluKısım);
            }


            return ayırdım;
        }
        public UluDegisken[] DenklemCözümleyici(UluDegisken[] unknowns)//bokumu çözümle ama harbi işe yarıyor
        {
            UluDegisken[] ayırıcı = new UluDegisken[unknowns.Length];
            int indx = 0;
            for (int i = 0; i < ayırıcı.Length; i++)
            {
                ayırıcı[indx] = new UluDegisken();
                if (unknowns[i].tamKısım != ' ')
                {
                    if (unknowns[i].sonucmu) ayırıcı[indx] = uluIllalahEttirici(unknowns[i]); else ayırıcı[indx] = unknowns[i];

                    Console.WriteLine("katlıkısım = {0}\ttamkısım = {1} \t ulukısım = {2}\t sonucmu = {3}", ayırıcı[indx].katlıKısım, ayırıcı[indx].tamKısım, ayırıcı[indx].uluKısım, ayırıcı[indx].sonucmu);
                    indx++;
                    continue;
                }

            }
            for (int i = 0; i < ayırıcı.Length; i++)
            {
                //if (ayırıcı[indx] == null) ayırıcı[indx] = new UluDegisken();

                if (unknowns[i].tamKısım == ' ' && unknowns[i].katlıKısım != 0)
                {
                    if (unknowns[i].sonucmu) ayırıcı[indx] = uluIllalahEttirici(unknowns[i]); else ayırıcı[indx] = unknowns[i];
                    //ayırıcı[indx] = (unknowns[i]);
                    Console.WriteLine("katlıkısım = {0}\ttamkısım = {1} \t ulukısım = {2}\t sonucmu = {3}", ayırıcı[indx].katlıKısım, ayırıcı[indx].tamKısım, ayırıcı[indx].uluKısım, ayırıcı[indx].sonucmu);
                    indx++;
                    continue;
                }

            }

            return ayırıcı;
        }
        public UluDegisken uluSıfırlayıcı()//sıfırlar evelallah
        {
            UluDegisken u = new UluDegisken(0, ' ', 0);
            return u;
        }
        public UluDegisken uluDegiskenTekliToplamTurev(UluDegisken dx, UluDegisken dt)//türev alıyomus buda sanırsam tek tek alıyor
        {
            UluDegisken uluToplam = new UluDegisken();

            uluToplam.isaretim = dx.isaretim;

            if (dx.tamKısım == dt.tamKısım)
            {
                uluToplam.tamKısım = dt.tamKısım;
                uluToplam.katlıKısım = dx.katlıKısım * (dx.uluKısım / dt.uluKısım);
                uluToplam.uluKısım = (dx.uluKısım / dt.uluKısım) - 1;
                if (dx.uluKısım == dt.uluKısım)
                {
                    uluToplam.katlıKısım = dx.katlıKısım;
                    uluToplam.tamKısım = ' ';
                    uluToplam.uluKısım = 1;
                }
            }
            else
            {
                uluToplam.tamKısım = ' ';
                uluToplam.katlıKısım = 0;
                uluToplam.uluKısım = 1;
            }

            return uluToplam;
        }
        public UluDegisken uluIllalahEttirici(UluDegisken ayırdım)//illallah ettirir yani tam tersine döner işaretler ,basit x i yanlız bırakma durumu
        {
            if (ayırdım.isaretim == '+')
            {
                ayırdım.isaretim = '-';
            }
            else if (ayırdım.isaretim == '-')
            {
                ayırdım.isaretim = '+';
            }
            else if (ayırdım.isaretim == '*')
            {
                ayırdım.isaretim = '/';
            }
            else
            {
                ayırdım.isaretim = '*';
            }
            return ayırdım;
        }
        #endregion
        #region String
        public string turev(string dx, string dt)//kısaltma
        {

            return uluDegiskenYazdır(uluSadelestirme(uluDegiskenToplamTurev(uluDegiskenDiziAyırıcı(dx), uluDegiskenDiziAyırıcı(dt))));
        }
        public string uluDegiskenYazdır(UluDegisken[] a)//o kadar çözümledik bari yazsın
        {
            String yolla = "";
            bool isaretbasammı = false;
            for (int i = 0; i < a.Length; i++)
            {
                int yazdırdımmı = 0;
                if (a[i].isaretim != '+') isaretbasammı = true;
                if (isaretbasammı)
                {
                    if (i != 0) yolla += " ";
                    yolla += a[i].isaretim + " ";
                    isaretbasammı = false;

                }


                if (a[i].katlıKısım != 0 && a[i].katlıKısım != 1)
                {
                    yolla += a[i].katlıKısım;
                    yazdırdımmı++;
                }

                if (a[i].tamKısım != ' ')
                {
                    yolla += a[i].tamKısım;
                    yazdırdımmı++;
                }

                if (a[i].uluKısım != 0 && a[i].uluKısım != 1)
                {
                    yolla += "^" + a[i].uluKısım;
                    yazdırdımmı++;
                }
                if (yazdırdımmı == 3 || a[i].tamKısım != ' ') isaretbasammı = true;
                if (i != a.Length - 1)
                    if ((a[i + 1].uluKısım == 1 || a[i + 1].uluKısım == 0) && (a[i + 1].katlıKısım == 1 || a[i + 1].katlıKısım == 0) && a[i + 1].tamKısım == ' ') isaretbasammı = false;

            }

            return yolla;
        }
        public string parantezIci(string str)//eskisinin yenisini yazdık tap taze oldu parantez icini yollamakla kalmaz artık çözüyorda öyle yolluyor,bitmeyen kısım var dikkat et
        {//parantez icini yollar
            string dy = null;
            if (parantezSayısı(str) == 0) return str;
            int[,] index = pIndex(str);
            int[] rank = parantezci(str);
            int indx=minbul(rank);
            if (indx != -1)
            {
                rank[indx] = Int32.MaxValue;
                dy = str.Substring(index[indx, 0] + 1, index[indx, 1] - index[indx, 0] - 1);//cözcez bunu
                Console.WriteLine("dy = "+dy);
                dy = denkçoz(dy);
                Console.WriteLine("en son dy = " + dy);
                //ilerde bunu çözeceksin unutma
                Console.WriteLine("cozulen = " + str);
                str = str.Replace(str.Substring(index[indx, 0], (index[indx, 1] - index[indx, 0]) + 1), dy);
                Console.WriteLine("cozumden sonra = " + str);
                //Console.WriteLine(str + "\n" + dy);
                parantezIci(str);
            }







            return str;
        }
        public string bosluklarıYalarun(string input)//boşlukları siler
        {
            return input.Trim().Replace(" ", string.Empty);
        }
        public string parantCut(string str, int start, int end)//parantez kesici suan kullanılmıyor neden yazdıgımı bilmiyorum
        {

            string newStr = null;

            for (int i = 0; i < str.Length; i++)
            {
                if (!(i >= start && i <= end))
                {
                    newStr += str[i];


                }

            }
            return newStr;
        }
        public string pIndexYaz(int[,] a)//indexleri yazdırıyor kontrol amaçlı yazıldı kesinlikle silinebilir yada çok satırlı dizilerin yazılmasında kullanılabilir
        {
            string output = "";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                output += "index " + i + ": ";
                for (int j = 0; j < a.GetLength(1); j++)
                    output += " " + a[i, j];
                output += "\n";
            }
            return output;
        }
        #endregion
        #region Matematiksel
        private int[] fillArry(int[] fil, int val)//dizi doldurucu
        {
            for (int i = 0; i < fil.Length; i++)
            {
                fil[i] = val;
            }
            return fil;
        }
        private int[] kokparantez(string str)//parantezlerin sıralanmasında yazıldı '('=1,')'=-1 verir
        {
            int parantezSayı = parantezSayısı(str);
            if (parantezSayı == 0) return null;
            int[] dogru = new int[parantezSayı * 2];
            int a = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    dogru[a] = 1;
                    a++;
                }
                if (str[i] == ')')
                {
                    dogru[a] = -1;
                    a++;
                }
            }
            return dogru;
        }
        private int[] rankParantez(int[] index)//parantezlerin kaçıncı dereceden oldugunu anlamaya yarar 0 en düsük derecedir.
        {
            if (index == null) return fillArry(new int[2], 0);
            int[] output = new int[kacTaneVar(index, 1)];
            output = fillArry(output, 0);
            int a = 0;
            for (int i = 0; i < index.Length; i++)
            {
                int myval = -1;
                int indx = findAndSlain(index, 1, i);
                if (indx == -1) break;

                for (int j = indx; j < index.Length; j++)
                {

                    output[a] += index[j];
                    if (myval != -1 && output[a] == 0) break;
                    if (output[a] > myval)
                    {
                        myval = output[a];
                    }
                }
                // if (output[a] == 0) break;
                output[a] = myval - 1;
                a++;
            }
            return output;
        }
        private int[] parantezci(string str)//sadece kısaltma
        {
            return rankParantez(kokparantez(str));
        }
        private int maxbul(int a,int b)//iki sayı arasındaki bilindik büyük olanı bulma durumu
        {
            if (a > b) return a;
            return b;
        }
        private int maxbul(int[] output)//bir dizideki en büyügü bulma
        {
            int max = output[0];
            foreach(int i in output)
            {
                if (i < max) max = i;
            }
            return max;
        }
        private int maxbul(int[,] output)//multi-diamentional dizide en büyügü bulma
        {
            int max = output[0,0];
          for(int i = 0; i < output.GetLength(0); i++)
            {
                for(int j = 0; j < output.GetLength(1); j++)
                {
                    if (max < output[i, j]) max = output[i, j];
                }
            }
            return max;
        }
        private int minbul(int[,] output)//multi-diamentional dizide en kücügü bulma
        {
            int min = output[0, 0];
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    if (min > output[i, j]) min = output[i, j];
                }
            }
            return min;
        }
        private int minbul(int[] output)//normal dizide en kücügü bulma
        {
            if (output == null) return Int32.MaxValue;
            int min = output[0];
            int mindex = 0;
            for (int i = 0; i < output.GetLength(0); i++)
            {

                if (min > output[i]) { min = output[i]; mindex = i; }

            }
            return mindex;
        }
        private int [,] pIndex(string str)//parantezlerin indexlerini indexler [i,0] da açılanlar [i,1] de kapananlar bulunur
        {
            int parSayısı = parantezSayısı(str);
            if (parantezSayısı(str) == 0) return null;
            int [,] p =new int [parSayısı, 2];
            int a = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    p[a, 0] = i;
                    p[a, 1] = -1;
                    a++;
                }

                int b = a;
                if (b >= p.GetLength(0)) b = b - 1;
                if (str[i] == ')')
                {
                    while (p[b, 1] != -1)  b--;
                    p[b, 1] = i;
                }
                }

            return p;
        }
        private int findAndSlain(int[] index, int val, int queue)//verinin dizi üzerinde kaçıncı tekrarına kadar arayıp index döndürecekmiş
        {
            int count = 0; ;
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i] == val)
                {
                    count++;
                    if (count == queue + 1) return i;

                }
            }
            return -1;
        }
        private int findAndSlain(String str, char bul, int index)//verilen string üzerinde kelime arar ...

        {
            int kacıncı = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == bul)
                {
                    if (index == kacıncı) return i;

                    kacıncı++;
                }
            }


            return str.Length;
        }
        private int katSayıTransformer(int[] dizi, int len)//dizi şeklinde alınan ondalık karakterleri sırasına göre birlestirip decimal sayı üretir
        {
            int sayibasi = sayiBasisifirmi(dizi);
            int sayı = 0;

            for (int i = 0; i < dizi.Length; i++)
            {


                sayı += dizi[i] * (int)(Math.Pow(10, (dizi.Length - i - 1)));


            }

            sayı = sayıTersCevir(sayı);

            if (sayıKacBas(sayı) != len - sayibasi)
            {

                sayı *= (int)Math.Pow(10, len - sayibasi - sayıKacBas(sayı));

            }


            return sayı;
        }
        private int sayıKacBas(int sayi)//girilen sayının basamak sayısını döndüren kücük çaplı kod blogu
        {
            int counter = 0;
            while (sayi > 0)
            {
                sayi /= 10;
                counter++;
            }

            return counter;
        }
        private int sayiBasisifirmi(int[] sayi)//cidden neden yazdıgımı bilmiyorum büyük ihtimal bug önlemek amaçlıdır
        {
            int count = 0;
            for (int i = sayi.Length - 1; i > 0; i--)
            {

                if (sayi[i] == 0)
                {
                    count++;

                }
                else
                {

                    return count;
                }


            }
            return count;
        }
        private int sayıTersCevir(int sayı)//sayıyı dizi şeklinde aldıgım icin tersten giriliyor bu yüzdende ters cevirmek gerekiyor
        {
            int tSayı = 0;
            //int sayıbas = sayıKacBas(sayı);
            int a = -1;
            int asayı = sayı;
            while (asayı > 0)
            {
                asayı /= 10;
                a++;
            }
            while (sayı > 0)
            {

                tSayı += sayı % 10 * (int)Math.Pow(10, a);
                sayı /= 10;
                a--;
            }
            // tSayı*=(int)Math.Pow(10,(sayıbas - sayıKacBas(tSayı)));
            return tSayı;
        }
        private int parantezSayısı(string dx)//stringdeki geçerli parantez sayısını döndüren fonksiyon
        {
            int parantezSayısıx = 0;
            int parantezSayısıy = 0;
            for (int i = 0; i < dx.Length; i++)
            {

                if (dx[i] == '(')
                {
                    parantezSayısıx++;
                }
                if (dx[i] == ')')
                {
                    if(parantezSayısıx>parantezSayısıy)
                    parantezSayısıy++;
                }
            }
            if (parantezSayısıx <= parantezSayısıy)
            {
                return parantezSayısıx;
            }
            else
            {
                return parantezSayısıy;
            }
        }
        private int kacTaneVar(int[]arry,int toCheck)//dizi üzerinde kaç adet oldugunu kontrol eder
        {
            int count = 0;
            if (arry == null) return 0;
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i] == toCheck)
                {
                    count++;
                }
            }

            return count;

        }
        private int kacTaneVar(String str, char[] toCheck)// dizi üzerinde char dizisi arar ve kaç tane oldugunu döndürür
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < toCheck.Length; j++)
                {
                    if (str[i] == toCheck[j] && i != 0)
                    {
                        count++;
                    }
                }
                if (str[i] == '=')
                {
                    if (str[i + 1] != '+' && str[i + 1] != '-') count++;

                    /*if(str[i+1]==' ')
                    {
                        if (str[i + 2] != '+' && str[i + 2] != '-')
                            count++;
                    }
                    else {
                    if (str[i + 1] != '+' && str[i + 1] != '-')
                        count++;
                         }*/
                }
            }
            return count;
        }
        private int kacTaneVar(String str, char toCheck)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == toCheck)
                {
                    count++;
                }
            }

            return count;
        }
        private int kacDegiskenVar(UluDegisken[] checkin)//ulu degişken sayısına bakar
        {
            int count = 0;
            char last = ' ';
            float lastust = 0;
            for (int i = 0; i < checkin.Length; i++)
            {


                if (checkin[i].tamKısım != ' ')
                {
                    if (checkin[i].tamKısım != last)
                    {
                        count++;
                        last = checkin[i].tamKısım;
                        lastust = checkin[i].uluKısım;
                    }
                    else if (checkin[i].uluKısım != lastust)
                    {
                        count++;
                        last = checkin[i].tamKısım;
                        lastust = checkin[i].uluKısım;
                    }
                }
            }
            return count;
        }

        private bool uluSıfırmı(UluDegisken a)//ulu degişkenin tam olarak 0 yani null olma koşulunu kontrol eder
        {
            if (a.katlıKısım == 0 && a.tamKısım == ' ' && a.uluKısım == 0) return true;
            return false;
        }
        private bool isaretmi(char str)//tanımlı bir isaret olup olmadıgına bakan fonksiyon birimimiz
        {

            for (int j = 0; j < isaretler.Length; j++)
                if (str == isaretler[j])
                {
                    return true;
                }

            return false;
        }
        private bool isInteger(char chr)//bir sayımı diye kontrol ediyoruz
        {
            if (Char.IsNumber(chr))
            {
                return true;
            }
            return false;
        }
        #endregion
        public string denkçoz(string a)
        {
            return uluDegiskenYazdır(uluSadelestirme(DenklemCözümleyici((uluDegiskenDiziAyırıcı(a)))));
        }

        #region kullanma
        public double exDenkSln(UluDegisken[] unknowns)//bunlar bug dolu ,ya temizle yada hiç kullanma //denklem çözümleyici v0.1 fonksiyonu
        {
            //sadece tek x olursa gecerli oluyor :& daha genel bisey yazmam lazım :/
            unknowns = denkHazırlayıcı(unknowns);
            unknowns = uluSadelestirme(unknowns);
            double value = 0;

            for (int i = 0; i < unknowns.Length; i++)
            {
                if (unknowns[i].tamKısım == ' ')
                {
                    if (unknowns[i].isaretim == '+')
                    {
                        value -= Math.Pow(unknowns[i].katlıKısım, unknowns[i].uluKısım);
                    }
                    else if (unknowns[i].isaretim == '-')
                    {
                        value += Math.Pow(unknowns[i].katlıKısım, unknowns[i].uluKısım);
                    }
                }
            }
            for (int i = 0; i < unknowns.Length; i++)
            {
                if (unknowns[i].tamKısım != ' ')
                {
                    if (unknowns[i].isaretim == '+')
                    {
                        value /= unknowns[i].katlıKısım;
                    }
                    else if (unknowns[i].isaretim == '-')
                    {
                        value /= unknowns[i].katlıKısım * -1;
                    }

                    value = Math.Pow(value, 1 / unknowns[i].uluKısım);
                }
            }
            // value *= -1;
            return value;
        }
        public void DenklemNasılCozulur(UluDegisken[] unknowns)//deneme amaçlı oluşturulmuş karşılaştırıcı bir fonksiyon
        {
            unknowns = uluSadelestirme(unknowns);

            if (kacDegiskenVar(unknowns) == 1)
            {
                Console.WriteLine(exDenkSln(unknowns));
            }
            else
            {
                Console.WriteLine(uluDegiskenYazdır(uluSadelestirme(denkHazırlayıcı(unknowns))));
            }
        }
        #endregion
        #region Kendime Notlar
        /*

        denklem çözümleyici v2.01 beta



        */
        #endregion
    }
}
