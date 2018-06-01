// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Microsoft.NET.Build.Tasks
{
    /// <summary>
    /// Embeds the App Name into the AppHost.exe
    /// </summary>
    public static class AppHost
    {
        private static StringBuilder _alllog;
        private static Stopwatch _stopWatch;
        private const string _placeHolder = "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2"; //hash value embedded in default apphost executable
        private readonly static byte[] _bytesToSearch = Encoding.UTF8.GetBytes(_placeHolder);

        /// <summary>
        /// Create an AppHost with embedded configuration of app binary location
        /// </summary>
        /// <param name="appHostSourceFilePath">The path of Apphost template, which has the place holder</param>
        /// <param name="appHostDestinationFilePath">The destination path for desired location to place, including the file name</param>
        /// <param name="appBinaryFilePath">Full path to app binary or relative path to the result apphost file</param>
        /// <param name="overwriteExisting">If override the file existed in <paramref name="appHostDestinationFilePath"/></param>
        public static void Create(
            string appHostSourceFilePath,
            string appHostDestinationFilePath,
            string appBinaryFilePath,
            bool overwriteExisting = false)
        {
            _alllog = new StringBuilder();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();

            _alllog.AppendLine("038: "+ _stopWatch.Elapsed.ToString());
            var hostExtension = Path.GetExtension(appHostSourceFilePath);
            var appbaseName = Path.GetFileNameWithoutExtension(appBinaryFilePath);
            var bytesToWrite = Encoding.UTF8.GetBytes(appBinaryFilePath);
            var destinationDirectory = new FileInfo(appHostDestinationFilePath).Directory.FullName;

            if (File.Exists(appHostDestinationFilePath))
            {
                //We have already done the required modification to apphost.exe
                return;
            }

            if (bytesToWrite.Length > 1024)
            {
                throw new BuildErrorException(Strings.FileNameIsTooLong, appBinaryFilePath);
            }

            _alllog.AppendLine("055: " + _stopWatch.Elapsed.ToString());
            var array = File.ReadAllBytes(appHostSourceFilePath);

            _alllog.AppendLine("057: " + _stopWatch.Elapsed.ToString());
            SearchAndReplace(array, _bytesToSearch, bytesToWrite, appHostSourceFilePath);

            _alllog.AppendLine("060: " + _stopWatch.Elapsed.ToString());
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            _alllog.AppendLine("067: " + _stopWatch.Elapsed.ToString());
            // Copy AppHostSourcePath to ModifiedAppHostPath so it inherits the same attributes\permissions.
            File.Copy(appHostSourceFilePath, appHostDestinationFilePath, overwriteExisting);

            _alllog.AppendLine("071: " + _stopWatch.Elapsed.ToString());
            // Re-write ModifiedAppHostPath with the proper contents.
            using (FileStream fs = new FileStream(appHostDestinationFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Read))
            {
                _alllog.AppendLine("075: " + _stopWatch.Elapsed.ToString());
                fs.Write(array, 0, array.Length);
            }

            _stopWatch.Stop();
            _alllog.AppendLine("080: " + _stopWatch.Elapsed.ToString());
            _alllog.AppendLine(_stopWatch.Elapsed.ToString());
            File.WriteAllText(@"C:\Users\wul\Downloads\AppHostLog_baseline.txt", _alllog.ToString());
        }

        // See: https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm
        private static int[] ComputeKMPFailureFunction(byte[] pattern)
        {
            _alllog.AppendLine("088: " + _stopWatch.Elapsed.ToString());
            int[] table = new int[pattern.Length];
            if (pattern.Length >= 1)
            {
                table[0] = -1;
            }
            if (pattern.Length >= 2)
            {
                table[1] = 0;
            }

            int pos = 2;
            int cnd = 0;
            while (pos < pattern.Length)
            {
                if (pattern[pos - 1] == pattern[cnd])
                {
                    table[pos] = cnd + 1;
                    cnd++;
                    pos++;
                }
                else if (cnd > 0)
                {
                    cnd = table[cnd];
                }
                else
                {
                    table[pos] = 0;
                    pos++;
                }
            }
            return table;
        }

        // See: https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm
        private static int KMPSearch(byte[] pattern, byte[] bytes)
        {
            int m = 0;
            int i = 0;
            int[] table = ComputeKMPFailureFunction(pattern);

            _alllog.AppendLine("129: " + _stopWatch.Elapsed.ToString());
            while (m + i < bytes.Length)
            {
                if (pattern[i] == bytes[m + i])
                {
                    if (i == pattern.Length - 1)
                    {
                        return m;
                    }
                    i++;
                }
                else
                {
                    if (table[i] > -1)
                    {
                        m = m + i - table[i];
                        i = table[i];
                    }
                    else
                    {
                        m++;
                        i = 0;
                    }
                }
            }
            return -1;
        }

        private static void SearchAndReplace(byte[] array, byte[] searchPattern, byte[] patternToReplace, string appHostSourcePath)
        {
            _alllog.AppendLine("159: " + _stopWatch.Elapsed.ToString());


            int offset = KMPSearch(searchPattern, array);
            if (offset < 0)
            {
                throw new BuildErrorException(Strings.AppHostHasBeenModified, appHostSourcePath, _placeHolder);
            }

            _alllog.AppendLine("168: " + _stopWatch.Elapsed.ToString());
            patternToReplace.CopyTo(array, offset);

            _alllog.AppendLine("171: " + _stopWatch.Elapsed.ToString());
            if (patternToReplace.Length < searchPattern.Length)
            {
                for (int i = patternToReplace.Length; i < searchPattern.Length; i++)
                {
                    array[i + offset] = 0x0;
                }
            }
            _alllog.AppendLine("0179: " + _stopWatch.Elapsed.ToString());
        }
    }
}
