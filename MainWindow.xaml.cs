using System;
using System.IO;
using System.Net;
using System.Windows;

namespace SAODownloader
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly char[] CodeConverTable =
        {
            'f',
            'n',
            '5',
            'h',
            's',
            'e',
            'm',
            '4',
            'v',
            'c',
            '3',
            '9',
            'p',
            'j',
            't',
            '8',
            'b',
            '2',
            'k',
            '7',
            'g',
            '0',
            '1',
            'u',
            '6',
            'w',
            'y',
            '_',
            'o',
            'i',
            'r',
            'a',
            'l',
            'd',
            'q',
            '-',
            'x',
            'z',
            '.',
            '/',
            'Z'
        };

        private string _folder;

        private string _prefix;


        public MainWindow()
        {
            InitializeComponent();
            foreach (var directory in new[] {"ENReview", "ENRelease", "JPReview", "JPRelease"})
                Directory.CreateDirectory(directory);
        }

        private static string EncodeReleaseServerResourceName(string name)
        {
            var array = new char[name.Length];
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (c >= 'a' && c <= 'z')
                    array[i] = CodeConverTable[c - 'a'];
                else if (c >= '0' && c <= '9')
                    array[i] = CodeConverTable[c - '0' + '\u001a'];
                else
                    switch (c)
                    {
                        case '.':
                            array[i] = CodeConverTable[36];
                            break;
                        case '_':
                            array[i] = CodeConverTable[37];
                            break;
                        case '-':
                            array[i] = CodeConverTable[38];
                            break;
                        case '/':
                            array[i] = CodeConverTable[39];
                            break;
                        default:
                            array[i] = CodeConverTable[40];
                            break;
                    }
            }

            return new string(array) + ".asprjs";
        }

        private void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
            foreach (var s in TextBoxText.Text.Split(new[] {Environment.NewLine},
                StringSplitOptions.None))
                try
                {
                    new WebClient().DownloadFile(
                        $"https://saoif-com.sslcs.cdngc.net{_prefix}{EncodeReleaseServerResourceName(s)}",
                        $@"{_folder}/{s.Replace('/', '&')}");
                }
                catch
                {
                    // ignored
                }
        }

        private void RadioButtonENReview_Checked(object sender, RoutedEventArgs e)
        {
            _prefix = "/resources_fc/ie6n22b4/";
            _folder = "ENReview";
        }

        private void RadioButtonENRelease_Checked(object sender, RoutedEventArgs e)
        {
            _prefix = "/resources_fc/hiizaxgm8/";
            _folder = "ENRelease";
        }

        private void RadioButtonJPRelease_Checked(object sender, RoutedEventArgs e)
        {
            _prefix = "/resources/ie6n22b4/";
            _folder = "JPRelease";
        }

        private void RadioButtonJPReview_Checked(object sender, RoutedEventArgs e)
        {
            _prefix = "/resources/hiizaxgm8/";
            _folder = "JPReview";
        }
    }
}