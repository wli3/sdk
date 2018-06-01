// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.NET.Build.Tasks
{
    /// <summary>
    /// Embeds the App Name into the AppHost.exe
    /// </summary>
    public static class AppHost
    {
        private const string _placeHolder = "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2"; //hash value embedded in default apphost executable
        private readonly static byte[] _bytesToSearch = Encoding.UTF8.GetBytes(_placeHolder);
        private static StringBuilder _alllog;
        private static Stopwatch _stopWatch;

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
            var hostExtension = Path.GetExtension(appHostSourceFilePath);
            var appbaseName = Path.GetFileNameWithoutExtension(appBinaryFilePath);
            var bytesToWrite = Encoding.UTF8.GetBytes(appBinaryFilePath);
            var destinationDirectory = new FileInfo(appHostDestinationFilePath).Directory.FullName;

            _alllog.AppendLine(_stopWatch.Elapsed.ToString());

            if (File.Exists(appHostDestinationFilePath))
            {
                //We have already done the required modification to apphost.exe
                return;
            }

            _alllog.AppendLine(_stopWatch.Elapsed.ToString());
            if (bytesToWrite.Length > 1024)
            {
                throw new BuildErrorException(Strings.FileNameIsTooLong, appBinaryFilePath);
            }

            _alllog.AppendLine(_stopWatch.Elapsed.ToString());

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }


            _alllog.AppendLine("64: " + _stopWatch.Elapsed.ToString());

            File.Copy(appHostSourceFilePath, appHostDestinationFilePath, overwriteExisting);
            _alllog.AppendLine("68: " + _stopWatch.Elapsed.ToString());

            _alllog.AppendLine("memory: " + GC.GetTotalMemory(true));
            using (var memoryMappedFile = MemoryMappedFile.CreateFromFile(appHostDestinationFilePath, FileMode.Open))
            {
                _alllog.AppendLine("72: " + _stopWatch.Elapsed.ToString());
                using (MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor())
                {
                    _alllog.AppendLine("75: " + _stopWatch.Elapsed.ToString());
                    SearchAndReplace(accessor, _bytesToSearch, bytesToWrite, appHostSourceFilePath);
                }
            }
            _stopWatch.Stop();
            _alllog.AppendLine(_stopWatch.Elapsed.ToString());
            File.WriteAllText(@"C:\Users\wul\Downloads\AppHostLog.txt", _alllog.ToString());
        }

        // See: https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm
        private static int[] ComputeKMPFailureFunction(byte[] pattern)
        {
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
        private static unsafe int KMPSearch(byte[] pattern, MemoryMappedViewAccessor accessor)
        {
            int m = 0;
            int i = 0;
            int[] table = ComputeKMPFailureFunction(pattern);

            _alllog.AppendLine("125: " + _stopWatch.Elapsed.ToString());

            byte* ptrMemMap = (byte*)0;
            accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref ptrMemMap);
          //  new String((sbyte*)ptrMemMap + offset);

            while (m + i < accessor.Capacity)
            {
                if (pattern[i] == *(ptrMemMap + m + i))
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

            _alllog.AppendLine("152: " + _stopWatch.Elapsed.ToString());

            return -1;
        }

        private static void SearchAndReplace(
            MemoryMappedViewAccessor accessor,
            byte[] searchPattern,
            byte[] patternToReplace,
            string appHostSourcePath)
        {
            _alllog.AppendLine("163: " + _stopWatch.Elapsed.ToString());
            int position = KMPSearch(searchPattern, accessor);
            if (position < 0)
            {
                throw new BuildErrorException(Strings.AppHostHasBeenModified, appHostSourcePath, _placeHolder);
            }

            _alllog.AppendLine("170: " + _stopWatch.Elapsed.ToString());
            accessor.WriteArray(
                position: position,
                array: patternToReplace,
                offset: 0,
                count: patternToReplace.Length);

            _alllog.AppendLine("177: " + _stopWatch.Elapsed.ToString());
            if (patternToReplace.Length < searchPattern.Length)
            {
                for (int i = patternToReplace.Length; i < searchPattern.Length; i++)
                {
                    byte empty = 0x0;
                    accessor.Write(i + position, empty);
                }
            }

            _alllog.AppendLine("187: " + _stopWatch.Elapsed.ToString());
        }
    }
}
