using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPI_Thu_Thap.BUS
{
    public class thongTinGoiThau
    {
       public List<string> getData(string check)
        {
            int limit = 0;
            string xpath1 = "//*[@id=\"info-general\"]/div[4]/div[2]/div[";
            string xpath2 = "]/div[2]";
            List<string> xpath = new List<string>();
            if(check != "Chủ đầu tư")
            {
                limit = 9;
            }    
            else
            {
                limit = 10;
            }

            for(int i = 1; i <= limit; i++)
            {
                xpath.Add(xpath1 + i + xpath2);
            }
            return xpath;
        }
    }
}
