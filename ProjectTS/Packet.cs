using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//wytyczne
//
//połączeniowy,
//wszystkie dane przesyłane w postaci binarnej,
//pole operacji o długości 3 bitów,
//pola liczb o długości 32 bitów,
//pole statusu o długości 2 bitów,
//pole identyfikatora o długości 8 bitów,
//dodatkowe pola zdefiniowane przez programistę

namespace ProjectTS
{
    public class Packet
    {
        BitArray bitArr;
        int index = 0;
        int size;
        
        BitArray operation = new BitArray(3);
        int number1;
        int number2;
        BitArray status = new BitArray(2);
        BitArray id = new BitArray(8);
        BitArray state = new BitArray(2); //odpowiada za informowanie serwera czy jest to pakiet           \
                                          //z operacją dwuargumentową lub wieloargumentową o wartościach:  |
                                          //"00"-operacja dwuargumentowa,                                  |-czekam na odpowiedz Marka
                                          //"01"-operacja wieloargumentowa, ale nie ostatni pakiet,        | czy tak może być to zrobione
                                          //"10"-operacja wieloargumentowa, ostatni pakiet.                / 

        public Packet(int size)
        {
            this.size = size;
            bitArr = new BitArray(size);
        }

        public void Add(bool value)
        {
            bitArr.Set(index, value);
            index++;
        }

        public void Add(byte value)
        {
            for (int i = 0; i < 8; i++)
            {
                this.Add((value & (1 << i)) != 0);
            }
        }

        public void Add(int value)
        {
            foreach (byte by in BitConverter.GetBytes(value))
            {
                Add(by);
            }
        }
        public byte[] GetBytes()
        {
            byte[] ret = new byte[(bitArr.Length - 1) / 8 + 1];
            bitArr.CopyTo(ret, 0);
            return ret;
        }
    }
}
