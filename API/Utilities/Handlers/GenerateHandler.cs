using API.Models;
namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        
        public static string Nik(string nik)
        {
            string newNik;
            if(nik is null)
            {
                newNik = "111111";
            } else
            {
                newNik= (int.Parse(nik) + 1).ToString();
            }

            return newNik;
        }
    }
}
