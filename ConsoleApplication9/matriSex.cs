using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematik
{
    public class matriSex
    {
        public static void diziYazdır(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
        }
        public static int[][] matrisçarpımı(int[][] a, int[][] b)
        {
            if (a.Length != b[0].Length) return null;

            int[][] çarpım = new int[a.Length][];
              for(int i = 0; i<çarpım.Length;i++){
                çarpım[i] = new int[b[0].Length];
               for(int j = 0; j<çarpım[0].Length;j++){
                int c = 0;
               for(int k = 0; k<çarpım[0].Length;k++){
                c+=a[j][k]*b[k][i % b[0].Length];
               }
                çarpım[i][j]=c;
               }
              }
              return çarpım;
             }
        public static void matrisYazdır(int[][] m)
        {
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < m[0].Length; j++)
                {
                    Console.Write(m[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static int[][] matrisTranspoz(int[][] a)
        {
            int[][] b =new int[a.Length][];
            for(int i = 0; i<a.Length;i++)
            {
                b[i] = new int [a[0].Length];
                for (int j = 0; j<a[0].Length;j++)
                {
                    b[i][j]=a[j][i];
                }
             }
         return b;
        }
        public static int[][] diziBirleştir(int[] a, int[] b)
        {
            if (a.Length != b.Length) return null;
            int[][] c =new int[a.Length][];
                    for (int i = 0; i < c.Length; i++)
                    {
                        c[i] = new int[b.Length];
                        for (int j = 0; j < 2; j++)
                     if (j % 2 == 0) c[i][j] = a[i]; else c[i][j] = b[i];
                    }
        
            return c;
 
        }
        public static int[][] matrisKofaktörMatris(int[][] a)
        {
            int[][] b =new int[a.Length][];
         for(int i = 0; i<a.Length;i++){
                        b[i] = new int[a[0].Length];
          for(int j = 0; j<a[0].Length;j++){
           b[i][j]= (int) (Math.Pow(-1, i+j)* matrisDeterminant(matrisMinor(a, i, j)));
          }
         }
 
         return b;
        }
        public static int matrisDeterminant(int[][] a)
        {

            int b = 0;

            if (a.Length == 1)
            {
                return a[0][0];
            }
            else if (a.Length == 2)
            {
                return a[0][0] * a[1][1] - a[0][1] * a[1][0];
            }
            else {
                for (int i = 0; i < a.Length; i++)
                {
                    b += (a[0][i]) * (int)Math.Pow(-1, i) * matrisDeterminant(matrisMinor(a, 0, i));
                }
            }
            return b;
        }
        public static int[][] matrisEkMatris(int[][] a)
        {
                    int[][] b =new int[a.Length][];
                    for (int i = 0; i < b.Length;i++) b[i] = new int[a[0].Length];
                     b=matrisTranspoz(matrisKofaktörMatris(a));
 
         return b;
        }
        public static int[] matrisSatırOrt(int[][] a)
        {
            int[] sOrt = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                int c = 0;
                for (int j = 0; j < a[0].Length; j++)
                {
                    c += a[i][j];
                }
                sOrt[i] = c / a.Length;
            }


            return sOrt;
        }
        public static int[] matrisStunOrt(int[][] a)
        {
            int[] sOrt = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                int c = 0;
                for (int j = 0; j < a[0].Length; j++)
                {
                    c += a[j][i];
                }
                sOrt[i] = c / a.Length;
            }


            return sOrt;
        }
        public static int[][] matrisBüyüt(int[][] a, int b)
        {
            int[][] c =new int[a.Length * b][];
                    for (int i = 0; i < c.Length; i++)
                    {
                        c[i] = new int[a[0].Length * b];
                        for (int j = 0; j < c[0].Length; j++)
                        {
                            c[i][j] = a[i / b][j / b];
                        }
                    }
            return c;
        }
        public static int[] matrisKösegen(int[][] a)
        {
            int[] b = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    if (i == j) b[i] = a[i][j];
                }
            }

            return b;

        }
        public static int[][] matrisMinor(int[][] a, int b, int c)
        {
            int[][] d =new int[a.Length - 1][];
            int x = 0;
            for(int i = 0; i<a.Length;i++)
            {
                        d[i]=new int[a[0].Length - 1];
                if (i==b) continue;
                int y = 0;
                for(int j = 0; j<a[0].Length;j++)
                {
                    if(j==c) continue;
                    d[x][y]=a[i][j];
                    y++;
                }
                x++;
             }
          return d;
        }
        public static int[][] matrisTersMatris(int[][] a)
        {
            return matrisSayıÇarpımı(matrisEkMatris(a), 1 / matrisDeterminant(a));
        }
        public static int[][] matrisSayıÇarpımı(int[][] a, int b)
        {
            int[][] c =new int[a.Length][];
                    for (int i = 0; i < a.Length; i++)
                    {
                        c[i] = new int[a[0].Length];
                        for (int j = 0; j < a[0].Length; j++)
                            c[i][j] = b * a[i][j];
                    }
            return c;
        }
}
}
