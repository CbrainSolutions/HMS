using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;

namespace MainProjectHos.Models.DataLayer
{
    public class Commons
    {

        public static void ADDParameter(ref List<SqlParameter> lstParam, string param_name, DbType dbtype, object param_Value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = param_name;
            param.DbType = dbtype;
            param.Value = param_Value;
            lstParam.Add(param);
        }

        public static DateTime GetExpDate()
        {
            DateTime dt = Convert.ToDateTime(ConfigurationManager.ConnectionStrings["ExpDate"].ConnectionString);
            return dt;
        }


        public static bool IsRecordExists(string tableName, string columnName, string columnValue)
        {
            clsDataAccess mobjdata = new clsDataAccess();
            bool isExists = false;
            try
            {
                string sQuery = "Select Count(*) from " + tableName + " where ";
                if (columnValue.Trim().Length > 0)
                {
                    //if (glCode.Contains("'"))
                    //{
                    columnValue = columnValue.Replace("'", "''");
                    //}
                    sQuery += columnName + " = '" + columnValue + "'";
                }
                else
                {
                    sQuery += "isnull(" + columnName + ",'') = ''";
                }
                int cnt = Convert.ToInt32(mobjdata.ExecuteScalar(sQuery));
                if (cnt > 0)
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }
            }
            catch (Exception ex)
            {
                //Logger.Log("AutoNet.Common.Commons -GetColValue(string tableName, string glCode, string searchColName, string retColName)", ex);
                isExists = false;
            }
            return isExists;
        }

        public static bool IsRecordExists(string tableName, string[] columnName, string[] columnValue)
        {
            clsDataAccess mobjdata = new clsDataAccess();
            bool isExists = false;
            try
            {
                string sQuery = "Select Count(*) from " + tableName + " where ";
                for (int i = 0; i < columnName.Length; i++)
                {
                    if (columnValue[i].Trim().Length > 0)
                    {
                        columnValue[i] = columnValue[i].Replace("'", "''");
                        sQuery += columnName[i] + " = '" + columnValue[i].Trim() + "'";
                    }
                    else
                    {
                        sQuery += "isnull(" + columnName[i] + ",'') = ''";
                    }
                    if (i < (columnName.Length - 1))
                    {
                        sQuery += " and ";
                    }
                }
                int cnt = ConvertToInt(mobjdata.ExecuteScalar(sQuery));
                if (cnt > 0)
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }
            }
            catch (Exception ex)
            {
                //Logger.Log("AutoNet.Common.Commons -GetColValue(string tableName, string glCode, string searchColName, string retColName)", ex);
                isExists = false;
            }
            return isExists;
        }

        public static void FileLog(string Msg, Exception ex)
        {
            try
            {
                System.Diagnostics.StackTrace stTrace = null;
                if (ex.StackTrace != null)
                    stTrace = new System.Diagnostics.StackTrace(ex);
                System.Diagnostics.StackFrame stFrame = null;
                if (stTrace != null && stTrace.FrameCount > 0)
                    stFrame = stTrace.GetFrame(stTrace.FrameCount - 1);
                if (stFrame != null)
                    Msg += "At Line No. " + stFrame.GetFileLineNumber().ToString().Trim() + " Col " + stFrame.GetFileColumnNumber().ToString().Trim();
                string fileName = "Log" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                //string wDirName = Application.StartupPath + "\\Logs";
                string wDirName = HttpContext.Current.Server.MapPath("~/Logs");
                if (!Directory.Exists(wDirName))
                    Directory.CreateDirectory(wDirName);
                string filePath = wDirName + "\\" + fileName + ".log";
                FileStream fs;
                if (File.Exists(filePath))
                {
                    fs = new FileStream(@filePath, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(@filePath, FileMode.Create, FileAccess.Write);
                }
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("Message : " + Msg);
                sw.WriteLine("Exception : " + ex.Message);
                sw.WriteLine("LogDate : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                sw.WriteLine("User Name : " + SessionWrapper.UserName);
                int wLen = 0;
                if (Msg.Length > ex.Message.Length)
                {
                    wLen = Msg.Length;
                }
                else if (Msg.Length <= ex.Message.Length)
                {
                    wLen = ex.Message.Length;
                }
                sw.WriteLine("".PadRight(wLen, '-'));
                sw.Dispose();
                fs.Dispose();
            }
            catch
            {

            }
        }

        public static void ShowMessage(string strMsg, Page page)
        {
            if (SessionWrapper.CurrentBrowser == BrowserType.IE.ToString())
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "<script>window.onload=function(){show_alert('<span style=\"color:Green;font-size:13px;font-family:Verdana,Arial;font-weight:bold;\">" + strMsg + "</span>');};</script>", false);
                //ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "<script type='text/javascript'>alert('" + strMsg + "')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "<script>show_alert('<span style=\"color:Green;font-size:13px;font-family:Verdana,Arial;font-weight:bold;\">" + strMsg + "</span>');</script>", false);
                // ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "<script type='text/javascript'>alert('" + strMsg + "')</script>", false);
            }
        }

        public static bool SendMail(string mailFrom, string mailTo, string subject, string body)
        {
            return SendMail(mailFrom, string.Empty, mailTo, subject, body);
        }

        public static bool SendMail(string mailFrom, string displayName, string mailTo, string subject, string body)
        {
            bool result = false;
            MailMessage mailMsg = new MailMessage();
            if (displayName.Trim().Length > 0)
                mailMsg.From = new MailAddress(mailFrom, displayName);
            else
                mailMsg.From = new MailAddress(mailFrom);
            mailMsg.To.Add(mailTo);

            mailMsg.Subject = subject;
            mailMsg.Body = body;

            mailMsg.IsBodyHtml = true;
            mailMsg.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["CredEmail"],
                                                                                    ConfigurationManager.AppSettings["CredPassword"]);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;

            try
            {
                smtp.Send(mailMsg);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static Char CommaSeperator
        {
            get { return ','; }
        }

        /// <summary>
        /// Function to Convert Parameter Object to String.
        /// </summary>
        /// <param name="value">Object Name to be Converted.</param>
        /// <returns>string str</returns>
        public static string ConvertToString(object value)
        {
            string str = string.Empty;
            try
            {
                str = value.ToString().Trim();
            }
            catch (Exception)
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Boolean.
        /// </summary>
        /// <param name="value">Object Name to be Converted.</param>
        /// <returns>bool retVal</returns>
        public static bool ConvertToBool(object value)
        {
            bool retVal = false;
            try
            {
                retVal = (bool)value;
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Integer.
        /// </summary>
        /// <param name="value">Object Value to be Converted.</param>
        /// <returns> int str</returns>
        public static int ConvertToInt(object value)
        {
            int str = 0;
            try
            {
                str = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                str = 0;
            }
            return str;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Long.
        /// </summary>
        /// <param name="value">Object Value to be Converted.</param>
        /// <returns> int str</returns>
        public static long ConvertToLong(object value)
        {
            long str = 0;
            try
            {
                str = Convert.ToInt64(value);
            }
            catch (Exception)
            {
                str = 0;
            }
            return str;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Short.
        /// </summary>
        /// <param name="value">Object Value to be Converted.</param>
        /// <returns> int str</returns>
        public static short ConvertToShort(object value)
        {
            short str = 0;
            try
            {
                str = Convert.ToInt16(value);
            }
            catch (Exception)
            {
                str = 0;
            }
            return str;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Date.
        /// </summary>
        /// <param name="value">Object Value to be Converted.</param>
        /// <returns>DateTime dt</returns>
        public static DateTime ConvertToDate(object value)
        {
            DateTime dt = new DateTime();
            try
            {
                //dt = Convert.ToDateTime(value);
                dt = DateTime.ParseExact(ConvertToString(value), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                dt = Convert.ToDateTime("01/01/1753");
            }
            return dt;
        }

        /// <summary>
        /// Function to Convert Parameter Object to Decimal.
        /// </summary>
        /// <param name="value">Object Name to be Converted.</param>
        /// <returns>Decimal dec</returns>
        public static Decimal ConvertToDecimal(object value)
        {
            Decimal dec = 0.00M;
            try
            {
                dec = Convert.ToDecimal(value);
            }
            catch (Exception)
            {

                dec = 0.00M;
            }
            return dec;
        }



        public static string F1Key
        {
            get { return "112"; }
        }

        public static string CapA
        {
            get { return "65"; }
        }

        public static string Grave
        {
            get { return "96"; }
        }

        public static string Smallz
        {
            get { return "122"; }
        }

        public static string Space
        {
            get { return "32"; }
        }

        public static string Zero
        {
            get { return "46"; }
        }

        public static string Nine
        {
            get { return "57"; }
        }
        public static string Backspace
        {
            get { return "8"; }
        }

        public static string HoTab
        {
            get { return "9"; }
        }

        public static string FullStop
        {
            get { return "46"; }
        }

        public static string CapZ
        {
            get { return "90"; }
        }

        public static string Smalli
        {
            get { return "105"; }
        }

        public static string InvQueMark
        {
            get { return "191"; }
        }

        public static string Smallo
        {
            get { return "111"; }
        }

        public static string Percent
        {
            get { return "33"; }
        }

        public static string OpenParenth
        {
            get { return "46"; }
        }

        public static string Delete
        {
            get { return "127"; }
        }

        public static string Foundation
        {
            get { return "Foundation"; }
        }

        public static string Rupees(decimal dec)
        {
            int inputNo = Convert.ToInt32(dec);

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Fourty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0)
                    {
                        if (sb.ToString().Length > 0)
                        {
                            sb.Append("and ");
                        }

                    }
                    else
                    {
                        sb.Append("");
                    }
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}