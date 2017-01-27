using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // 追加

namespace FolderCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Scan";
            watcher.Filter = "*.txt";
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

            watcher.Created += new FileSystemEventHandler(watcher_Changed);
            watcher.Deleted += new FileSystemEventHandler(watcher_Changed);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);

            // 監視を開始
            watcher.EnableRaisingEvents = true;
            Console.WriteLine(watcher.Path + "を監視中...");

            Console.ReadKey();
        }

        // 作成・変更・削除のイベントハンドラ
        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    Console.WriteLine("ファイル 「" + e.FullPath + "」が変更されました。");
                    break;
                case System.IO.WatcherChangeTypes.Created:
                    Console.WriteLine("ファイル 「" + e.FullPath + "」が作成されました。");
                    break;
                case System.IO.WatcherChangeTypes.Deleted:
                    Console.WriteLine("ファイル 「" + e.FullPath + "」が削除されました。");
                    break;
            }
        }

        // 名前変更のイベントハンドラ
        private static void watcher_Renamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("ファイル 「" + e.FullPath + "」の名前が変更されました。");
        }

    }
}
