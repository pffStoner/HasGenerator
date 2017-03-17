using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Class1
    {

        private Encoding converter;
        
        public static char[] db1 =
                    {
                        ' ','0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
                         , 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'
                         , 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R'
                         , 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                        '!', '@', '#', '$', '%', '^', '&', '.', '/', '{', '}',
                          '*', '(', ')', '_', '+', '-', '=', '`',
                    };

        public static string[] db2 =
                    {
                        "00","1", "2", "2", "3", "4", "5", "6",
                        "7", "8", "9", "0990", "0991", "0992", "0993",
                        "0994", "0995", "0996", "0997", "0998", "0999", "1000",
                        "1001", "1002", "1003", "1004", "1005", "1006", "1007",
                        "1008", "1009", "1010", "1011", "1012", "1013", "1014",
                        "1015", "1016", "1017", "1018", "1019", "1020", "1020"
                        ,"1021", "1022", "1023", "1024", "1025","1026","1027",
                        "1028","1029","1030","1031","1032","1033","1034","1035",
                         "1036",
                    };

        public void SetNewBaseCoding(Encoding m)
        {
            this.converter = m;
        }

        public Class1()
        {
            converter = new ASCIIEncoding(); //= Encoding.Default;
        }

        public string GetString(byte[] x)
        {
            return converter.GetString(x);
        }

        public byte[] GetBytes(string x)
        {
            return converter.GetBytes(x);
        }

        public string Encode(string x)
        {
            x = (x.ToUpper()).Trim();
            string res = "";
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < db1.Length; j++)
                {
                    if (x[i] == db1[j])
                    {
                        res += db2[j];
                        break;
                    }
                }
            }
            return res;
        }

       

        public byte[] Encode(byte[] m)
        {
            string x = this.converter.GetString(m);
            x = (x.ToUpper()).Trim();
            string res = "";
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < db1.Length; j++)
                {
                    if (x[i] == db1[j])
                    {
                        res += db2[j];
                        break;
                    }
                }
            }
            return this.converter.GetBytes(res);
        }

     

    }
}
