using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPI_Thu_Thap.BUS
{
    public class cachThucDuThau
    {
        string xpath1 = "//*[@id=\"info-general\"]/div[5]/div[2]/div[";
        string xpath2 = "]/div[2]";

        public List<string> getData()
        {
            int i = 0;
            List<string> xpath = new List<string>();
            for (i = 1; i <= 5; i ++)
            {
                xpath.Add(xpath1 + i + xpath2);
            }
            return xpath;
        }
    }
}
