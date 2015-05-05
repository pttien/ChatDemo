using MailChimp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailChimp
{
    class Program
    {






        public static bool AddOrUpdateSubscriber(string email, string list, string apiKey, string firstName, string lastName)
        {
            var mcApi = new MCApi(apiKey, true);
            var merges = new List.Merges() ;
            merges.Add("FNAME", firstName);
            merges.Add("LNAME", lastName);
            var subscriptionOptions = new List.SubscribeOptions();
            subscriptionOptions.UpdateExisting = true;
            subscriptionOptions.DoubleOptIn = false;
            subscriptionOptions.SendWelcome = false;
            return mcApi.ListSubscribe(list, email, merges, subscriptionOptions);
        }
        static void Main(string[] args)
        {
            int[] A  = new int[]{1,2,3,4,5,6,7,8,9,10};
              int solanIn;
              int soMoiLanIn;
              int chieuDaiMang = A.Length;
              Console.WriteLine(" nhap so lan in : " );
              solanIn =int.Parse( Console.ReadLine());
  
               Console.WriteLine(" nhap so luong so trong 1 lan in : " );
               soMoiLanIn = int.Parse(Console.ReadLine());
              int i = 0;
  
              while( i < solanIn){
               int start = i * (soMoiLanIn);
               Console.WriteLine(" start " + start);
               Console.WriteLine(" Lan " + (i + 1) +  " : ");
               for(int j = 0; j < soMoiLanIn; j++){
                 Console.WriteLine(" "+ A[(start + j )%chieuDaiMang]);
               }
               Console.WriteLine("");
               i++;
              }
  
              Console.WriteLine("solanIn = " + solanIn);
  
           Console.ReadKey();

        }

    }
}
