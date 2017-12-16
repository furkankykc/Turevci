using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication9
{
   public  class UluDegisken
    {
        public float uluKısım, katlıKısım;
        public char tamKısım;
        public char isaretim='+';
        public bool sonucmu = false;
        public UluDegisken(float katlıKısım, char tamKısım, float uluKısım)
        {
            this.tamKısım = tamKısım;
            this.uluKısım = uluKısım;
            this.katlıKısım = katlıKısım;
        }
        public UluDegisken()
        {
            this.tamKısım = ' ';
            this.uluKısım = 1;
            this.katlıKısım = 0;
        }
        public UluDegisken(char tamKısım)
        {
            this.tamKısım = tamKısım;
            this.uluKısım = 1;
            this.katlıKısım = 0;
        }
        public UluDegisken(float katlıKısım, char tamKısım)
        {
            this.tamKısım = tamKısım;
            this.uluKısım = 1;
            this.katlıKısım = katlıKısım;
        }
        public UluDegisken(char tamKısım, float uluKısım)
        {
            this.tamKısım = tamKısım;
            this.uluKısım = uluKısım;
            this.katlıKısım = 0;
        }
        public UluDegisken(int p1,char p2,int p3)
    {

this.katlıKısım = p1;
this. tamKısım= p2;
this.uluKısım= p3;
    }
        private static UluDegisken uluSıfırlayıcı()
{
    UluDegisken u = new UluDegisken(0, ' ', 0);
    return u;
}
        public static UluDegisken  operator+(UluDegisken a,UluDegisken b)
        {
           UluDegisken uluToplam= new UluDegisken();
           if (a.uluKısım == 0) a= uluSıfırlayıcı();
           if (b.uluKısım == 0) b = uluSıfırlayıcı();
           if (a.isaretim == b.isaretim && a.uluKısım!=0)
           {
               uluToplam.isaretim = a.isaretim;
               if ((a.tamKısım == b.tamKısım) && (a.uluKısım == b.uluKısım))
               {
                   uluToplam.tamKısım = a.tamKısım;
                   uluToplam.katlıKısım = a.katlıKısım + b.katlıKısım;
                   uluToplam.uluKısım = a.uluKısım;
               }
               else
               {
                   uluToplam.tamKısım = a.tamKısım;
                   uluToplam.katlıKısım = a.katlıKısım;
                   uluToplam.uluKısım = a.uluKısım;
                   uluToplam.tamKısım = b.tamKısım;
                   uluToplam.katlıKısım = b.katlıKısım;
                   uluToplam.uluKısım = b.uluKısım;
               }
           }

           else
           {
               uluToplam.isaretim = a.isaretim;
               if ((a.tamKısım == b.tamKısım) && (a.uluKısım == b.uluKısım) )
               {
                   uluToplam.tamKısım = a.tamKısım;
                   if(a.katlıKısım - b.katlıKısım>0)
                   {
                   uluToplam.katlıKısım = a.katlıKısım - b.katlıKısım;

                   }else if(a.katlıKısım - b.katlıKısım== 0){
                       uluToplam.isaretim = ' ';
                       uluToplam.tamKısım= ' ';
                       uluToplam.uluKısım=1;

                   }

                   else{
                       uluToplam.katlıKısım = b.katlıKısım - a.katlıKısım;
                   }
                   uluToplam.uluKısım = a.uluKısım;
               }
               else
               {
                   uluToplam.tamKısım = a.tamKısım;
                   uluToplam.katlıKısım = a.katlıKısım;
                   uluToplam.uluKısım = a.uluKısım;
                   uluToplam.tamKısım = b.tamKısım;
                   uluToplam.katlıKısım = b.katlıKısım;
                   uluToplam.uluKısım = b.uluKısım;
               }


           }
           if (uluToplam.uluKısım == 0) uluToplam = uluSıfırlayıcı();
           return uluToplam;
        }
        public static UluDegisken operator-(UluDegisken a, UluDegisken b)
        {
            UluDegisken uluToplam = new UluDegisken();
            if ((a.tamKısım == b.tamKısım) && (a.uluKısım == b.uluKısım))
            {
                uluToplam.tamKısım = a.tamKısım;
                uluToplam.katlıKısım = a.katlıKısım - b.katlıKısım;
                uluToplam.uluKısım = a.uluKısım;
            }

            return uluToplam;
        }
        public static UluDegisken operator*(UluDegisken a, UluDegisken b)
        {
            UluDegisken uluToplam = new UluDegisken();
          if(a.tamKısım==b.tamKısım)
            {
             uluToplam.tamKısım=a.tamKısım;
              uluToplam.katlıKısım=a.katlıKısım*b.katlıKısım;
              uluToplam.uluKısım=a.uluKısım+b.uluKısım;
            }

            return uluToplam;
        }
        public static UluDegisken operator ^(UluDegisken a, int b)
        {
            if (a.isaretim == '-') a.isaretim = '+';
            a.katlıKısım=(int)Math.Pow(a.katlıKısım,b);
            a.uluKısım *= b;

            return a;
        }
       /* public static UluDegisken[] operator ^(UluDegisken[] a, int b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].isaretim == '-') a[i].isaretim = '+';
                a[i].katlıKısım = (int)Math.Pow(a[i].katlıKısım, b);
                a[i].uluKısım *= b;
            }
            return a;
        }*/

    }
    }
