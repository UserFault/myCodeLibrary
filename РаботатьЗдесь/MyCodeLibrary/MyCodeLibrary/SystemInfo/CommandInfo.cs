using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;


namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Allows command prompt commands to be run.
    /// </summary>
    public static class CommandInfo
    {
        /// <summary>
        /// Output object that is returned after the command has completed
        /// </summary>
        public class Output
        {

            private List<string> m_Result;

            private List<string> m_Error;

            private int m_ExitCode;

            private String m_Exception;


            public Output()
            {
                this.m_Error = new List<String>();
                this.m_Exception = String.Empty;
                this.m_ExitCode = 0;
                this.m_Result = new List<String>();
            }

            //Этих коллекций нет в NET Framework 2.0 - они есть в 3.0, и они потокобезопасные.
            //Я их заменил пока простыми списками, авось прокатит.
            /// <summary>
            /// Returns the text result of the command.
            /// </summary>
            public List<string> Result
            {
                get { return m_Result; }
            }

            /// <summary>
            /// Returns the error if an error occurred.
            /// </summary>
            public List<string> Error
            {
                get { return m_Error; }
            }
            /// <summary>
            /// Returns the exit code. Returns 0 if no error occurred.
            /// </summary>
            public int ExitCode
            {
                get { return m_ExitCode; }
                internal set { m_ExitCode = value; }
            }
            /// <summary>
            /// Returns the exception if an exception occurred.
            /// </summary>
            public String Exception
            {
                get { return m_Exception; }
                internal set { m_Exception = value; }
            }

            //заменены из

            ///// <summary>
            ///// Returns the text result of the command.
            ///// </summary>
            //public ObservableCollection<String> Result { get; } = new ObservableCollection<String>();

            ///// <summary>
            ///// Returns the error if an error occurred.
            ///// </summary>
            //public ObservableCollection<String> Error { get; } = new ObservableCollection<String>();

            ///// <summary>
            ///// Returns the exit code. Returns 0 if no error occurred.
            ///// </summary>
            //public int ExitCode { get; internal set; } = 0;

            ///// <summary>
            ///// Returns the exception if an exception occurred.
            ///// </summary>
            //public String Exception { get; internal set; } = String.Empty;
        }

        /// <summary>
        /// Runs a command prompt command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Output Run(object command)
        {
            return Run(command, false, false);
        }

        /// <summary>
        /// Runs a command prompt command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Output RunElevated(object command)
        {
            return Run(command, false, true);
        }

        /// <summary>
        /// Runs a command prompt command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="returnOutput"></param>
        /// <returns></returns>
        public static Output Run(object command, Boolean returnOutput)
        {
            return Run(command, returnOutput, false);
        }

        private static Output Run(object command, Boolean returnOutput, Boolean elevate)
        {
            var output = new Output();
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                {

                    // The following commands are needed to redirect the Standard output.
                    // This means that it will be redirected to the Process.StandardOutput StreamReader.
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    // Do not create the black window.
                    CreateNoWindow = true
                };

                if(elevate)
                {
                    procStartInfo.RedirectStandardOutput = false;
                    procStartInfo.RedirectStandardError = false;
                    procStartInfo.UseShellExecute = true;
                    procStartInfo.Verb = "runas";
                    procStartInfo.CreateNoWindow = false;
                }

                // Now we create a process, assign its ProcessStartInfo and start it
                using (Process proc = new System.Diagnostics.Process())
                {
                    proc.StartInfo = procStartInfo;
                    proc.Start();

                    if (returnOutput)
                    {
                        String line;
                        // Get the output into a strings
                        while ((line = proc.StandardOutput.ReadLine()) != null)
                        {
                            output.Result.Add(line);
                        }
                        while ((line = proc.StandardError.ReadLine()) != null)
                        {
                            output.Error.Add(line);
                        }
                        output.ExitCode = proc.ExitCode;
                    }
                }
            }
            catch (Exception objException) { output.Exception = Convert.ToString(objException, CultureInfo.CurrentCulture); }

            return output;
        }
    }
}
