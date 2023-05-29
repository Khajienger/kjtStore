using System;
using System.IO;
using System.Windows;

namespace kjtStore
{
    internal class Autorizator
    {
        private string programDataPath = @"pd";
        private string pathLog = @"pd\lg.kjt";
        private string pathPass = @"pd\ps.kjt";

        private bool autorizatorIsLoad = false;

        public bool LoginAndPassword(string Login, string Password)
        {
            bool success = true;
            bool _success;

            _success = KjtPasswordChecker.CheckInputAndRealPassowrd
                (Login, KjtWriterReaderCipher.ReadText(pathLog, true), false) ?
                true : success = false;

            _success = KjtPasswordChecker.CheckInputAndRealPassowrd
                (Password, KjtWriterReaderCipher.ReadText(pathPass, true), false) ?
                true : success = false;

            return success;
        }

        public bool ChangePassword(string OldPassword, string NewPassword, string NewPasswordRepeat)
        {
            try
            {
                bool success = true;
                bool _success;

                _success = KjtPasswordChecker.CheckInputAndRealPassowrd
                    (OldPassword,
                    KjtWriterReaderCipher.ReadText(pathPass, true),
                    true) ?
                    true : success = false;

                _success = KjtPasswordChecker.CheckRepeatEqual
                    (NewPassword, NewPasswordRepeat, true) ?
                    true : success = false;

                _success = KjtPasswordChecker.CheckEmpty
                    (NewPassword, true) ?
                    true : success = false;

                _success = KjtPasswordChecker.CheckLength7x15
                    (NewPassword, true) ?
                    true : success = false;

                _success = KjtPasswordChecker.CheckRepeats7x
                    (NewPassword, true) ?
                    true : success = false;

                if (success)
                {
                    KjtWriterReaderCipher.WriteText
                    (pathPass,
                    NewPassword,
                    false,
                    true);
                }

                return success;

            }
            catch (Exception e)
            {
                MessageBox.Show($"Возникла какая - то ошибка!\n\n{e.Message}");
                return false;
            }
        }

        public void Preparing()
        {
            if(!autorizatorIsLoad)
            {
                CheckProgramFiles();
                autorizatorIsLoad = true;
            }
        }

        private void CheckProgramFiles()
        {
            if (!Directory.Exists(programDataPath))
            {
                Directory.CreateDirectory(programDataPath);
            }

            if (!File.Exists(pathLog))
            {
                KjtWriterReaderCipher.WriteText(pathLog, "onlinestore", true, true);
            }

            if (!File.Exists(pathPass))
            {
                KjtWriterReaderCipher.WriteText(pathPass, "onlinetrader", true, true);
            }
        }
    }
}