using System.Xml;
using System.Text;

namespace ICP_Automation_Project
{
    public class Helper
    {
        #region GetID
        public static String GetID(String elem)
        {
            //_logger.LogDebug("Get xml value by ID");
            return GetXmlValue(elem, "ID");
        }
        #endregion

        #region GetValue
        public static String GetValue(String elem)
        {
            //_logger.LogDebug("Get xml value by element");
            return GetXmlValue(elem, "Value");
        }
        #endregion

        #region GetXmlValue
        public static String GetXmlValue(String elem, String nodeName)
        {
            //_logger.LogDebug("Get xml value by nodename");
            XmlDocument xml = new XmlDocument();
            xml.Load("out.xml");
            String str = "//DocumentElement//Elements[Element='" + elem + "']";
            XmlNodeList xnList = xml.SelectNodes(str);
            String result = String.Empty;
            foreach (XmlNode xn in xnList)
            {
                result = xn[nodeName].InnerText;
            }
            return result;
        }
        #endregion

        #region
        public static string GetRandomTelNo()
        {
            //_logger.LogDebug("Generate random number");
            Random rand = new Random();
            StringBuilder telNo = new StringBuilder(12);
            int number;

            number = rand.Next(7, 10);
            telNo = telNo.Append(number);
            for (int i = 1; i < 10; i++)
            {
                number = rand.Next(0, 8); // digit between 0 (incl) and 8 (excl)
                telNo = telNo.Append(number.ToString());
            }
            return telNo.ToString();
        }
        #endregion
    }
}
