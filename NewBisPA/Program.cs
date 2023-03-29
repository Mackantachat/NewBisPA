using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using ITUtility;
using System.Diagnostics;


namespace NewBisPA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormKwanbualung_Report());
            try
            {
                RegistryKey localMachineReg = Registry.LocalMachine;

                RegistryKey softwareReg = localMachineReg.OpenSubKey("Software", true);

                RegistryKey bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);
                if (bangkokLifeAssuranceReg == null)
                {
                    RegistryKey a = softwareReg.OpenSubKey("Wow6432Node", true);

                    bangkokLifeAssuranceReg = a.OpenSubKey("Bangkok Life Assurance", true);
                    if (bangkokLifeAssuranceReg == null)
                        throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                }

                RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);
                if (blaWinAppMenuReg == null)
                    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");

                string UserID = (string)blaWinAppMenuReg.GetValue("UserID");
                if (UserID == null)
                    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                string Password = (string)blaWinAppMenuReg.GetValue("Password");
                if (Password == null)
                    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                DateTime MenuLoginDate = Convert.ToDateTime((string)blaWinAppMenuReg.GetValue("LoginDate"));
                if (MenuLoginDate == new DateTime())
                    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                string StartCommand = (string)blaWinAppMenuReg.GetValue("StartCommand");

                blaWinAppMenuReg.Close();
                bangkokLifeAssuranceReg.Close();
                softwareReg.Close();
                localMachineReg.Close();
                //Application.Run(new ApplicationForm(UserID));
                //Application.Run(new OldCustomerDataForm());
                //Application.Run(new FormApplicationSelector(UserID));
                //  Application.Run(new ApplicationForm());
                if (StartCommand == Utility.AppSettings("appListProgramGroupId"))
                    Application.Run(new FormApplicationSelector(UserID));
                else if (StartCommand == Utility.AppSettings("appViewerProgramGroupId"))
                    Application.Run(new FormApplicationViewer(UserID));
                else if (StartCommand == Utility.AppSettings("nbUnderwriteProgramGroupId"))
                    Application.Run(new ApplicationForm());
                else if (StartCommand == Utility.AppSettings("KWBLReportProgramGroupId"))
                    Application.Run(new FormKwanbualung_Report());
                else if (StartCommand == Utility.AppSettings("nbPaRejectLetter"))
                    Application.Run(new ReportForm.frmRejectionReport(UserID));
                else if (StartCommand == Utility.AppSettings("nbPaFollowLetter"))
                    Application.Run(new ReportForm.frmCallReport(UserID));
                else
                {
                    long programGroupId;
                    if (long.TryParse(StartCommand, out programGroupId))
                    {
                        SecuritySvcRef.ProgramGroup pg = null;
                        SecuritySvcRef.ProcessResult pr;
                        using (SecuritySvcRef.SecurityServiceClient client = new SecuritySvcRef.SecurityServiceClient())
                        {
                            if (StartCommand == Utility.AppSettings("nbPaRejectLetter"))
                            {
                                // blaWinAppMenuReg.SetValue("StartCommand","42", RegistryValueKind.String); 
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("underWriteProgramGroupId")));
                                pg.Description = "ระบบ" + pg.Description;
                            }
                            else if (StartCommand == Utility.AppSettings("nbPaFollowLetter"))
                            {
                                // blaWinAppMenuReg.SetValue("StartCommand", "46", RegistryValueKind.String);
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("underWriteProgramGroupId")));
                                pg.Description = "ระบบ" + pg.Description;
                            }
                            else if (StartCommand == Utility.AppSettings("KWBLCloseAccountProgramGroupId"))
                            {
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("KWBLManagementProgramGroupId")));
                            }
                            else if (StartCommand == Utility.AppSettings("KWBLCloseDateEditProgramGroupId"))
                            {
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("KWBLManagementProgramGroupId")));
                            }
                            else if (StartCommand == Utility.AppSettings("KWBLLetterProgramGroupId"))
                            {
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("KWBLManagementProgramGroupId")));
                            }
                            else if (StartCommand == Utility.AppSettings("KWBLCloseAccountFileTextProgramGroupId"))
                            {
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(Utility.AppSettings("KWBLManagementProgramGroupId")));
                            }
                            else
                            {
                                pr = client.GetProgramGroupById(out pg, Convert.ToInt64(StartCommand));
                            }

                        }
                        if (pr.Successed)
                        {

                            if (pg != null)
                            {

                                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);

                                System.IO.DirectoryInfo appDataDirInfo = new System.IO.DirectoryInfo(appDataPath);

                                System.IO.DirectoryInfo startMenuDir = null;

                                startMenuDir = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

                                System.IO.FileInfo[] files = startMenuDir.GetFiles(pg.Description + ".appref-ms", System.IO.SearchOption.AllDirectories);

                                if (files.Length == 0)
                                    MessageBox.Show("กรุณาติดตั้ง" + pg.Description);
                                else
                                {
                                    Process.Start(files[0].FullName);
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Application.Run(new ExceptionHandleForm(ex.Message));
            }

        }
    }
}
